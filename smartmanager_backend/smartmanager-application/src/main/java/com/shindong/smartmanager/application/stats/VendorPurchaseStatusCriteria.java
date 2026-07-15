package com.shindong.smartmanager.application.stats;

import java.time.LocalDate;

public record VendorPurchaseStatusCriteria(
        Long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        String modelType,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        Integer fiscalYear,
        Integer fiscalMonth,
        String division
) {
}
