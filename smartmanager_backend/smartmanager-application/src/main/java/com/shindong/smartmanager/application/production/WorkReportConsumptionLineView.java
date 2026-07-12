package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkReportConsumptionLineView(
        Long itemCompositionId,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        boolean lotTracked,
        String locationCode,
        BigDecimal unitRatio,
        BigDecimal requiredQty,
        BigDecimal issuedQty,
        BigDecimal remainingQty,
        boolean satisfied,
        Long sourceProcessId,
        String sourceProcessName,
        BigDecimal suggestedIssueQty
) {
}
