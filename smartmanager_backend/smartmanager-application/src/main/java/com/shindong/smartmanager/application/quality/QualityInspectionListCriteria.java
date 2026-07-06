package com.shindong.smartmanager.application.quality;

import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.time.LocalDate;

public record QualityInspectionListCriteria(
        QualityInspectionStatus status,
        QualityInspectionSourceType sourceType,
        String partnerName,
        String itemNo,
        String itemName,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        LocalDate completedDateFrom,
        LocalDate completedDateTo
) {
}
