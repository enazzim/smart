package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record MaterialRequirementLineSaveCommand(
        long mrpRunId,
        long productionPlanId,
        long parentItemId,
        long componentItemId,
        BigDecimal bomUnitQty,
        BigDecimal plannedQty,
        BigDecimal grossQty
) {
}
