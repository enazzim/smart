package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.math.RoundingMode;

public final class WorkPlanDemandCalculator {

    private WorkPlanDemandCalculator() {
    }

    /**
     * §5.4: T_sec = (setupTime × 60) + (standardTime × Q), 결과는 분(올림).
     */
    public static long demandMinutes(int setupTime, int standardTime, BigDecimal plannedQty) {
        BigDecimal seconds = BigDecimal.valueOf(setupTime)
                .multiply(BigDecimal.valueOf(60))
                .add(BigDecimal.valueOf(standardTime).multiply(plannedQty));
        return seconds.divide(BigDecimal.valueOf(60), 0, RoundingMode.CEILING).longValue();
    }
}
