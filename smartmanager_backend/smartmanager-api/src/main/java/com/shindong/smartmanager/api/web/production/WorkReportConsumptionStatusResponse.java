package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkReportConsumptionLineView;
import com.shindong.smartmanager.application.production.WorkReportConsumptionStatusView;
import java.math.BigDecimal;
import java.util.List;

public record WorkReportConsumptionStatusResponse(
        long workOrderId,
        String parentItemNo,
        String parentItemName,
        BigDecimal cumulativeGoodQty,
        BigDecimal pendingGoodQty,
        List<WorkReportConsumptionLineResponse> lines,
        boolean allSatisfied,
        boolean materialIssueEnabled
) {
    public static WorkReportConsumptionStatusResponse from(WorkReportConsumptionStatusView view) {
        return new WorkReportConsumptionStatusResponse(
                view.workOrderId(),
                view.parentItemNo(),
                view.parentItemName(),
                view.cumulativeGoodQty(),
                view.pendingGoodQty(),
                view.lines().stream().map(WorkReportConsumptionLineResponse::from).toList(),
                view.allSatisfied(),
                view.materialIssueEnabled()
        );
    }
}
