package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.outsource.OutsourceHistoryRecord;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.application.quality.QualityInspectionRepository;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import com.shindong.smartmanager.domain.outsource.OutsourcingReceiptStatus;
import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
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

public class OutsourcingReceiptService {

    private final OutsourcingReceiptRepository receiptRepository;
    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final OutsourceHistoryRepository outsourceHistoryRepository;
    private final QualityInspectionRepository qualityInspectionRepository;
    private final OutsourcingReceiptInventoryService inventoryService;
    private final PartnerLedgerService partnerLedgerService;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;
    private final DomainEventStore domainEventStore;

    public OutsourcingReceiptService(
            OutsourcingReceiptRepository receiptRepository,
            OutsourcingOrderRepository outsourcingOrderRepository,
            OutsourceHistoryRepository outsourceHistoryRepository,
            QualityInspectionRepository qualityInspectionRepository,
            OutsourcingReceiptInventoryService inventoryService,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService,
            DomainEventStore domainEventStore
    ) {
        this.receiptRepository = receiptRepository;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.outsourceHistoryRepository = outsourceHistoryRepository;
        this.qualityInspectionRepository = qualityInspectionRepository;
        this.inventoryService = inventoryService;
        this.partnerLedgerService = partnerLedgerService;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
        this.domainEventStore = domainEventStore;
    }

    public List<OutsourcingReceiptCandidateView> listCandidates(OutsourcingReceiptCandidateCriteria criteria) {
        return receiptRepository.findReceiptCandidates(criteria);
    }

    public List<OutsourcingReceiptView> list(OutsourcingReceiptListCriteria criteria) {
        return receiptRepository.findAllActive(criteria);
    }

