package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkCenterLoadDayView;
import com.shindong.smartmanager.application.production.WorkCenterLoadDetailView;
import com.shindong.smartmanager.application.production.WorkCenterLoadResult;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record WorkCenterLoadResponse(
        double warnLoadThreshold,
        List<WorkCenterLoadDayResponse> days
) {
    public static WorkCenterLoadResponse from(WorkCenterLoadResult result) {
        return new WorkCenterLoadResponse(
                result.warnLoadThreshold(),
                result.days().stream().map(WorkCenterLoadDayResponse::from).toList()
        );
    }
}

record WorkCenterLoadDayResponse(
        long workCenterId,
        String workCenterName,
        LocalDate date,
        long demandMinutes,
        int capaMinutes,
        Double loadRate,
        int workPlanCount,
        boolean overThreshold,
        List<WorkCenterLoadDetailResponse> details
) {
    static WorkCenterLoadDayResponse from(WorkCenterLoadDayView view) {
        return new WorkCenterLoadDayResponse(
                view.workCenterId(),
                view.workCenterName(),
                view.date(),
                view.demandMinutes(),
                view.capaMinutes(),
                view.loadRate(),
                view.workPlanCount(),
                view.overThreshold(),
                view.details().stream().map(WorkCenterLoadDetailResponse::from).toList()
        );
    }
}

record WorkCenterLoadDetailResponse(
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
    static WorkCenterLoadDetailResponse from(WorkCenterLoadDetailView view) {
        return new WorkCenterLoadDetailResponse(
                view.workPlanId(),
                view.planNo(),
                view.itemNo(),
                view.itemName(),
                view.processName(),
                view.plannedQty(),
                view.demandMinutes(),
                view.setupTime(),
                view.standardTime()
        );
    }
}
