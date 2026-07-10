package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record EtcPurchaseOrderListCriteria(
        String itemName,
        String partnerName,
        String orderNo,
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        LocalDate deliveryFrom,
        LocalDate deliveryTo,
        Boolean openOnly
) {
}
