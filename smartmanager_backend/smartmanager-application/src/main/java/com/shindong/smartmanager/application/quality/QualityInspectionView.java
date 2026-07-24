package com.shindong.smartmanager.application.quality;

import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record QualityInspectionView(
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
}
