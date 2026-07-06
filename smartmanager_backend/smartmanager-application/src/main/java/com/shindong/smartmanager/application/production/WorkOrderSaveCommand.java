package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkOrderSaveCommand(
        long workPlanId,
        String orderNum,
        BigDecimal orderedQty
) {
}
