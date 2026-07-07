package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.time.LocalDate;

public record ProductionPlanStandaloneCommand(
        long itemId,
        BigDecimal plannedQty,
        LocalDate requestedDeliveryDate
) {
}
