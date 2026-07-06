package com.shindong.smartmanager.application.production;

public record WorkPlanCancelPlanResult(
        long productionPlanId,
        String planNo,
        int cancelledCount
) {
}
