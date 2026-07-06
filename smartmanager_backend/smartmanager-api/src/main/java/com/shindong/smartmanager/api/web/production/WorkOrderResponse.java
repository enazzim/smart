package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkOrderView;
import com.shindong.smartmanager.domain.production.WorkOrderStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record WorkOrderResponse(
        long id,
        long workPlanId,
        String orderNum,
        long productionPlanId,
        String planNo,
        long itemId,
        String itemNo,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        String processCode,
        String processName,
        Long workCenterId,
        String workCenterName,
        BigDecimal orderedQty,
        BigDecimal reportedQty,
        BigDecimal remainingQty,
        LocalDate planStartDate,
        WorkOrderStatus status,
        String statusLabel,
        boolean cancellable,
        Instant createdAt,
        String createdBy
) {
    public static WorkOrderResponse from(WorkOrderView view) {
        return new WorkOrderResponse(
                view.id(),
                view.workPlanId(),
                view.orderNum(),
                view.productionPlanId(),
                view.planNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processSequenceId(),
                view.processSequenceNum(),
                view.processCode(),
                view.processName(),
                view.workCenterId(),
                view.workCenterName(),
                view.orderedQty(),
                view.reportedQty(),
                view.remainingQty(),
                view.planStartDate(),
                view.status(),
                statusLabel(view.status()),
                view.cancellable(),
                view.createdAt(),
                view.createdBy()
        );
    }

    private static String statusLabel(WorkOrderStatus status) {
        return switch (status) {
            case ISSUED -> "발행";
            case CANCELLED -> "취소";
        };
    }
}
