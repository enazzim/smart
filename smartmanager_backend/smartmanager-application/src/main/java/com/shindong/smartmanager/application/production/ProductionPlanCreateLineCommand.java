package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record ProductionPlanCreateLineCommand(
        long salesOrderLineId,
        BigDecimal plannedQty
) {
}
