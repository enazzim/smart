package com.shindong.smartmanager.application.production;

import java.time.LocalDate;
import java.util.List;

public record WorkCenterLoadDayView(
        long workCenterId,
        String workCenterName,
        LocalDate date,
        long demandMinutes,
        int capaMinutes,
        Double loadRate,
        int workPlanCount,
        boolean overThreshold,
        List<WorkCenterLoadDetailView> details
) {
}
