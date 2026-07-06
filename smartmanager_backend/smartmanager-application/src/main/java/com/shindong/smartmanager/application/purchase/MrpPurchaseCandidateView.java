package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.Instant;
import java.util.List;

public record MrpPurchaseCandidateView(
        long requirementLineId,
        long mrpRunId,
        String runNo,
        long productionPlanId,
        String planNo,
        long componentItemId,
        String componentItemNo,
        String componentItemName,
        String componentPropertyClassification,
        String unit,
        BigDecimal grossQty,
        BigDecimal orderedQty,
        BigDecimal suggestedQty,
        Instant createdAt,
        BigDecimal orderRateTotal,
        boolean orderable,
        String orderableMessage,
        List<MrpPurchaseCandidateVendorView> vendors
) {
}
