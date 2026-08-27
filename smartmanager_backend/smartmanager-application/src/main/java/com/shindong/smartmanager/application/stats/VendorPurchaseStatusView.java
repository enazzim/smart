package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;
import java.time.LocalDate;

public record VendorPurchaseStatusView(
        long historyId,
        String ledgerKind,
        long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        String modelType,
        String processName,
        LocalDate receiptDate,
        BigDecimal currentStockQty,
        BigDecimal receiptQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        String division,
        int fiscalYear,
        int fiscalMonth
) {
}
