package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkReportIssueOnHandView;
import java.math.BigDecimal;

public record WorkReportIssueOnHandResponse(
        long itemId,
        BigDecimal onHandQty
) {
    public static WorkReportIssueOnHandResponse from(WorkReportIssueOnHandView view) {
        return new WorkReportIssueOnHandResponse(view.itemId(), view.onHandQty());
    }
}
