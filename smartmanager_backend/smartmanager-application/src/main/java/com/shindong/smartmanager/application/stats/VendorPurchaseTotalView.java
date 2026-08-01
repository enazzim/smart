package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;

public record VendorPurchaseTotalView(
        long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        String division,
        BigDecimal purchaseQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        int fiscalYear,
        int fiscalMonth
) {
}
