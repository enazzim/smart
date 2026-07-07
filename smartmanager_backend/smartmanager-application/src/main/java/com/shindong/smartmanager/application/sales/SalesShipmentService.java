package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class SalesShipmentService {

    private final SalesShipmentRepository salesShipmentRepository;
    private final SalesShipmentInventoryService inventoryService;
    private final MonthClosingService monthClosingService;
    private final DomainEventStore domainEventStore;

    public SalesShipmentService(
            SalesShipmentRepository salesShipmentRepository,
            SalesShipmentInventoryService inventoryService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        this.salesShipmentRepository = salesShipmentRepository;
        this.inventoryService = inventoryService;
        this.monthClosingService = monthClosingService;
        this.domainEventStore = domainEventStore;
    }

    public List<SalesShipmentCandidateView> listCandidates(SalesShipmentCandidateCriteria criteria) {
        return salesShipmentRepository.findCandidates(criteria);
    }

    public List<SalesShipmentView> list(SalesShipmentListCriteria criteria) {
        return salesShipmentRepository.findAllActive(criteria);
    }

    public SalesShipmentView get(long id) {
        return salesShipmentRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("출고·납품을 찾을 수 없습니다: " + id));
    }

    public SalesShipmentView register(CreateSalesShipmentCommand command, String actorUserId) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("출고할 수주 라인을 1건 이상 선택하세요.");
        }
        LocalDate shipmentDate = command.shipmentDate() != null ? command.shipmentDate() : LocalDate.now();
        monthClosingService.assertTransactionOpen(shipmentDate);

        Set<Long> seenLineIds = new HashSet<>();
        Map<Long, SalesOrderLineShipmentContext> contextByLineId = new LinkedHashMap<>();
        Map<Long, List<CreateSalesShipmentLineCommand>> linesByPartner = new LinkedHashMap<>();

        for (CreateSalesShipmentLineCommand lineCommand : command.lines()) {
            if (!seenLineIds.add(lineCommand.salesOrderLineId())) {
                throw new IllegalArgumentException("중복된 수주 라인입니다: " + lineCommand.salesOrderLineId());
            }
            SalesOrderLineShipmentContext context = salesShipmentRepository.findOrderLineContext(
                    lineCommand.salesOrderLineId()
            );
            validateLine(lineCommand, context);
            contextByLineId.put(lineCommand.salesOrderLineId(), context);
            linesByPartner.computeIfAbsent(context.partnerId(), ignored -> new ArrayList<>()).add(lineCommand);
        }

        SalesShipmentView lastShipment = null;
        for (Map.Entry<Long, List<CreateSalesShipmentLineCommand>> entry : linesByPartner.entrySet()) {
            lastShipment = registerForPartner(
                    shipmentDate,
                    entry.getKey(),
                    entry.getValue(),
                    contextByLineId,
                    actorUserId
            );
        }
        return lastShipment;
    }

    private SalesShipmentView registerForPartner(
            LocalDate shipmentDate,
            long partnerId,
            List<CreateSalesShipmentLineCommand> lines,
            Map<Long, SalesOrderLineShipmentContext> contextByLineId,
            String actorUserId
    ) {
        Set<Long> orderIds = new HashSet<>();
        List<SalesShipmentLineSaveCommand> lineSaves = new ArrayList<>();

        for (CreateSalesShipmentLineCommand lineCommand : lines) {
            SalesOrderLineShipmentContext context = contextByLineId.get(lineCommand.salesOrderLineId());
            orderIds.add(context.salesOrderId());
            BigDecimal amount = lineAmount(lineCommand.shipmentQty(), context.unitPrice());
            lineSaves.add(new SalesShipmentLineSaveCommand(
                    lineCommand.salesOrderLineId(),
                    lineCommand.shipmentQty(),
                    context.itemId(),
                    context.unitPrice(),
                    amount
            ));
            inventoryService.assertSufficientSalesStock(
                    shipmentDate,
                    context.itemId(),
                    context.itemNo(),
                    lineCommand.shipmentQty()
            );
        }

        Long salesOrderId = orderIds.size() == 1 ? orderIds.iterator().next() : null;
        String shipmentNo = nextShipmentNo(shipmentDate);
        SalesShipmentView saved = salesShipmentRepository.save(
                new SalesShipmentSaveCommand(shipmentNo, partnerId, shipmentDate, salesOrderId, lineSaves),
                actorUserId
        );

        for (SalesShipmentLineView line : saved.lines()) {
            SalesOrderLineShipmentContext context = contextByLineId.get(line.salesOrderLineId());
            inventoryService.applyRegistration(
                    shipmentDate,
                    line.id(),
                    context.itemId(),
                    context.itemNo(),
                    line.shipmentQty(),
                    line.amount(),
                    actorUserId
            );
            salesShipmentRepository.addShippedQty(line.salesOrderLineId(), line.shipmentQty(), actorUserId);
            salesShipmentRepository.refreshLineDeliveryStatus(line.salesOrderLineId(), actorUserId);
        }

        appendEvent(EventTypes.SALES_SHIPMENT_REGISTERED, saved.id(), actorUserId, saved.shipmentNo());
        return salesShipmentRepository.findActiveIssuedById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        SalesShipmentView shipment = salesShipmentRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("출고·납품을 찾을 수 없습니다: " + id));
        if (!shipment.cancelable()) {
            throw new IllegalArgumentException("취소할 수 없는 출고·납품입니다.");
        }
        monthClosingService.assertTransactionOpen(shipment.shipmentDate());

        for (SalesShipmentLineView line : shipment.lines()) {
            SalesOrderLineShipmentContext context = salesShipmentRepository.findOrderLineContext(line.salesOrderLineId());
            inventoryService.applyCancellation(
                    shipment.shipmentDate(),
                    line.id(),
                    context.itemId(),
                    line.shipmentQty(),
                    line.amount(),
                    actorUserId
            );
            salesShipmentRepository.subtractShippedQty(line.salesOrderLineId(), line.shipmentQty(), actorUserId);
            salesShipmentRepository.refreshLineDeliveryStatus(line.salesOrderLineId(), actorUserId);
        }

        salesShipmentRepository.cancelById(id, actorUserId);
        appendEvent(EventTypes.SALES_SHIPMENT_CANCELLED, id, actorUserId, shipment.shipmentNo());
    }

    private void validateLine(CreateSalesShipmentLineCommand line, SalesOrderLineShipmentContext context) {
        if (context.orderStatus() == SalesOrderStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 수주는 출고할 수 없습니다: " + context.orderNo());
        }
        if (line.shipmentQty().compareTo(context.remainingQty()) > 0) {
            throw new IllegalArgumentException(
                    "출고 수량이 잔량을 초과합니다. 품목=" + context.itemNo()
                            + ", 잔량=" + context.remainingQty().stripTrailingZeros().toPlainString()
            );
        }
    }

    static BigDecimal lineAmount(BigDecimal qty, BigDecimal unitPrice) {
        BigDecimal price = unitPrice != null ? unitPrice : BigDecimal.ZERO;
        return qty.multiply(price).setScale(2, RoundingMode.HALF_UP);
    }

    private String nextShipmentNo(LocalDate shipmentDate) {
        String prefix = "SS-" + shipmentDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = salesShipmentRepository.countByShipmentNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private void appendEvent(String eventType, long shipmentId, String actorUserId, String shipmentNo) {
        String payload = """
                {"salesShipmentId":%d,"shipmentNo":"%s"}
                """.formatted(shipmentId, shipmentNo).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.SALES_SHIPMENT,
                String.valueOf(shipmentId),
                actorUserId,
                payload
        ));
    }
}
