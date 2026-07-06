package com.shindong.smartmanager.application.production;

public record MrpCancelPlanResult(
        long productionPlanId,
        String planNo
) {
}
