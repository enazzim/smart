package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkPlanCancelPlanResult;

public record WorkPlanCancelPlanResponse(
        long productionPlanId,
        String planNo,
        int cancelledCount
) {
    public static WorkPlanCancelPlanResponse from(WorkPlanCancelPlanResult result) {
        return new WorkPlanCancelPlanResponse(
                result.productionPlanId(),
                result.planNo(),
                result.cancelledCount()
        );
    }
}
