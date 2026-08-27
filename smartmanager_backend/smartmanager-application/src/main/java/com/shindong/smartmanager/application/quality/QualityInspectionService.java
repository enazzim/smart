package com.shindong.smartmanager.application.quality;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineReceiptContext;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderView;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptService;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.purchase.PurchaseOrderLineReceiptContext;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptLineView;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptRepository;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptService;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptView;
import com.shindong.smartmanager.application.sales.SalesOrderFulfillmentSyncService;
import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.List;
import java.util.Map;

public class QualityInspectionService {

    private final QualityInspectionRepository inspectionRepository;
    private final PurchaseReceiptRepository purchaseReceiptRepository;
    private final PurchaseReceiptService purchaseReceiptService;
    private final OutsourcingReceiptRepository outsourcingReceiptRepository;
    private final OutsourcingReceiptService outsourcingReceiptService;
    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;
    private final SalesOrderFulfillmentSyncService salesOrderFulfillmentSyncService;

    public QualityInspectionService(
            QualityInspectionRepository inspectionRepository,
            PurchaseReceiptRepository purchaseReceiptRepository,
            PurchaseReceiptService purchaseReceiptService,
            OutsourcingReceiptRepository outsourcingReceiptRepository,
            OutsourcingReceiptService outsourcingReceiptService,
            OutsourcingOrderRepository outsourcingOrderRepository,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService,
            SalesOrderFulfillmentSyncService salesOrderFulfillmentSyncService
    ) {
        this.inspectionRepository = inspectionRepository;
        this.purchaseReceiptRepository = purchaseReceiptRepository;
        this.purchaseReceiptService = purchaseReceiptService;
        this.outsourcingReceiptRepository = outsourcingReceiptRepository;
        this.outsourcingReceiptService = outsourcingReceiptService;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
        this.salesOrderFulfillmentSyncService = salesOrderFulfillmentSyncService;
    }

    public List<QualityInspectionView> list(QualityInspectionListCriteria criteria) {
        return inspectionRepository.findActive(criteria);
    }

