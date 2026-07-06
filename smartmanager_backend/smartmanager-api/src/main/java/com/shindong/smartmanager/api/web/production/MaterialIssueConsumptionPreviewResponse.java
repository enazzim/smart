package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MaterialIssueConsumptionPreviewView;
import com.shindong.smartmanager.application.production.MaterialIssuePreviewLineView;
import java.math.BigDecimal;
import java.util.List;

public record MaterialIssueConsumptionPreviewResponse(
        long workOrderId,
        String parentItemNo,
        BigDecimal goodQty,
        List<MaterialIssuePreviewLineResponse> lines
) {
    public static MaterialIssueConsumptionPreviewResponse from(MaterialIssueConsumptionPreviewView view) {
        return new MaterialIssueConsumptionPreviewResponse(
                view.workOrderId(),
                view.parentItemNo(),
                view.goodQty(),
                view.lines().stream().map(MaterialIssuePreviewLineResponse::from).toList()
        );
    }
}
