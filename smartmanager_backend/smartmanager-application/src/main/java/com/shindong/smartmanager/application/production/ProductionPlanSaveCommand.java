package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.time.LocalDate;

public record ProductionPlanSaveCommand(
        long salesOrderId,
        long salesOrderLineId,
        long itemId,
        BigDecimal plannedQty,
        LocalDate requestedDeliveryDate
) {
}
