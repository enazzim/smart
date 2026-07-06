package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record PurchaseReceiptCandidateCriteria(
        String partnerName,
        String orderNo,
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        String itemNum,
        String itemName
) {
}
