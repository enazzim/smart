package com.shindong.smartmanager.application.quality;

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
import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.List;

public class QualityInspectionService {

    private final QualityInspectionRepository inspectionRepository;
    private final PurchaseReceiptRepository purchaseReceiptRepository;
    private final PurchaseReceiptService purchaseReceiptService;
    private final OutsourcingReceiptRepository outsourcingReceiptRepository;
    private final OutsourcingReceiptService outsourcingReceiptService;
    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final MonthClosingService monthClosingService;

    public QualityInspectionService(
            QualityInspectionRepository inspectionRepository,
            PurchaseReceiptRepository purchaseReceiptRepository,
            PurchaseReceiptService purchaseReceiptService,
            OutsourcingReceiptRepository outsourcingReceiptRepository,
            OutsourcingReceiptService outsourcingReceiptService,
            OutsourcingOrderRepository outsourcingOrderRepository,
            MonthClosingService monthClosingService
    ) {
        this.inspectionRepository = inspectionRepository;
        this.purchaseReceiptRepository = purchaseReceiptRepository;
        this.purchaseReceiptService = purchaseReceiptService;
        this.outsourcingReceiptRepository = outsourcingReceiptRepository;
        this.outsourcingReceiptService = outsourcingReceiptService;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.monthClosingService = monthClosingService;
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

        QualityInspectionView inspection = get(id);
        if (inspection.status() != QualityInspectionStatus.PENDING) {
            throw new IllegalArgumentException("대기 중인 검사만 완료할 수 있습니다.");
        }

        BigDecimal total = command.passedQty().add(command.failedQty());
        if (total.compareTo(inspection.requestQty()) != 0) {
            throw new IllegalArgumentException(
                    "합격·불량 수량 합계가 의뢰수량과 일치해야 합니다. 의뢰="
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
                completedAt,
                actorUserId
        );

        if (inspection.sourceType() == QualityInspectionSourceType.OUTSOURCE) {
            completeOutsourceInspection(inspection, command, actorUserId);
        } else {
            completePurchaseInspection(inspection, command, actorUserId);
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

        if (inspection.passedQty().compareTo(BigDecimal.ZERO) > 0
                || inspection.requestQty().compareTo(BigDecimal.ZERO) > 0) {
            if (inspection.sourceType() == QualityInspectionSourceType.OUTSOURCE) {
                cancelOutsourceInspectionStock(inspection, completedDate, actorUserId);
            } else {
                cancelPurchaseInspectionStock(inspection, completedDate, actorUserId);
            }
        }

        inspectionRepository.cancelById(id, actorUserId);
    }

    private void cancelPurchaseInspectionStock(
            QualityInspectionView inspection,
            LocalDate movementDate,
            String actorUserId
    ) {
        PurchaseReceiptView receipt = findPurchaseReceiptByLineId(inspection.sourceReceiptLineId());
        PurchaseReceiptLineView receiptLine = receipt.lines().stream()
                .filter(line -> line.id() == inspection.sourceReceiptLineId())
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("입고 라인을 찾을 수 없습니다."));
        PurchaseOrderLineReceiptContext orderLine = purchaseReceiptRepository.findOrderLineContext(
                receiptLine.purchaseOrderLineId());

        purchaseReceiptService.reverseStockAndLedger(
                receiptLine,
                orderLine,
                inspection.companyId(),
                movementDate,
                inspection.passedQty(),
                PurchaseHistorySourceType.QUALITY_INSPECTION,
                inspection.id(),
                "QUALITY_INSPECTION_CANCEL",
                actorUserId
        );
        purchaseReceiptRepository.subtractReceivedQty(orderLine.purchaseOrderLineId(), inspection.passedQty(), actorUserId);
        purchaseReceiptRepository.updateReceiptLinePostedQty(receiptLine.id(), inspection.passedQty().negate(), actorUserId);
        purchaseReceiptRepository.updateReceiptStatus(receipt.id(), actorUserId);
        purchaseReceiptRepository.refreshPurchaseOrderReceiptStatus(orderLine.purchaseOrderId(), actorUserId);
    }

    private void cancelOutsourceInspectionStock(
            QualityInspectionView inspection,
            LocalDate movementDate,
            String actorUserId
    ) {
        OutsourcingReceiptView receipt = findOutsourcingReceiptByLineId(inspection.sourceReceiptLineId());
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

        outsourcingReceiptService.reverseStockAndLedger(
                receiptLine,
                orderLine,
                order,
                orderLineView,
                inspection.companyId(),
                movementDate,
                inspection.requestQty(),
                inspection.passedQty(),
                OutsourceHistorySourceType.QUALITY_INSPECTION,
                inspection.id(),
                actorUserId
        );
        outsourcingReceiptRepository.subtractReceivedQty(orderLine.outsourcingOrderLineId(), inspection.passedQty(), actorUserId);
        outsourcingReceiptRepository.updateReceiptLinePostedQty(receiptLine.id(), inspection.passedQty().negate(), actorUserId);
        outsourcingReceiptRepository.updateReceiptStatus(receipt.id(), actorUserId);
        outsourcingOrderRepository.refreshOrderStatus(orderLine.outsourcingOrderId(), actorUserId);
    }

    private void completePurchaseInspection(
            QualityInspectionView inspection,
            CompleteQualityInspectionCommand command,
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
            purchaseReceiptService.applyStockAndLedger(
                    receiptLine,
                    orderLine,
                    inspection.companyId(),
                    command.completedDate(),
                    command.passedQty(),
                    PurchaseHistorySourceType.QUALITY_INSPECTION,
                    inspection.id(),
                    "QUALITY_INSPECTION",
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
                    actorUserId
            );
            if (command.passedQty().compareTo(BigDecimal.ZERO) > 0) {
                outsourcingReceiptRepository.addReceivedQty(orderLine.outsourcingOrderLineId(), command.passedQty(), actorUserId);
                outsourcingReceiptRepository.updateReceiptLinePostedQty(receiptLine.id(), command.passedQty(), actorUserId);
            }
        }

        outsourcingReceiptRepository.updateReceiptStatus(receipt.id(), actorUserId);
        outsourcingOrderRepository.refreshOrderStatus(orderLine.outsourcingOrderId(), actorUserId);
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
}
