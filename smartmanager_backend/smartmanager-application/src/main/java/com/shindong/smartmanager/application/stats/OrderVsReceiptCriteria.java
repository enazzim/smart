package com.shindong.smartmanager.application.stats;

import java.time.LocalDate;

public record OrderVsReceiptCriteria(
        Long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        String division
) {
}
