package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingShipmentCandidateView(
        long orderLineId,
        long orderId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        String processName,
        String beginProcessName,
        String endProcessName,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal remainingQty,
        boolean shippable,
        String shippableMessage
) {
}
