package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.sales.SalesHistorySourceType;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class SalesRevenueService {

    private final SalesRevenueRepository salesRevenueRepository;
    private final SalesHistoryRepository salesHistoryRepository;
    private final SalesRevenueInventoryService inventoryService;
    private final ItemRepository itemRepository;
    private final PartnerLedgerService partnerLedgerService;
    private final FiscalCalendarService fiscalCalendarService;
    private final MonthClosingService monthClosingService;
    private final DomainEventStore domainEventStore;

    public SalesRevenueService(
            SalesRevenueRepository salesRevenueRepository,
            SalesHistoryRepository salesHistoryRepository,
            SalesRevenueInventoryService inventoryService,
            ItemRepository itemRepository,
            PartnerLedgerService partnerLedgerService,
            FiscalCalendarService fiscalCalendarService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        this.salesRevenueRepository = salesRevenueRepository;
        this.salesHistoryRepository = salesHistoryRepository;
        this.inventoryService = inventoryService;
        this.itemRepository = itemRepository;
        this.partnerLedgerService = partnerLedgerService;
        this.fiscalCalendarService = fiscalCalendarService;
        this.monthClosingService = monthClosingService;
        this.domainEventStore = domainEventStore;
    }

    public List<SalesRevenueCandidateView> listCandidates(SalesRevenueCandidateCriteria criteria) {
        return salesRevenueRepository.findCandidates(criteria);
    }

    public List<SalesRevenueView> list(SalesRevenueListCriteria criteria) {
        return salesRevenueRepository.findAllActive(criteria);
    }

    public SalesRevenueView register(CreateSalesRevenueCommand command, String actorUserId) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("매출 등록할 출고 라인을 1건 이상 선택하세요.");
        }
        LocalDate revenueDate = command.revenueDate() != null ? command.revenueDate() : LocalDate.now();
        monthClosingService.assertTransactionOpen(revenueDate);

        Set<Long> seenLineIds = new HashSet<>();
        Map<Long, SalesShipmentLineRevenueContext> contextByLineId = new LinkedHashMap<>();
        Map<Long, List<CreateSalesRevenueLineCommand>> linesByPartner = new LinkedHashMap<>();

        for (CreateSalesRevenueLineCommand lineCommand : command.lines()) {
            if (!seenLineIds.add(lineCommand.salesShipmentLineId())) {
                throw new IllegalArgumentException("중복된 출고 라인입니다: " + lineCommand.salesShipmentLineId());
            }
            SalesShipmentLineRevenueContext context = salesRevenueRepository.findShipmentLineContext(
                    lineCommand.salesShipmentLineId()
            );
            validateLine(lineCommand, context);
            contextByLineId.put(lineCommand.salesShipmentLineId(), context);
            linesByPartner.computeIfAbsent(context.partnerId(), ignored -> new ArrayList<>()).add(lineCommand);
        }

        SalesRevenueView lastRevenue = null;
        for (Map.Entry<Long, List<CreateSalesRevenueLineCommand>> entry : linesByPartner.entrySet()) {
            lastRevenue = registerForPartner(
                    revenueDate,
                    entry.getKey(),
                    entry.getValue(),
                    contextByLineId,
                    actorUserId
            );
        }
        return lastRevenue;
    }

    private SalesRevenueView registerForPartner(
            LocalDate revenueDate,
            long partnerId,
            List<CreateSalesRevenueLineCommand> lines,
            Map<Long, SalesShipmentLineRevenueContext> contextByLineId,
            String actorUserId
    ) {
        Set<Long> shipmentIds = new HashSet<>();
        List<SalesRevenueLineSaveCommand> lineSaves = new ArrayList<>();

        for (CreateSalesRevenueLineCommand lineCommand : lines) {
            SalesShipmentLineRevenueContext context = contextByLineId.get(lineCommand.salesShipmentLineId());
            shipmentIds.add(context.shipmentId());
            BigDecimal amount = lineAmount(lineCommand.revenueQty(), context.unitPrice());
            Long lotId = resolveLotId(context.itemId(), lineCommand.lotId());
            lineSaves.add(new SalesRevenueLineSaveCommand(
                    lineCommand.salesShipmentLineId(),
                    lineCommand.revenueQty(),
                    context.itemId(),
                    context.unitPrice(),
                    amount,
                    lotId
            ));
            inventoryService.assertSufficientDeliveryStock(
                    revenueDate,
                    context.itemId(),
                    context.itemNo(),
                    lineCommand.revenueQty()
            );
        }

        Long salesShipmentId = shipmentIds.size() == 1 ? shipmentIds.iterator().next() : null;
        String revenueNo = nextRevenueNo(revenueDate);
        SalesRevenueView saved = salesRevenueRepository.save(
                new SalesRevenueSaveCommand(revenueNo, partnerId, revenueDate, salesShipmentId, lineSaves),
                actorUserId
        );

        FiscalPeriod period = fiscalCalendarService.resolvePeriod(revenueDate);
        for (SalesRevenueLineView line : saved.lines()) {
            SalesShipmentLineRevenueContext context = contextByLineId.get(line.salesShipmentLineId());
            inventoryService.applyRegistration(
                    revenueDate,
                    line.id(),
                    context.itemId(),
                    context.itemNo(),
                    line.revenueQty(),
                    line.amount(),
                    line.lotId(),
                    actorUserId
            );
            salesRevenueRepository.addInvoicedQty(line.salesShipmentLineId(), line.revenueQty(), actorUserId);
            salesHistoryRepository.save(new SalesHistoryCommand(
                    partnerId,
                    context.itemId(),
                    line.revenueQty(),
                    line.unitPrice(),
                    line.amount(),
                    revenueDate,
                    SalesHistorySourceType.SALES_REVENUE,
                    line.id(),
                    period.fiscalYear(),
                    period.fiscalMonth(),
                    actorUserId
            ));
            partnerLedgerService.addSalesAmount(partnerId, revenueDate, line.amount(), actorUserId);
        }

        appendEvent(EventTypes.SALES_REVENUE_REGISTERED, saved.id(), actorUserId, saved.revenueNo());
        return salesRevenueRepository.findActiveIssuedById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        SalesRevenueView revenue = salesRevenueRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("매출을 찾을 수 없습니다: " + id));
        if (!revenue.cancelable()) {
            throw new IllegalArgumentException("취소할 수 없는 매출입니다.");
        }
        monthClosingService.assertTransactionOpen(revenue.revenueDate());

        for (SalesRevenueLineView line : revenue.lines()) {
            SalesShipmentLineRevenueContext context = salesRevenueRepository.findShipmentLineContext(
                    line.salesShipmentLineId()
            );
            inventoryService.applyCancellation(
                    revenue.revenueDate(),
                    line.id(),
                    context.itemId(),
                    line.revenueQty(),
                    line.amount(),
                    line.lotId(),
                    actorUserId
            );
            salesRevenueRepository.subtractInvoicedQty(line.salesShipmentLineId(), line.revenueQty(), actorUserId);
            salesHistoryRepository.deactivateBySource(SalesHistorySourceType.SALES_REVENUE, line.id(), actorUserId);
            partnerLedgerService.subtractSalesAmount(revenue.partnerId(), revenue.revenueDate(), line.amount(), actorUserId);
        }

        salesRevenueRepository.cancelById(id, actorUserId);
        appendEvent(EventTypes.SALES_REVENUE_CANCELLED, id, actorUserId, revenue.revenueNo());
    }

    private void validateLine(CreateSalesRevenueLineCommand line, SalesShipmentLineRevenueContext context) {
        if (line.revenueQty().compareTo(context.remainingQty()) > 0) {
            throw new IllegalArgumentException(
                    "매출 수량이 잔량을 초과합니다. 품목=" + context.itemNo()
                            + ", 잔량=" + context.remainingQty().stripTrailingZeros().toPlainString()
            );
        }
        resolveLotId(context.itemId(), line.lotId());
    }

    private Long resolveLotId(long itemId, Long lotId) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));
        if (item.lotTracked()) {
            if (lotId == null) {
                throw new IllegalArgumentException("Lot 추적 품목은 Lot를 선택해야 합니다: " + item.itemNo());
            }
            return lotId;
        }
        if (lotId != null) {
            throw new IllegalArgumentException("Lot 비추적 품목은 Lot를 지정할 수 없습니다: " + item.itemNo());
        }
        return null;
    }

    static BigDecimal lineAmount(BigDecimal qty, BigDecimal unitPrice) {
        BigDecimal price = unitPrice != null ? unitPrice : BigDecimal.ZERO;
        return qty.multiply(price).setScale(2, RoundingMode.HALF_UP);
    }

    private String nextRevenueNo(LocalDate revenueDate) {
        String prefix = "SR-" + revenueDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = salesRevenueRepository.countByRevenueNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private void appendEvent(String eventType, long revenueId, String actorUserId, String revenueNo) {
        String payload = """
                {"salesRevenueId":%d,"revenueNo":"%s"}
                """.formatted(revenueId, revenueNo).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.SALES_REVENUE,
                String.valueOf(revenueId),
                actorUserId,
                payload
        ));
    }
}
