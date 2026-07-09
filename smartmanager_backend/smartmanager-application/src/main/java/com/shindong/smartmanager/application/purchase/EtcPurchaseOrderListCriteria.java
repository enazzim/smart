package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record EtcPurchaseOrderListCriteria(
        String itemName,
        String partnerName,
        LocalDate deliveryFrom,
        LocalDate deliveryTo,
        Boolean openOnly
) {
}
