package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.MrpPurchaseCandidateView;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.List;

public record MrpPurchaseCandidateResponse(
        long requirementLineId,
        long mrpRunId,
        String runNo,
        long productionPlanId,
        String planNo,
        long componentItemId,
        String componentItemNo,
        String componentItemName,
        String componentPropertyClassification,
        BigDecimal grossQty,
        BigDecimal orderedQty,
        BigDecimal suggestedQty,
        String unit,
        Instant createdAt,
        BigDecimal orderRateTotal,
        boolean orderable,
        String orderableMessage,
        List<MrpPurchaseCandidateVendorResponse> vendors
) {
    public static MrpPurchaseCandidateResponse from(MrpPurchaseCandidateView view) {
        return new MrpPurchaseCandidateResponse(
                view.requirementLineId(),
                view.mrpRunId(),
                view.runNo(),
                view.productionPlanId(),
                view.planNo(),
                view.componentItemId(),
                view.componentItemNo(),
                view.componentItemName(),
                view.componentPropertyClassification(),
                view.grossQty(),
                view.orderedQty(),
                view.suggestedQty(),
                view.unit(),
                view.createdAt(),
                view.orderRateTotal(),
                view.orderable(),
                view.orderableMessage(),
                view.vendors().stream().map(MrpPurchaseCandidateVendorResponse::from).toList()
        );
    }
}
