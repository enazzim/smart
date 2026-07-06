package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MrpCancelPlanResult;

public record MrpCancelPlanResponse(
        long productionPlanId,
        String planNo
) {
    public static MrpCancelPlanResponse from(MrpCancelPlanResult result) {
        return new MrpCancelPlanResponse(result.productionPlanId(), result.planNo());
    }
}
