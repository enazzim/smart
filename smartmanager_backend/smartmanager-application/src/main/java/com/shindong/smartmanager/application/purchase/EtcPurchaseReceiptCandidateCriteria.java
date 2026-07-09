package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record EtcPurchaseReceiptCandidateCriteria(
        String itemName,
        String partnerName,
        LocalDate deliveryFrom,
        LocalDate deliveryTo
) {
}
