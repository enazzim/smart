package com.shindong.smartmanager.application.stats;

public record VendorPurchaseTotalCriteria(
        Long companyId,
        Long itemId,
        String itemNo,
        Integer fiscalYear,
        Integer fiscalMonth,
        String division
) {
}
