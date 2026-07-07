package com.shindong.smartmanager.application.sales;

import java.time.LocalDate;

public record SalesShipmentCandidateCriteria(
        String partnerName,
        String orderNo,
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        String itemNum,
        String itemName
) {
}
