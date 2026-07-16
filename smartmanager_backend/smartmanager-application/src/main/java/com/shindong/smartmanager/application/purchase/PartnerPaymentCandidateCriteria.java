package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;

public record PartnerPaymentCandidateCriteria(
        String partnerName,
        boolean includeZeroUnpaid
) {
    public PartnerPaymentCandidateCriteria(String partnerName) {
        this(partnerName, false);
    }
}
