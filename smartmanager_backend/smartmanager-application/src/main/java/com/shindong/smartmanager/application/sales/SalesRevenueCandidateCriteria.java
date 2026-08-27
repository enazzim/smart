package com.shindong.smartmanager.application.sales;

import java.time.LocalDate;

public record SalesRevenueCandidateCriteria(
        String partnerName,
        String shipmentNo,
        LocalDate shipmentDateFrom,
        LocalDate shipmentDateTo,
        String orderNo,
        String itemNum,
        String itemName,
        Long itemId
) {
}
