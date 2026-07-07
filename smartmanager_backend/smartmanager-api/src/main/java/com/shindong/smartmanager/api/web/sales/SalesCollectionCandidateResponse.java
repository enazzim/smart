package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesCollectionCandidateView;
import java.math.BigDecimal;

public record SalesCollectionCandidateResponse(
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        BigDecimal revenueAmount,
        BigDecimal collectedAmount,
        BigDecimal uncollectedAmount,
        boolean collectable
) {
    public static SalesCollectionCandidateResponse from(SalesCollectionCandidateView view) {
        return new SalesCollectionCandidateResponse(
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.revenueAmount(),
                view.collectedAmount(),
                view.uncollectedAmount(),
                view.collectable()
        );
    }
}
