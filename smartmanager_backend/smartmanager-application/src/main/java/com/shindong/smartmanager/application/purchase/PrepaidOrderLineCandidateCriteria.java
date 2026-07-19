package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;

public record PrepaidOrderLineCandidateCriteria(
        Long partnerId,
        PartnerPaymentCostCategory costCategory,
        String itemNo,
        String itemName,
        String orderNo
) {
}
