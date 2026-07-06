package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.util.List;

public record MaterialIssueConsumptionPreviewView(
        long workOrderId,
        String parentItemNo,
        BigDecimal goodQty,
        List<MaterialIssuePreviewLineView> lines
) {
}
