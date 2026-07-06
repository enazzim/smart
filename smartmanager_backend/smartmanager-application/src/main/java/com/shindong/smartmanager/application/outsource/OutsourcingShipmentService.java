package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentStatus;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;

public class OutsourcingShipmentService {

    private final OutsourcingShipmentRepository outsourcingShipmentRepository;
    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final OutsourcingShipmentConsumptionCalculator consumptionCalculator;
    private final OutsourcingShipmentInventoryService inventoryService;
    private final InventoryBalanceService inventoryBalanceService;
    private final ItemRepository itemRepository;
    private final ProcessRepository processRepository;
    private final MonthClosingService monthClosingService;
    private final DomainEventStore domainEventStore;

    public OutsourcingShipmentService(
            OutsourcingShipmentRepository outsourcingShipmentRepository,
            OutsourcingOrderRepository outsourcingOrderRepository,
            OutsourcingShipmentConsumptionCalculator consumptionCalculator,
            OutsourcingShipmentInventoryService inventoryService,
            InventoryBalanceService inventoryBalanceService,
            ItemRepository itemRepository,
            ProcessRepository processRepository,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        this.outsourcingShipmentRepository = outsourcingShipmentRepository;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.consumptionCalculator = consumptionCalculator;
        this.inventoryService = inventoryService;
        this.inventoryBalanceService = inventoryBalanceService;
        this.itemRepository = itemRepository;
        this.processRepository = processRepository;
        this.monthClosingService = monthClosingService;
        this.domainEventStore = domainEventStore;
    }

    public List<OutsourcingShipmentCandidateView> listCandidates() {
        List<OutsourcingShipmentCandidateView> candidates = new ArrayList<>();
        for (OutsourcingOrderView order : outsourcingOrderRepository.findAllActive()) {
            if (order.status() == OutsourcingOrderStatus.CANCELLED) {
                continue;
            }
            for (OutsourcingOrderLineView line : order.lines()) {
                BigDecimal remaining = line.orderQty().subtract(line.shippedQty());
                if (remaining.compareTo(BigDecimal.ZERO) <= 0) {
                    continue;
                }
                boolean shippable = order.status() != OutsourcingOrderStatus.RECEIVED;
                String message = shippable ? null : "입고 완료된 발주입니다.";
                candidates.add(new OutsourcingShipmentCandidateView(
                        line.id(),
                        order.id(),
                        order.orderNo(),
                        order.orderDate(),
                        order.partnerId(),
                        order.partnerName(),
                        line.itemId(),
                        line.itemNo(),
                        line.itemName(),
                        line.processName(),
                        line.beginProcessName(),
                        line.endProcessName(),
                        line.orderQty(),
                        line.shippedQty(),
                        remaining,
                        shippable,
                        message
                ));
            }
        }
        return candidates;
    }

    public OutsourcingShipmentInputPreviewView previewInput(long orderLineId, BigDecimal shipmentQty, LocalDate shipmentDate) {
        OrderLineContext context = resolveOrderLineContext(orderLineId);
        LocalDate stockDate = shipmentDate != null ? shipmentDate : LocalDate.now();
        List<OutsourcingShipmentInputSaveCommand> inputs = consumptionCalculator.calculateInputLines(
                context.order(),
                context.line(),
                shipmentQty,
                "system"
        );
        Map<Long, String> processNames = loadProcessNames(context.line().itemId());
        List<OutsourcingShipmentInputPreviewLineView> lines = inputs.stream()
                .map(input -> {
                    var item = itemRepository.findActiveById(input.itemId()).orElseThrow();
                    BigDecimal onHand = inventoryBalanceService.currentStockQty(
                            input.itemId(),
                            input.sourceLocationCode(),
                            stockDate,
                            input.sourceProcessId(),
                            null,
                            null
                    );
                    BigDecimal unitRatio = shipmentQty.compareTo(BigDecimal.ZERO) > 0
                            ? input.issueQty().divide(shipmentQty, 4, java.math.RoundingMode.HALF_UP)
                            : BigDecimal.ZERO;
                    return new OutsourcingShipmentInputPreviewLineView(
                            input.itemId(),
                            item.itemNo(),
                            item.itemName(),
                            item.propertyClassification().name(),
                            input.itemCompositionId(),
                            unitRatio,
                            input.issueQty(),
                            input.sourceLocationCode(),
                            input.sourceProcessId(),
                            input.inputProcessId(),
                            processNames.getOrDefault(input.inputProcessId(), ""),
                            onHand
                    );
                })
                .toList();
        return new OutsourcingShipmentInputPreviewView(
                orderLineId,
                context.order().orderNo(),
                context.line().itemNo(),
                shipmentQty,
                lines
        );
    }

    public List<OutsourcingShipmentView> list(OutsourcingShipmentListCriteria criteria) {
        return outsourcingShipmentRepository.findAllActive(criteria);
    }

