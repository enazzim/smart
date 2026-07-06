package com.shindong.smartmanager.api.web.quality;

import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;

public record CompleteQualityInspectionRequest(
        @NotNull BigDecimal passedQty,
        @NotNull BigDecimal failedQty,
        Long inspectionDecisionCodeId,
        Long unsuitabilityCauseCodeId,
        Long unsuitabilityStatusCodeId,
        @NotNull LocalDate completedDate
) {
}
