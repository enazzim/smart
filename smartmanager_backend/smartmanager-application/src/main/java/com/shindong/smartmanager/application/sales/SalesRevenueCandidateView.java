package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesRevenueCandidateView(
        long shipmentLineId,
        long shipmentId,
        String shipmentNo,
        LocalDate shipmentDate,
        long partnerId,
        String partnerName,
        long orderLineId,
        String orderNo,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal shippedQty,
        BigDecimal invoicedQty,
        BigDecimal remainingQty,
        BigDecimal deliveryOnHandQty,
        BigDecimal unitPrice,
        boolean billable,
        String billableMessage
) {
}
