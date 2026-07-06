package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkPlanView;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.WorkPlanStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record WorkPlanResponse(
        long id,
        long productionPlanId,
        String planNo,
        long itemId,
        String itemNo,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        String processCode,
        String processName,
        WorkDistinction workDistinction,
        String workDistinctionLabel,
        Long workCenterId,
        String workCenterName,
        BigDecimal plannedQty,
        LocalDate planStartDate,
        LocalDate planEndDate,
        int setupTime,
        int standardTime,
        WorkPlanStatus status,
        String statusLabel,
        boolean cancellable,
        Instant createdAt,
        String createdBy
) {
    public static WorkPlanResponse from(WorkPlanView view) {
        return new WorkPlanResponse(
                view.id(),
                view.productionPlanId(),
                view.planNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processSequenceId(),
                view.processSequenceNum(),
                view.processCode(),
                view.processName(),
                view.workDistinction(),
                workDistinctionLabel(view.workDistinction()),
                view.workCenterId(),
                view.workCenterName(),
                view.plannedQty(),
                view.planStartDate(),
                view.planEndDate(),
                view.setupTime(),
                view.standardTime(),
                view.status(),
                statusLabel(view.status()),
                view.cancellable(),
                view.createdAt(),
                view.createdBy()
        );
    }

    private static String workDistinctionLabel(WorkDistinction distinction) {
        return switch (distinction) {
            case INHOUSE -> "자가";
            case OUTSOURCE -> "외주";
            case SPLIT -> "혼합";
        };
    }

    private static String statusLabel(WorkPlanStatus status) {
        return switch (status) {
            case PLANNED -> "수립";
            case CANCELLED -> "취소";
        };
    }
}
