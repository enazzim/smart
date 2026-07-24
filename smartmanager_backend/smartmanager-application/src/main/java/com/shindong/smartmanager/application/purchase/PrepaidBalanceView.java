package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import java.math.BigDecimal;

public record PrepaidBalanceView(
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        PartnerPaymentCostCategory costCategory,
        BigDecimal prepaidIn,
        BigDecimal prepaidOut,
        BigDecimal prepaidRemaining
) {
}