    public QualityInspectionView get(long id) {
        return inspectionRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("품질검사를 찾을 수 없습니다: " + id));
    }

    public QualityInspectionView complete(long id, CompleteQualityInspectionCommand command, String actorUserId) {
        monthClosingService.assertTransactionOpen(command.completedDate());
        FiscalPeriod fiscalPeriod = fiscalCalendarService.resolvePeriod(
                command.completedDate(),
                command.fiscalYear(),
                command.fiscalMonth()
        );
        monthClosingService.assertPeriodOpen(fiscalPeriod.fiscalYear(), fiscalPeriod.fiscalMonth());

        QualityInspectionView inspection = get(id);
        if (inspection.status() != QualityInspectionStatus.PENDING) {
            throw new IllegalArgumentException("대기 중인 검사만 완료할 수 있습니다.");
        }

        BigDecimal total = command.passedQty().add(command.failedQty());
        int cmp = total.compareTo(inspection.requestQty());
        if (cmp < 0) {
            throw new IllegalArgumentException(
                    "합격·불량 수량 합계가 의뢰수량보다 작습니다. 의뢰="
                            + inspection.requestQty().stripTrailingZeros().toPlainString()
            );
        }
        if (cmp > 0 && !command.allowOverQty()) {
            throw new IllegalArgumentException(
                    "합격·불량 수량 합계가 의뢰수량을 초과합니다. 의뢰="
                            + inspection.requestQty().stripTrailingZeros().toPlainString()
            );
        }

        Instant completedAt = command.completedDate().atStartOfDay(ZoneId.systemDefault()).toInstant();
        inspectionRepository.complete(
                id,
                command.passedQty(),
                command.failedQty(),
                command.inspectionDecisionCodeId(),
                command.unsuitabilityCauseCodeId(),
                command.unsuitabilityStatusCodeId(),
                blankToNull(command.failureReason()),
                completedAt,
                actorUserId
        );

        if (inspection.sourceType() == QualityInspectionSourceType.OUTSOURCE) {
            completeOutsourceInspection(inspection, command, fiscalPeriod, actorUserId);
        } else {
            completePurchaseInspection(inspection, command, fiscalPeriod, actorUserId);
        }

        return get(id);
    }

    public void cancel(long id, String actorUserId) {
        QualityInspectionView inspection = get(id);
        if (inspection.status() != QualityInspectionStatus.COMPLETED) {
            throw new IllegalArgumentException("완료된 검사만 취소할 수 있습니다.");
        }
        if (inspection.completedAt() == null) {
            throw new IllegalStateException("검사완료일이 없습니다.");
        }
        LocalDate completedDate = inspection.completedAt().atZone(ZoneId.systemDefault()).toLocalDate();
        monthClosingService.assertTransactionOpen(completedDate);

        if (inspection.sourceType() == QualityInspectionSourceType.OUTSOURCE) {
            revertOutsourceInspectionToPending(inspection, completedDate, actorUserId);
        } else {
            revertPurchaseInspectionToPending(inspection, completedDate, actorUserId);
        }

        inspectionRepository.revertCompletedToPending(id, actorUserId);
    }

    private void revertPurchaseInspectionToPending(
            QualityInspectionView inspection,
            LocalDate movementDate,
            String actorUserId
    ) {
        PurchaseReceiptView receipt = findPurchaseReceiptByLineId(inspection.sourceReceiptLineId());
        if (receipt.status() == com.shindong.smartmanager.domain.purchase.PurchaseReceiptStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 입고 전표의 품질검사는 되돌릴 수 없습니다.");
        }
        PurchaseReceiptLineView receiptLine = receipt.lines().stream()
                .filter(line -> line.id() == inspection.sourceReceiptLineId())
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("입고 라인을 찾을 수 없습니다."));
        PurchaseOrderLineReceiptContext orderLine = purchaseReceiptRepository.findOrderLineContext(
                receiptLine.purchaseOrderLineId());

        BigDecimal passedQty = inspection.passedQty() != null ? inspection.passedQty() : BigDecimal.ZERO;
        if (passedQty.compareTo(BigDecimal.ZERO) > 0) {
            purchaseReceiptService.reverseStockAndLedger(
                    receiptLine,
                    orderLine,
                    inspection.companyId(),
                    movementDate,
                    passedQty,
                    PurchaseHistorySourceType.QUALITY_INSPECTION,
                    inspection.id(),
                    "QUALITY_INSPECTION_CANCEL",
                    actorUserId
            );
            purchaseReceiptRepository.subtractReceivedQty(orderLine.purchaseOrderLineId(), passedQty, actorUserId);
            purchaseReceiptRepository.updateReceiptLinePostedQty(receiptLine.id(), passedQty.negate(), actorUserId);
        }

        purchaseReceiptRepository.addWaitingInspectionQty(
                orderLine.purchaseOrderLineId(), inspection.requestQty(), actorUserId);
        purchaseReceiptRepository.updateReceiptStatus(receipt.id(), actorUserId);
        purchaseReceiptRepository.refreshPurchaseOrderReceiptStatus(orderLine.purchaseOrderId(), actorUserId);
    }

    private void revertOutsourceInspectionToPending(
            QualityInspectionView inspection,
            LocalDate movementDate,
            String actorUserId
    ) {
        OutsourcingReceiptView receipt = findOutsourcingReceiptByLineId(inspection.sourceReceiptLineId());
        if (receipt.status() == com.shindong.smartmanager.domain.outsource.OutsourcingReceiptStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 입고 전표의 품질검사는 되돌릴 수 없습니다.");
        }
        OutsourcingReceiptLineView receiptLine = receipt.lines().stream()
                .filter(line -> line.id() == inspection.sourceReceiptLineId())
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("외주입고 라인을 찾을 수 없습니다."));
        OutsourcingOrderLineReceiptContext orderLine = outsourcingReceiptRepository.findOrderLineContext(
                receiptLine.outsourcingOrderLineId());
        OutsourcingOrderView order = outsourcingOrderRepository.findActiveByOrderLineId(
                receiptLine.outsourcingOrderLineId()).orElseThrow();
        OutsourcingOrderLineView orderLineView = order.lines().stream()
                .filter(line -> line.id() == receiptLine.outsourcingOrderLineId())
                .findFirst()
                .orElseThrow();

        BigDecimal passedQty = inspection.passedQty() != null ? inspection.passedQty() : BigDecimal.ZERO;
        if (inspection.requestQty().compareTo(BigDecimal.ZERO) > 0) {
            outsourcingReceiptService.reverseStockAndLedger(
                    receiptLine,
                    orderLine,
                    order,
                    orderLineView,
                    inspection.companyId(),
                    movementDate,
                    inspection.requestQty(),
                    passedQty,
                    OutsourceHistorySourceType.QUALITY_INSPECTION,
                    inspection.id(),
                    actorUserId
            );
        }
        if (passedQty.compareTo(BigDecimal.ZERO) > 0) {
            outsourcingReceiptRepository.subtractReceivedQty(
                    orderLine.outsourcingOrderLineId(), passedQty, actorUserId);
            outsourcingReceiptRepository.updateReceiptLinePostedQty(
                    receiptLine.id(), passedQty.negate(), actorUserId);
        }

        outsourcingReceiptRepository.addWaitingInspectionQty(
                orderLine.outsourcingOrderLineId(), inspection.requestQty(), actorUserId);
        outsourcingReceiptRepository.updateReceiptStatus(receipt.id(), actorUserId);
        outsourcingOrderRepository.refreshOrderStatus(orderLine.outsourcingOrderId(), actorUserId);
        syncFulfillmentForWorkPlan(orderLineView.workPlanId(), actorUserId);
    }

    private void completePurchaseInspection(
            QualityInspectionView inspection,
            CompleteQualityInspectionCommand command,
            FiscalPeriod fiscalPeriod,
            String actorUserId
    ) {
        PurchaseReceiptView receipt = findPurchaseReceiptByLineId(inspection.sourceReceiptLineId());
        PurchaseReceiptLineView receiptLine = receipt.lines().stream()
                .filter(line -> line.id() == inspection.sourceReceiptLineId())
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("입고 라인을 찾을 수 없습니다."));

        PurchaseOrderLineReceiptContext orderLine = purchaseReceiptRepository.findOrderLineContext(
                receiptLine.purchaseOrderLineId());
        purchaseReceiptRepository.releaseWaitingInspectionQty(
                orderLine.purchaseOrderLineId(), inspection.requestQty(), actorUserId);

        if (command.passedQty().compareTo(BigDecimal.ZERO) > 0) {
            if (orderLine.lotTracked()
                    && !command.autoGenerateLot()
                    && (command.lotNo() == null || command.lotNo().isBlank())) {
                throw new IllegalArgumentException(
                        "Lot 추적 품목은 Lot 번호 또는 자동생성이 필요합니다: " + orderLine.itemNum());
            }
            purchaseReceiptService.applyStockAndLedger(
                    receiptLine,
                    orderLine,
                    inspection.companyId(),
                    command.completedDate(),
                    command.passedQty(),
                    PurchaseHistorySourceType.QUALITY_INSPECTION,
                    inspection.id(),
                    "QUALITY_INSPECTION",
                    fiscalPeriod,
                    command.lotNo(),
                    command.autoGenerateLot(),
                    actorUserId
            );
            purchaseReceiptRepository.addReceivedQty(orderLine.purchaseOrderLineId(), command.passedQty(), actorUserId);
            purchaseReceiptRepository.updateReceiptLinePostedQty(receiptLine.id(), command.passedQty(), actorUserId);
        }

        purchaseReceiptRepository.updateReceiptStatus(receipt.id(), actorUserId);
        purchaseReceiptRepository.refreshPurchaseOrderReceiptStatus(orderLine.purchaseOrderId(), actorUserId);
    }

    private void completeOutsourceInspection(
            QualityInspectionView inspection,
            CompleteQualityInspectionCommand command,
            FiscalPeriod fiscalPeriod,
            String actorUserId
    ) {
        OutsourcingReceiptView receipt = findOutsourcingReceiptByLineId(inspection.sourceReceiptLineId());
        OutsourcingReceiptLineView receiptLine = receipt.lines().stream()
                .filter(line -> line.id() == inspection.sourceReceiptLineId())
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("외주입고 라인을 찾을 수 없습니다."));

        OutsourcingOrderLineReceiptContext orderLine = outsourcingReceiptRepository.findOrderLineContext(
                receiptLine.outsourcingOrderLineId());
        outsourcingReceiptRepository.releaseWaitingInspectionQty(
                orderLine.outsourcingOrderLineId(), inspection.requestQty(), actorUserId);

        OutsourcingOrderView order = outsourcingOrderRepository.findActiveByOrderLineId(
                receiptLine.outsourcingOrderLineId()).orElseThrow();
        OutsourcingOrderLineView orderLineView = order.lines().stream()
                .filter(line -> line.id() == receiptLine.outsourcingOrderLineId())
                .findFirst()
                .orElseThrow();

        if (inspection.requestQty().compareTo(BigDecimal.ZERO) > 0) {
            outsourcingReceiptService.applyStockAndLedger(
                    receiptLine,
                    orderLine,
                    order,
                    orderLineView,
                    inspection.companyId(),
                    command.completedDate(),
                    inspection.requestQty(),
                    command.passedQty(),
                    OutsourceHistorySourceType.QUALITY_INSPECTION,
                    inspection.id(),
                    fiscalPeriod,
                    command.lotNo(),
                    command.autoGenerateLot(),
                    Map.of(),
                    actorUserId
            );
            if (command.passedQty().compareTo(BigDecimal.ZERO) > 0) {
                outsourcingReceiptRepository.addReceivedQty(orderLine.outsourcingOrderLineId(), command.passedQty(), actorUserId);
                outsourcingReceiptRepository.updateReceiptLinePostedQty(receiptLine.id(), command.passedQty(), actorUserId);
            }
        }

        outsourcingReceiptRepository.updateReceiptStatus(receipt.id(), actorUserId);
        outsourcingOrderRepository.refreshOrderStatus(orderLine.outsourcingOrderId(), actorUserId);
        syncFulfillmentForWorkPlan(orderLineView.workPlanId(), actorUserId);
    }

    private PurchaseReceiptView findPurchaseReceiptByLineId(long receiptLineId) {
        return purchaseReceiptRepository.findAllActive().stream()
                .filter(receipt -> receipt.lines().stream().anyMatch(line -> line.id() == receiptLineId))
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("구매입고 전표를 찾을 수 없습니다."));
    }

    private OutsourcingReceiptView findOutsourcingReceiptByLineId(long receiptLineId) {
        return outsourcingReceiptRepository.findAllActive(null).stream()
                .filter(receipt -> receipt.lines().stream().anyMatch(line -> line.id() == receiptLineId))
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("외주입고 전표를 찾을 수 없습니다."));
    }

    private static String blankToNull(String value) {
        if (value == null || value.isBlank()) {
            return null;
        }
        return value.trim();
    }

    private void syncFulfillmentForWorkPlan(Long workPlanId, String actorUserId) {
        if (workPlanId == null) {
            return;
        }
        salesOrderFulfillmentSyncService.syncByWorkPlanId(workPlanId, actorUserId);
    }
}
