package com.shindong.smartmanager.api.web.quality;

import com.shindong.smartmanager.application.quality.QualityInspectionView;
import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record QualityInspectionResponse(
        long id,
        String inspectionNo,
        QualityInspectionSourceType sourceType,
        long sourceReceiptLineId,
        long itemId,
        String itemNum,
        String itemName,
        long companyId,
        String companyName,
        BigDecimal requestQty,
        BigDecimal passedQty,
        BigDecimal failedQty,
        QualityInspectionStatus status,
        String orderNo,
        LocalDate receiptDate,
        Instant createdAt,
        Instant completedAt,
        boolean lotTracked,
        String failureReason
) {
    public static QualityInspectionResponse from(QualityInspectionView view) {
        return new QualityInspectionResponse(
                view.id(),
                view.inspectionNo(),
                view.sourceType(),
                view.sourceReceiptLineId(),
                view.itemId(),
                view.itemNum(),
                view.itemName(),
                view.companyId(),
                view.companyName(),
                view.requestQty(),
                view.passedQty(),
                view.failedQty(),
                view.status(),
                view.orderNo(),
                view.receiptDate(),
                view.createdAt(),
                view.completedAt(),
                view.lotTracked(),
                view.failureReason()
        );
    }
}
