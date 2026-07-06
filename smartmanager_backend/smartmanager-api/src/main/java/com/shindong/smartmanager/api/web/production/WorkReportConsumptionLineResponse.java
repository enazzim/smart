package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkReportConsumptionLineView;
import java.math.BigDecimal;

public record WorkReportConsumptionLineResponse(
        long itemCompositionId,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        BigDecimal unitRatio,
        BigDecimal requiredQty,
        BigDecimal issuedQty,
        BigDecimal remainingQty,
        boolean satisfied,
        BigDecimal suggestedIssueQty,
        Long sourceProcessId,
        String sourceProcessName
) {
    public static WorkReportConsumptionLineResponse from(WorkReportConsumptionLineView view) {
        return new WorkReportConsumptionLineResponse(
                view.itemCompositionId(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.unitRatio(),
                view.requiredQty(),
                view.issuedQty(),
                view.remainingQty(),
                view.satisfied(),
                view.suggestedIssueQty(),
                view.sourceProcessId(),
                view.sourceProcessName()
        );
    }
}
