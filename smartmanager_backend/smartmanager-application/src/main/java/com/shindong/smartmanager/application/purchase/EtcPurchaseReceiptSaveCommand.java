package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcPurchaseReceiptSaveCommand(
        long etcPurchaseOrderId,
        long partnerId,
        String itemName,
        BigDecimal receiptQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate receiptDate,
        int fiscalYear,
        int fiscalMonth
) {
}