    public OutsourcingShipmentView register(CreateOutsourcingShipmentCommand command, String actorUserId) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("출고할 발주 라인을 1건 이상 선택하세요.");
        }
        LocalDate shipmentDate = command.shipmentDate() != null ? command.shipmentDate() : LocalDate.now();
        monthClosingService.assertTransactionOpen(shipmentDate);

        Set<Long> seenOrderLineIds = new HashSet<>();
        List<OutsourcingShipmentLineSaveCommand> lineSaves = new ArrayList<>();
        Map<Long, OutsourcingOrderView> orderByLineId = new HashMap<>();

        for (CreateOutsourcingShipmentLineCommand lineCommand : command.lines()) {
            if (!seenOrderLineIds.add(lineCommand.orderLineId())) {
                throw new IllegalArgumentException("중복된 발주 라인입니다: " + lineCommand.orderLineId());
            }
            OrderLineContext context = resolveOrderLineContext(lineCommand.orderLineId());
            orderByLineId.put(lineCommand.orderLineId(), context.order());

            if (context.order().status() == OutsourcingOrderStatus.CANCELLED) {
                throw new IllegalArgumentException("취소된 발주는 출고할 수 없습니다: " + context.order().orderNo());
            }
            BigDecimal remaining = context.line().orderQty().subtract(context.line().shippedQty());
            if (lineCommand.shipmentQty().compareTo(remaining) > 0) {
                throw new IllegalArgumentException(
                        "발주 잔량(" + remaining.stripTrailingZeros().toPlainString()
                                + ")을 초과하는 출고수량입니다.");
            }

            List<OutsourcingShipmentInputSaveCommand> inputs = consumptionCalculator.calculateInputLines(
                    context.order(),
                    context.line(),
                    lineCommand.shipmentQty(),
                    actorUserId
            );
            inventoryService.assertSufficientStock(shipmentDate, context.order().partnerId(), inputs);
            lineSaves.add(new OutsourcingShipmentLineSaveCommand(
                    lineCommand.orderLineId(),
                    lineCommand.shipmentQty(),
                    inputs
            ));
        }

        String shipmentNo = nextShipmentNo(shipmentDate);
        OutsourcingShipmentView saved = outsourcingShipmentRepository.save(
                new OutsourcingShipmentSaveCommand(shipmentNo, shipmentDate, lineSaves),
                actorUserId
        );

        for (OutsourcingShipmentLineSaveCommand lineSave : lineSaves) {
            OutsourcingOrderView order = orderByLineId.get(lineSave.orderLineId());
            outsourcingOrderRepository.addShippedQty(lineSave.orderLineId(), lineSave.shipmentQty(), actorUserId);
            inventoryService.applyRegistration(
                    shipmentDate,
                    saved.id(),
                    order.partnerId(),
                    lineSave.inputLines(),
                    actorUserId
            );
            outsourcingOrderRepository.refreshOrderStatus(order.id(), actorUserId);
        }

        appendEvent(EventTypes.OUTSOURCING_SHIPMENT_REGISTERED, saved.id(), actorUserId, saved.shipmentNo());
        return outsourcingShipmentRepository.findActiveIssuedById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        OutsourcingShipmentView shipment = outsourcingShipmentRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("외주출고를 찾을 수 없습니다: " + id));
        if (!shipment.cancelable()) {
            throw new IllegalArgumentException("취소할 수 없는 외주출고입니다.");
        }
        monthClosingService.assertTransactionOpen(shipment.shipmentDate());

        for (OutsourcingShipmentLineView line : shipment.lines()) {
            OrderLineContext context = resolveOrderLineContext(line.orderLineId());
            if (context.line().receivedQty().compareTo(BigDecimal.ZERO) > 0) {
                throw new IllegalArgumentException("입고 실적이 있어 출고를 취소할 수 없습니다.");
            }
            List<OutsourcingShipmentInputSaveCommand> inputs = line.inputLines().stream()
                    .map(input -> new OutsourcingShipmentInputSaveCommand(
                            input.itemId(),
                            input.itemCompositionId(),
                            input.issueQty(),
                            input.sourceLocationCode(),
                            input.sourceProcessId(),
                            input.inputProcessId()
                    ))
                    .toList();
            inventoryService.applyCancellation(
                    shipment.shipmentDate(),
                    shipment.id(),
                    context.order().partnerId(),
                    inputs,
                    actorUserId
            );
            outsourcingOrderRepository.subtractShippedQty(line.orderLineId(), line.shipmentQty(), actorUserId);
            outsourcingOrderRepository.refreshOrderStatus(context.order().id(), actorUserId);
        }

        outsourcingShipmentRepository.cancelById(id, actorUserId);
        appendEvent(EventTypes.OUTSOURCING_SHIPMENT_CANCELLED, id, actorUserId, shipment.shipmentNo());
    }

    private OrderLineContext resolveOrderLineContext(long orderLineId) {
        OutsourcingOrderLineView line = outsourcingOrderRepository.findActiveLineById(orderLineId)
                .orElseThrow(() -> new IllegalArgumentException("외주발주 라인을 찾을 수 없습니다: " + orderLineId));
        OutsourcingOrderView order = outsourcingOrderRepository.findActiveByOrderLineId(orderLineId)
                .orElseThrow(() -> new IllegalArgumentException("외주발주를 찾을 수 없습니다."));
        OutsourcingOrderLineView refreshed = order.lines().stream()
                .filter(row -> row.id() == orderLineId)
                .findFirst()
                .orElse(line);
        return new OrderLineContext(order, refreshed);
    }

    private Map<Long, String> loadProcessNames(long itemId) {
        return processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan).stream()
                .collect(Collectors.toMap(ProcessView::id, ProcessView::processName, (left, right) -> left));
    }

    private String nextShipmentNo(LocalDate shipmentDate) {
        String prefix = "OS-" + shipmentDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = outsourcingShipmentRepository.countByShipmentNoPrefix(prefix) + 1;
        return prefix + seq;
    }

    private void appendEvent(String eventType, long shipmentId, String actorUserId, String shipmentNo) {
        String payload = """
                {"outsourcingShipmentId":%d,"shipmentNo":"%s"}
                """.formatted(shipmentId, shipmentNo).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.OUTSOURCING_SHIPMENT,
                String.valueOf(shipmentId),
                actorUserId,
                payload
        ));
    }

    private record OrderLineContext(OutsourcingOrderView order, OutsourcingOrderLineView line) {
    }
}
