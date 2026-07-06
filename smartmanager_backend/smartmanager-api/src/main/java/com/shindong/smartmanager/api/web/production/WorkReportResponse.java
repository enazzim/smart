package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkReportView;
import com.shindong.smartmanager.domain.production.WorkReportStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record WorkReportResponse(
        long id,
        String reportNum,
        long workOrderId,
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
        LocalDate reportDate,
        BigDecimal goodQty,
        BigDecimal scrapQty,
        BigDecimal setupTime,
        BigDecimal runTime,
        String workerName,
        WorkReportStatus status,
        String statusLabel,
        boolean cancellable,
        Instant createdAt,
        String createdBy
) {
    public static WorkReportResponse from(WorkReportView view) {
        return new WorkReportResponse(
                view.id(),
                view.reportNum(),
                view.workOrderId(),
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
                view.reportDate(),
                view.goodQty(),
                view.scrapQty(),
                view.setupTime(),
                view.runTime(),
                view.workerName(),
                view.status(),
                statusLabel(view.status()),
                view.cancellable(),
                view.createdAt(),
                view.createdBy()
        );
    }

    private static String statusLabel(WorkReportStatus status) {
        return switch (status) {
            case REGISTERED -> "등록";
            case CANCELLED -> "취소";
        };
    }
}
