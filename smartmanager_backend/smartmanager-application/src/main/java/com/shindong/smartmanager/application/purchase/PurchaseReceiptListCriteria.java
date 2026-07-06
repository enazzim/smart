package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record PurchaseReceiptListCriteria(
        String partnerName,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        String itemNum,
        String itemName
) {
}
