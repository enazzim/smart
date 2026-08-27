package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import java.math.BigDecimal;
import java.time.LocalDate;

public record PrepaidOrderLineCandidateView(
        PartnerPaymentCostCategory costCategory,
        long orderLineId,
        long orderId,
        String orderNo,
        short lineNo,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        LocalDate orderDate,
        BigDecimal orderAmount,
        BigDecimal prepaidLinkedAmount,
        BigDecimal remainingAmount
) {
}
