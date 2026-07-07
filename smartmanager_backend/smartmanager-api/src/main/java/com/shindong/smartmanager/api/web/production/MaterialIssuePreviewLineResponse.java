package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MaterialIssuePreviewLineView;
import java.math.BigDecimal;

public record MaterialIssuePreviewLineResponse(
        Long itemCompositionId,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        BigDecimal unitRatio,
        BigDecimal requiredQty,
        Long sourceProcessId,
        String sourceProcessName
) {
    public static MaterialIssuePreviewLineResponse from(MaterialIssuePreviewLineView view) {
        return new MaterialIssuePreviewLineResponse(
                view.itemCompositionId(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.unitRatio(),
                view.requiredQty(),
                view.sourceProcessId(),
                view.sourceProcessName()
        );
    }
}
