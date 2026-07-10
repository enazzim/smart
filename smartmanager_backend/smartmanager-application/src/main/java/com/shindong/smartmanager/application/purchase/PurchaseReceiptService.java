package com.shindong.smartmanager.application.purchase;



import com.shindong.smartmanager.application.closing.FiscalCalendarService;

import com.shindong.smartmanager.application.closing.FiscalPeriod;

import com.shindong.smartmanager.application.closing.MonthClosingService;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;

import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;

import com.shindong.smartmanager.application.ledger.PartnerLedgerService;

import com.shindong.smartmanager.application.quality.QualityInspectionRepository;

import com.shindong.smartmanager.application.quality.QualityInspectionView;

import com.shindong.smartmanager.domain.inventory.StockMovementType;

import com.shindong.smartmanager.domain.item.CheckDistinction;

import com.shindong.smartmanager.domain.item.PropertyClassification;

import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;

import com.shindong.smartmanager.domain.purchase.PurchaseReceiptStatus;

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



public class PurchaseReceiptService {



    private final PurchaseReceiptRepository receiptRepository;

    private final PurchaseHistoryRepository purchaseHistoryRepository;

    private final QualityInspectionRepository qualityInspectionRepository;

    private final InventoryBalanceService inventoryBalanceService;

    private final PartnerLedgerService partnerLedgerService;

    private final MonthClosingService monthClosingService;

    private final FiscalCalendarService fiscalCalendarService;



    public PurchaseReceiptService(

            PurchaseReceiptRepository receiptRepository,

            PurchaseHistoryRepository purchaseHistoryRepository,

            QualityInspectionRepository qualityInspectionRepository,

            InventoryBalanceService inventoryBalanceService,

            PartnerLedgerService partnerLedgerService,

            MonthClosingService monthClosingService,

            FiscalCalendarService fiscalCalendarService

    ) {

        this.receiptRepository = receiptRepository;

        this.purchaseHistoryRepository = purchaseHistoryRepository;

        this.qualityInspectionRepository = qualityInspectionRepository;

        this.inventoryBalanceService = inventoryBalanceService;

        this.partnerLedgerService = partnerLedgerService;

        this.monthClosingService = monthClosingService;

        this.fiscalCalendarService = fiscalCalendarService;

    }



    public List<PurchaseReceiptCandidateView> listCandidates(PurchaseReceiptCandidateCriteria criteria) {

        return receiptRepository.findReceiptCandidates(criteria);

    }



    public List<PurchaseReceiptView> list(PurchaseReceiptListCriteria criteria) {

        return receiptRepository.findAllActive(criteria);

    }



    public PurchaseReceiptView get(long id) {

        return receiptRepository.findActiveById(id)

                .orElseThrow(() -> new IllegalArgumentException("구매입고를 찾을 수 없습니다: " + id));

    }



    public PurchaseReceiptView register(CreatePurchaseReceiptCommand command, String actorUserId) {

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



        Map<Long, List<CreatePurchaseReceiptLineCommand>> linesByPartner = new LinkedHashMap<>();

        Map<Long, PurchaseOrderLineReceiptContext> contextByLineId = new LinkedHashMap<>();

        Set<Long> affectedOrderIds = new HashSet<>();



        for (CreatePurchaseReceiptLineCommand line : command.lines()) {

            PurchaseOrderLineReceiptContext ctx = receiptRepository.findOrderLineContext(line.purchaseOrderLineId());

            validateReceiptLine(line, ctx);

            contextByLineId.put(line.purchaseOrderLineId(), ctx);

            linesByPartner.computeIfAbsent(ctx.partnerId(), ignored -> new ArrayList<>()).add(line);

            affectedOrderIds.add(ctx.purchaseOrderId());

        }



        PurchaseReceiptView lastReceipt = null;

        for (Map.Entry<Long, List<CreatePurchaseReceiptLineCommand>> entry : linesByPartner.entrySet()) {

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

            receiptRepository.refreshPurchaseOrderReceiptStatus(orderId, actorUserId);

        }

        return lastReceipt;

    }



