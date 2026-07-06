package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkCenterLoadDetailView(
        long workPlanId,
        String planNo,
        String itemNo,
        String itemName,
        String processName,
        BigDecimal plannedQty,
        long demandMinutes,
        int setupTime,
        int standardTime
) {
}
