package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesShipmentCandidateView(
        long orderLineId,
        long orderId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal remainingQty,
        BigDecimal salesOnHandQty,
        BigDecimal wipOnHandQty,
        boolean shippable,
        String shippableMessage
) {
}