    private PurchaseReceiptView registerForPartner(

            LocalDate receiptDate,

            FiscalPeriod fiscalPeriod,

            long partnerId,

            List<CreatePurchaseReceiptLineCommand> lines,

            Map<Long, PurchaseOrderLineReceiptContext> contextByLineId,

            String actorUserId

    ) {

        Set<Long> orderIds = new HashSet<>();

        List<PurchaseOrderLineReceiptContext> contexts = new ArrayList<>();

        for (CreatePurchaseReceiptLineCommand line : lines) {

            PurchaseOrderLineReceiptContext ctx = contextByLineId.get(line.purchaseOrderLineId());

            orderIds.add(ctx.purchaseOrderId());

            contexts.add(ctx);

        }

        Long purchaseOrderId = orderIds.size() == 1 ? orderIds.iterator().next() : null;



        List<PurchaseReceiptLineSaveCommand> lineSaves = new ArrayList<>();

        for (CreatePurchaseReceiptLineCommand line : lines) {

            PurchaseOrderLineReceiptContext ctx = contextByLineId.get(line.purchaseOrderLineId());

            BigDecimal amount = lineAmount(line.receiptQty(), ctx.unitPrice());

            lineSaves.add(new PurchaseReceiptLineSaveCommand(

                    line.purchaseOrderLineId(),

                    ctx.itemId(),

                    line.receiptQty(),

                    BigDecimal.ZERO,

                    ctx.unitPrice(),

                    amount

            ));

        }



        String receiptNo = nextReceiptNo(receiptDate);

        long receiptId = receiptRepository.saveReceipt(new PurchaseReceiptSaveCommand(

                receiptNo,

                partnerId,

                receiptDate,

                purchaseOrderId,

                PurchaseReceiptStatus.REGISTERED,

                lineSaves

        ), actorUserId);



        PurchaseReceiptView saved = get(receiptId);

        List<PurchaseReceiptLineView> resultLines = new ArrayList<>();



        for (int i = 0; i < saved.lines().size(); i++) {

            PurchaseReceiptLineView receiptLine = saved.lines().get(i);

            PurchaseOrderLineReceiptContext ctx = contexts.get(i);

            CheckDistinction checkDistinction = resolveCheckDistinction(ctx.checkDistinction());



            if (checkDistinction == CheckDistinction.INSPECTION) {

                long inspectionId = qualityInspectionRepository.createPending(

                        QualityInspectionSourceType.PURCHASE,

                        receiptLine.id(),

                        ctx.itemId(),

                        partnerId,

                        receiptLine.receiptQty(),

                        actorUserId

                );

                receiptRepository.addWaitingInspectionQty(ctx.purchaseOrderLineId(), receiptLine.receiptQty(), actorUserId);

                resultLines.add(new PurchaseReceiptLineView(

                        receiptLine.id(),

                        receiptLine.lineNo(),

                        receiptLine.purchaseOrderLineId(),

                        receiptLine.itemId(),

                        receiptLine.itemNum(),

                        receiptLine.itemName(),

                        receiptLine.receiptQty(),

                        receiptLine.postedQty(),

                        receiptLine.unitPrice(),

                        receiptLine.amount(),

                        inspectionId,

                        false

                ));

            } else {

                applyStockAndLedger(

                        receiptLine,

                        ctx,

                        partnerId,

                        receiptDate,

                        receiptLine.receiptQty(),

                        PurchaseHistorySourceType.PURCHASE_RECEIPT,

                        receiptLine.id(),

                        "PURCHASE_RECEIPT",

                        fiscalPeriod,

                        actorUserId

                );

                receiptRepository.addReceivedQty(ctx.purchaseOrderLineId(), receiptLine.receiptQty(), actorUserId);

                receiptRepository.updateReceiptLinePostedQty(receiptLine.id(), receiptLine.receiptQty(), actorUserId);

                resultLines.add(new PurchaseReceiptLineView(

                        receiptLine.id(),

                        receiptLine.lineNo(),

                        receiptLine.purchaseOrderLineId(),

                        receiptLine.itemId(),

                        receiptLine.itemNum(),

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

        PurchaseReceiptView refreshed = get(receiptId);

        return new PurchaseReceiptView(

                refreshed.id(),

                refreshed.receiptNo(),

                refreshed.partnerId(),

                refreshed.partnerName(),

                refreshed.receiptDate(),

                refreshed.purchaseOrderId(),

                refreshed.status(),

                refreshed.createdAt(),

                refreshed.createdBy(),

                resultLines

        );

    }



    private void validateReceiptLine(CreatePurchaseReceiptLineCommand line, PurchaseOrderLineReceiptContext ctx) {

        if (ctx.orderStatus() == PurchaseOrderStatus.CANCELLED) {

            throw new IllegalArgumentException("취소된 발주 라인은 입고할 수 없습니다: lineId=" + line.purchaseOrderLineId());

        }

        if (line.receiptQty().compareTo(ctx.remainQty()) > 0) {

            throw new IllegalArgumentException(

                    "입고 수량이 잔량을 초과합니다. 품목=" + ctx.itemNum()

                            + ", 잔량=" + ctx.remainQty().stripTrailingZeros().toPlainString()

            );

        }

    }



    public void cancel(long receiptId, String actorUserId) {

        PurchaseReceiptView receipt = get(receiptId);

        if (receipt.status() == PurchaseReceiptStatus.CANCELLED) {

            return;

        }

        monthClosingService.assertTransactionOpen(receipt.receiptDate());



        Set<Long> affectedOrderIds = new HashSet<>();



        for (PurchaseReceiptLineView line : receipt.lines()) {

            PurchaseOrderLineReceiptContext ctx = receiptRepository.findOrderLineContext(line.purchaseOrderLineId());

            affectedOrderIds.add(ctx.purchaseOrderId());



            BigDecimal postedQty = line.postedQty() != null ? line.postedQty() : BigDecimal.ZERO;

            if (postedQty.compareTo(BigDecimal.ZERO) > 0) {

                reverseStockAndLedger(

                        line,

                        ctx,

                        receipt.partnerId(),

                        receipt.receiptDate(),

                        postedQty,

                        PurchaseHistorySourceType.PURCHASE_RECEIPT,

                        line.id(),

                        "PURCHASE_RECEIPT_CANCEL",

                        actorUserId

                );

                receiptRepository.subtractReceivedQty(ctx.purchaseOrderLineId(), postedQty, actorUserId);

            }



            qualityInspectionRepository.findActiveByReceiptLineId(line.id()).ifPresent(inspection -> {

                if (inspection.status() == QualityInspectionStatus.PENDING) {

                    qualityInspectionRepository.cancelByReceiptLineId(line.id(), actorUserId);

                    receiptRepository.releaseWaitingInspectionQty(ctx.purchaseOrderLineId(), line.receiptQty(), actorUserId);

                } else if (inspection.status() == QualityInspectionStatus.COMPLETED

                        && inspection.passedQty().compareTo(BigDecimal.ZERO) > 0

                        && postedQty.compareTo(BigDecimal.ZERO) == 0) {

                    reverseStockAndLedger(

                            line,

                            ctx,

                            receipt.partnerId(),

                            receipt.receiptDate(),

                            inspection.passedQty(),

                            PurchaseHistorySourceType.QUALITY_INSPECTION,

                            inspection.id(),

                            "QUALITY_INSPECTION_CANCEL",

                            actorUserId

                    );

                    receiptRepository.subtractReceivedQty(ctx.purchaseOrderLineId(), inspection.passedQty(), actorUserId);

                    qualityInspectionRepository.cancelByReceiptLineId(line.id(), actorUserId);

                }

            });

        }



        receiptRepository.cancelReceipt(receiptId, actorUserId);



        for (long orderId : affectedOrderIds) {

            receiptRepository.refreshPurchaseOrderReceiptStatus(orderId, actorUserId);

        }

    }



    public void applyStockAndLedger(

            PurchaseReceiptLineView receiptLine,

            PurchaseOrderLineReceiptContext ctx,

            long partnerId,

            LocalDate movementDate,

            BigDecimal qty,

            PurchaseHistorySourceType historySourceType,

            long historySourceId,

            String movementReferenceType,

            String actorUserId

    ) {
        applyStockAndLedger(
                receiptLine,
                ctx,
                partnerId,
                movementDate,
                qty,
                historySourceType,
                historySourceId,
                movementReferenceType,
                null,
                actorUserId
        );
    }

    public void applyStockAndLedger(

            PurchaseReceiptLineView receiptLine,

            PurchaseOrderLineReceiptContext ctx,

            long partnerId,

            LocalDate movementDate,

            BigDecimal qty,

            PurchaseHistorySourceType historySourceType,

            long historySourceId,

            String movementReferenceType,

            FiscalPeriod fiscalPeriodOverride,

            String actorUserId

    ) {

        String locationCode = resolveLocationCode(ctx.propertyClassification());

        BigDecimal amount = lineAmount(qty, ctx.unitPrice());



        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(

                ctx.itemId(),

                locationCode,

                movementDate,

                StockMovementType.IN,

                qty,

                amount,

                movementReferenceType,

                historySourceType == PurchaseHistorySourceType.PURCHASE_RECEIPT

                        ? receiptLine.id()

                        : historySourceId,

                null,

                null,

                null,

                actorUserId

        ));



        FiscalPeriod period = fiscalPeriodOverride != null
                ? fiscalPeriodOverride
                : fiscalCalendarService.resolvePeriod(movementDate);

        purchaseHistoryRepository.save(new PurchaseHistoryCommand(

                partnerId,

                ctx.itemId(),

                null,

                qty,

                ctx.unitPrice(),

                amount,

                movementDate,

                historySourceType,

                historySourceType == PurchaseHistorySourceType.PURCHASE_RECEIPT

                        ? receiptLine.id()

                        : historySourceId,

                period.fiscalYear(),

                period.fiscalMonth(),

                actorUserId

        ));



        // 지급 확정은 승인처리 화면에서 반영 (partner_ledger 미갱신)

    }



    public void reverseStockAndLedger(

            PurchaseReceiptLineView receiptLine,

            PurchaseOrderLineReceiptContext ctx,

            long partnerId,

            LocalDate movementDate,

            BigDecimal qty,

            PurchaseHistorySourceType historySourceType,

            long historySourceId,

            String movementReferenceType,

            String actorUserId

    ) {

        String locationCode = resolveLocationCode(ctx.propertyClassification());

        BigDecimal amount = lineAmount(qty, ctx.unitPrice());



        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(

                ctx.itemId(),

                locationCode,

                movementDate,

                StockMovementType.OUT,

                qty,

                amount,

                movementReferenceType,

                historySourceId,

                null,

                null,

                null,

                actorUserId

        ));



        List<PurchaseHistoryRecord> histories = purchaseHistoryRepository.findActiveBySource(
                historySourceType, historySourceId);
        purchaseHistoryRepository.deactivateBySource(historySourceType, historySourceId, actorUserId);

        for (PurchaseHistoryRecord history : histories) {
            if (history.approvalStatus() == PayableApprovalStatus.APPROVED) {
                partnerLedgerService.subtractPurchaseAmount(
                        partnerId, history.historyDate(), history.amount(), actorUserId);
            }
        }

    }



    static String resolveLocationCode(String propertyClassification) {

        if (propertyClassification == null || propertyClassification.isBlank()) {

            throw new IllegalArgumentException("품목 재고분류가 없습니다.");

        }

        PropertyClassification classification = PropertyClassification.valueOf(propertyClassification);

        return switch (classification) {

            case 원자재 -> "RAW";

            case 상품, 제품 -> "SALES";

            default -> throw new IllegalArgumentException(

                    "구매입고가 허용되지 않는 재고분류입니다: " + propertyClassification

            );

        };

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

        String prefix = "PR-" + receiptDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";

        long seq = receiptRepository.countByReceiptNoPrefix(prefix) + 1;

        return prefix + String.format("%03d", seq);

    }

}