    public OutsourcingReceiptView get(long id) {
        return receiptRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("외주입고를 찾을 수 없습니다: " + id));
    }

    public OutsourcingReceiptView register(CreateOutsourcingReceiptCommand command, String actorUserId) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("입고 라인이 없습니다.");
        }
        monthClosingService.assertTransactionOpen(command.receiptDate());

        FiscalPeriod fiscalPeriod = fiscalCalendarService.resolvePeriod(
                command.receiptDate(),
                command.fiscalYear(),
                command.fiscalMonth()
        );
        monthClosingService.assertPeriodOpen(fiscalPeriod.fiscalYear(), fiscalPeriod.fiscalMonth());

        Map<Long, List<CreateOutsourcingReceiptLineCommand>> linesByPartner = new LinkedHashMap<>();
        Map<Long, OutsourcingOrderLineReceiptContext> contextByLineId = new LinkedHashMap<>();
        Set<Long> affectedOrderIds = new HashSet<>();

        for (CreateOutsourcingReceiptLineCommand line : command.lines()) {
            OutsourcingOrderLineReceiptContext ctx = receiptRepository.findOrderLineContext(line.outsourcingOrderLineId());
            validateReceiptLine(line, ctx);
            contextByLineId.put(line.outsourcingOrderLineId(), ctx);
            linesByPartner.computeIfAbsent(ctx.partnerId(), ignored -> new ArrayList<>()).add(line);
            affectedOrderIds.add(ctx.outsourcingOrderId());
        }

        OutsourcingReceiptView lastReceipt = null;
        for (Map.Entry<Long, List<CreateOutsourcingReceiptLineCommand>> entry : linesByPartner.entrySet()) {
            lastReceipt = registerForPartner(
                    command.receiptDate(),
                    fiscalPeriod,
                    entry.getKey(),
                    entry.getValue(),
                    contextByLineId,
                    actorUserId
            );
        }

        for (long orderId : affectedOrderIds) {
            outsourcingOrderRepository.refreshOrderStatus(orderId, actorUserId);
        }
        return lastReceipt;
    }

    public void cancel(long receiptId, String actorUserId) {
        OutsourcingReceiptView receipt = get(receiptId);
        if (receipt.status() == OutsourcingReceiptStatus.CANCELLED) {
            return;
        }
        monthClosingService.assertTransactionOpen(receipt.receiptDate());

        Set<Long> affectedOrderIds = new HashSet<>();

        for (OutsourcingReceiptLineView line : receipt.lines()) {
            OutsourcingOrderLineReceiptContext ctx = receiptRepository.findOrderLineContext(line.outsourcingOrderLineId());
            affectedOrderIds.add(ctx.outsourcingOrderId());

            OutsourcingOrderView order = outsourcingOrderRepository.findActiveByOrderLineId(line.outsourcingOrderLineId())
                    .orElseThrow(() -> new IllegalStateException("외주발주를 찾을 수 없습니다."));
            OutsourcingOrderLineView orderLine = order.lines().stream()
                    .filter(row -> row.id() == line.outsourcingOrderLineId())
                    .findFirst()
                    .orElseThrow(() -> new IllegalStateException("외주발주 라인을 찾을 수 없습니다."));

            BigDecimal postedQty = line.postedQty() != null ? line.postedQty() : BigDecimal.ZERO;
            if (postedQty.compareTo(BigDecimal.ZERO) > 0) {
                reverseStockAndLedger(
                        line,
                        ctx,
                        order,
                        orderLine,
                        receipt.partnerId(),
                        receipt.receiptDate(),
                        postedQty,
                        OutsourceHistorySourceType.OUTSOURCING_RECEIPT,
                        line.id(),
                        actorUserId
                );
                receiptRepository.subtractReceivedQty(ctx.outsourcingOrderLineId(), postedQty, actorUserId);
            }

            qualityInspectionRepository.findActiveByReceiptLineId(line.id()).ifPresent(inspection -> {
                if (inspection.status() == QualityInspectionStatus.PENDING) {
                    qualityInspectionRepository.cancelByReceiptLineId(line.id(), actorUserId);
                    receiptRepository.releaseWaitingInspectionQty(
                            ctx.outsourcingOrderLineId(), line.receiptQty(), actorUserId);
                } else if (inspection.status() == QualityInspectionStatus.COMPLETED
                        && inspection.passedQty().compareTo(BigDecimal.ZERO) > 0
                        && postedQty.compareTo(BigDecimal.ZERO) == 0) {
                    reverseStockAndLedger(
                            line,
                            ctx,
                            order,
                            orderLine,
                            receipt.partnerId(),
                            receipt.receiptDate(),
                            inspection.requestQty(),
                            inspection.passedQty(),
                            OutsourceHistorySourceType.QUALITY_INSPECTION,
                            inspection.id(),
                            actorUserId
                    );
                    receiptRepository.subtractReceivedQty(ctx.outsourcingOrderLineId(), inspection.passedQty(), actorUserId);
                    qualityInspectionRepository.cancelByReceiptLineId(line.id(), actorUserId);
                }
            });
        }

        receiptRepository.cancelReceipt(receiptId, actorUserId);

        for (long orderId : affectedOrderIds) {
            outsourcingOrderRepository.refreshOrderStatus(orderId, actorUserId);
        }
    }

    public void applyStockAndLedger(
            OutsourcingReceiptLineView receiptLine,
            OutsourcingOrderLineReceiptContext ctx,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            long partnerId,
            LocalDate movementDate,
            BigDecimal qty,
            OutsourceHistorySourceType historySourceType,
            long historySourceId,
            String actorUserId
    ) {
        applyStockAndLedger(
                receiptLine,
                ctx,
                order,
                orderLine,
                partnerId,
                movementDate,
                qty,
                qty,
                historySourceType,
                historySourceId,
                null,
                actorUserId
        );
    }

    public void applyStockAndLedger(
            OutsourcingReceiptLineView receiptLine,
            OutsourcingOrderLineReceiptContext ctx,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            long partnerId,
            LocalDate movementDate,
            BigDecimal qty,
            OutsourceHistorySourceType historySourceType,
            long historySourceId,
            FiscalPeriod fiscalPeriodOverride,
            String actorUserId
    ) {
        applyStockAndLedger(
                receiptLine,
                ctx,
                order,
                orderLine,
                partnerId,
                movementDate,
                qty,
                qty,
                historySourceType,
                historySourceId,
                fiscalPeriodOverride,
                actorUserId
        );
    }

    public void applyStockAndLedger(
            OutsourcingReceiptLineView receiptLine,
            OutsourcingOrderLineReceiptContext ctx,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            long partnerId,
            LocalDate movementDate,
            BigDecimal outsourceDecreaseQty,
            BigDecimal inboundQty,
            OutsourceHistorySourceType historySourceType,
            long historySourceId,
            FiscalPeriod fiscalPeriodOverride,
            String actorUserId
    ) {
        BigDecimal amount = lineAmount(inboundQty, ctx.unitPrice());

        inventoryService.applyRegistration(
                movementDate,
                receiptLine.id(),
                partnerId,
                order,
                orderLine,
                outsourceDecreaseQty,
                inboundQty,
                amount,
                actorUserId
        );

        FiscalPeriod period = fiscalPeriodOverride != null
                ? fiscalPeriodOverride
                : fiscalCalendarService.resolvePeriod(movementDate);
        outsourceHistoryRepository.save(new OutsourceHistoryCommand(
                partnerId,
                ctx.itemId(),
                inboundQty,
                ctx.unitPrice(),
                amount,
                movementDate,
                historySourceType,
                historySourceType == OutsourceHistorySourceType.OUTSOURCING_RECEIPT
                        ? receiptLine.id()
                        : historySourceId,
                period.fiscalYear(),
                period.fiscalMonth(),
                actorUserId
        ));

        // 지급 확정은 승인처리 화면에서 반영 (partner_ledger 미갱신)
    }

    public void reverseStockAndLedger(
            OutsourcingReceiptLineView receiptLine,
            OutsourcingOrderLineReceiptContext ctx,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            long partnerId,
            LocalDate movementDate,
            BigDecimal qty,
            OutsourceHistorySourceType historySourceType,
            long historySourceId,
            String actorUserId
    ) {
        reverseStockAndLedger(
                receiptLine,
                ctx,
                order,
                orderLine,
                partnerId,
                movementDate,
                qty,
                qty,
                historySourceType,
                historySourceId,
                actorUserId
        );
    }

    public void reverseStockAndLedger(
            OutsourcingReceiptLineView receiptLine,
            OutsourcingOrderLineReceiptContext ctx,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            long partnerId,
            LocalDate movementDate,
            BigDecimal outsourceDecreaseQty,
            BigDecimal inboundQty,
            OutsourceHistorySourceType historySourceType,
            long historySourceId,
            String actorUserId
    ) {
        BigDecimal amount = lineAmount(inboundQty, ctx.unitPrice());

        inventoryService.applyCancellation(
                movementDate,
                receiptLine.id(),
                partnerId,
                order,
                orderLine,
                outsourceDecreaseQty,
                inboundQty,
                amount,
                actorUserId
        );

        List<OutsourceHistoryRecord> histories = outsourceHistoryRepository.findActiveBySource(
                historySourceType, historySourceId);
        outsourceHistoryRepository.deactivateBySource(historySourceType, historySourceId, actorUserId);

        for (OutsourceHistoryRecord history : histories) {
            if (history.approvalStatus() == PayableApprovalStatus.APPROVED) {
                partnerLedgerService.subtractPurchaseAmount(
                        partnerId, history.historyDate(), history.amount(), actorUserId);
            }
        }
    }

    private OutsourcingReceiptView registerForPartner(
            LocalDate receiptDate,
            FiscalPeriod fiscalPeriod,
            long partnerId,
            List<CreateOutsourcingReceiptLineCommand> lines,
            Map<Long, OutsourcingOrderLineReceiptContext> contextByLineId,
            String actorUserId
    ) {
        Set<Long> orderIds = new HashSet<>();
        List<OutsourcingOrderLineReceiptContext> contexts = new ArrayList<>();
        for (CreateOutsourcingReceiptLineCommand line : lines) {
            OutsourcingOrderLineReceiptContext ctx = contextByLineId.get(line.outsourcingOrderLineId());
            orderIds.add(ctx.outsourcingOrderId());
            contexts.add(ctx);
        }
        Long outsourcingOrderId = orderIds.size() == 1 ? orderIds.iterator().next() : null;

        List<OutsourcingReceiptLineSaveCommand> lineSaves = new ArrayList<>();
        for (CreateOutsourcingReceiptLineCommand line : lines) {
            OutsourcingOrderLineReceiptContext ctx = contextByLineId.get(line.outsourcingOrderLineId());
            BigDecimal amount = lineAmount(line.receiptQty(), ctx.unitPrice());
            lineSaves.add(new OutsourcingReceiptLineSaveCommand(
                    line.outsourcingOrderLineId(),
                    ctx.itemId(),
                    line.receiptQty(),
                    BigDecimal.ZERO,
                    ctx.unitPrice(),
                    amount
            ));
        }

        String receiptNo = nextReceiptNo(receiptDate);
        long receiptId = receiptRepository.saveReceipt(new OutsourcingReceiptSaveCommand(
                receiptNo,
                partnerId,
                receiptDate,
                outsourcingOrderId,
                OutsourcingReceiptStatus.REGISTERED,
                lineSaves
        ), actorUserId);

        OutsourcingReceiptView saved = get(receiptId);
        List<OutsourcingReceiptLineView> resultLines = new ArrayList<>();

        for (int i = 0; i < saved.lines().size(); i++) {
            OutsourcingReceiptLineView receiptLine = saved.lines().get(i);
            OutsourcingOrderLineReceiptContext ctx = contexts.get(i);
            CheckDistinction checkDistinction = resolveCheckDistinction(ctx.checkDistinction());

            OutsourcingOrderView order = outsourcingOrderRepository.findActiveByOrderLineId(ctx.outsourcingOrderLineId())
                    .orElseThrow(() -> new IllegalStateException("외주발주를 찾을 수 없습니다."));
            OutsourcingOrderLineView orderLine = order.lines().stream()
                    .filter(row -> row.id() == ctx.outsourcingOrderLineId())
                    .findFirst()
                    .orElseThrow(() -> new IllegalStateException("외주발주 라인을 찾을 수 없습니다."));

            if (checkDistinction == CheckDistinction.INSPECTION) {
                long inspectionId = qualityInspectionRepository.createPending(
                        QualityInspectionSourceType.OUTSOURCE,
                        receiptLine.id(),
                        ctx.itemId(),
                        partnerId,
                        receiptLine.receiptQty(),
                        actorUserId
                );
                receiptRepository.addWaitingInspectionQty(ctx.outsourcingOrderLineId(), receiptLine.receiptQty(), actorUserId);
                resultLines.add(new OutsourcingReceiptLineView(
                        receiptLine.id(),
                        receiptLine.lineNo(),
                        receiptLine.outsourcingOrderLineId(),
                        receiptLine.itemId(),
                        receiptLine.itemNo(),
                        receiptLine.itemName(),
                        receiptLine.receiptQty(),
                        receiptLine.postedQty(),
                        receiptLine.unitPrice(),
                        receiptLine.amount(),
                        inspectionId,
                        false
                ));
            } else {
                inventoryService.assertSufficientOutsourceStock(
                        receiptDate, partnerId, order, orderLine, receiptLine.receiptQty());
                applyStockAndLedger(
                        receiptLine,
                        ctx,
                        order,
                        orderLine,
                        partnerId,
                        receiptDate,
                        receiptLine.receiptQty(),
                        OutsourceHistorySourceType.OUTSOURCING_RECEIPT,
                        receiptLine.id(),
                        fiscalPeriod,
                        actorUserId
                );
                receiptRepository.addReceivedQty(ctx.outsourcingOrderLineId(), receiptLine.receiptQty(), actorUserId);
                receiptRepository.updateReceiptLinePostedQty(receiptLine.id(), receiptLine.receiptQty(), actorUserId);
                resultLines.add(new OutsourcingReceiptLineView(
                        receiptLine.id(),
                        receiptLine.lineNo(),
                        receiptLine.outsourcingOrderLineId(),
                        receiptLine.itemId(),
                        receiptLine.itemNo(),
                        receiptLine.itemName(),
                        receiptLine.receiptQty(),
                        receiptLine.receiptQty(),
                        receiptLine.unitPrice(),
                        receiptLine.amount(),
                        null,
                        true
                ));
            }
        }

        receiptRepository.updateReceiptStatus(receiptId, actorUserId);
        appendEvent(EventTypes.OUTSOURCING_RECEIPT_REGISTERED, receiptId, actorUserId, receiptNo);

        OutsourcingReceiptView refreshed = get(receiptId);
        return new OutsourcingReceiptView(
                refreshed.id(),
                refreshed.receiptNo(),
                refreshed.partnerId(),
                refreshed.partnerName(),
                refreshed.receiptDate(),
                refreshed.outsourcingOrderId(),
                refreshed.status(),
                refreshed.createdAt(),
                refreshed.createdBy(),
                resultLines
        );
    }

    private void validateReceiptLine(
            CreateOutsourcingReceiptLineCommand line,
            OutsourcingOrderLineReceiptContext ctx
    ) {
        if (ctx.orderStatus() == OutsourcingOrderStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 발주 라인은 입고할 수 없습니다: lineId=" + line.outsourcingOrderLineId());
        }
        if (ctx.shippedQty().compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("출고 실적이 없는 발주 라인은 입고할 수 없습니다.");
        }
        if (line.receiptQty().compareTo(ctx.remainQty()) > 0) {
            throw new IllegalArgumentException(
                    "입고 수량이 잔량을 초과합니다. 품목=" + ctx.itemNo()
                            + ", 잔량=" + ctx.remainQty().stripTrailingZeros().toPlainString()
            );
        }
    }

    static CheckDistinction resolveCheckDistinction(String value) {
        if (value == null || value.isBlank()) {
            return CheckDistinction.NONE;
        }
        return CheckDistinction.valueOf(value);
    }

    static BigDecimal lineAmount(BigDecimal qty, BigDecimal unitPrice) {
        BigDecimal price = unitPrice != null ? unitPrice : BigDecimal.ZERO;
        return qty.multiply(price).setScale(2, RoundingMode.HALF_UP);
    }

    private String nextReceiptNo(LocalDate receiptDate) {
        String prefix = "OR-" + receiptDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = receiptRepository.countByReceiptNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private void appendEvent(String eventType, long receiptId, String actorUserId, String receiptNo) {
        String payload = """
                {"outsourcingReceiptId":%d,"receiptNo":"%s"}
                """.formatted(receiptId, receiptNo).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.OUTSOURCING_RECEIPT,
                String.valueOf(receiptId),
                actorUserId,
                payload
        ));
    }
}
