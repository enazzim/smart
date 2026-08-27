package com.shindong.smartmanager.api.web.quality;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import java.math.BigDecimal;
import java.time.LocalDate;

public record CompleteQualityInspectionRequest(
        @NotNull BigDecimal passedQty,
        @NotNull BigDecimal failedQty,
        Long inspectionDecisionCodeId,
        Long unsuitabilityCauseCodeId,
        Long unsuitabilityStatusCodeId,
        @Size(max = 500) String failureReason,
        @NotNull LocalDate completedDate,
        Integer fiscalYear,
        Integer fiscalMonth,
        String lotNo,
        Boolean autoGenerateLot,
        Boolean allowOverQty
) {
}
