package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record EtcPurchaseReceiptListCriteria(
        String itemName,
        String partnerName,
        LocalDate receiptFrom,
        LocalDate receiptTo
) {
}
