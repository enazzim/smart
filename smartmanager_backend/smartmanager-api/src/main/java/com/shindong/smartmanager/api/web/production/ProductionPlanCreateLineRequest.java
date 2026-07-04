package com.shindong.smartmanager.api.web.production;

import java.math.BigDecimal;

public record ProductionPlanCreateLineRequest(
        long salesOrderLineId,
        BigDecimal plannedQty
) {
}
