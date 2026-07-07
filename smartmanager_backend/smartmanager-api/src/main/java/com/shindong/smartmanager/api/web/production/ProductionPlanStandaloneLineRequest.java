package com.shindong.smartmanager.api.web.production;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import java.math.BigDecimal;
import java.time.LocalDate;
import org.springframework.format.annotation.DateTimeFormat;

public record ProductionPlanStandaloneLineRequest(
        @NotNull @Positive long itemId,
        @NotNull @Positive BigDecimal plannedQty,
        @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate requestedDeliveryDate
) {
}
