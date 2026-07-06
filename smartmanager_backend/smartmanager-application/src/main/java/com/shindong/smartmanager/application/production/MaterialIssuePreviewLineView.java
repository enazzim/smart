package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.util.List;

public record MaterialIssuePreviewLineView(
        long itemCompositionId,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        BigDecimal unitRatio,
        BigDecimal requiredQty,
        Long sourceProcessId,
        String sourceProcessName
) {
}
