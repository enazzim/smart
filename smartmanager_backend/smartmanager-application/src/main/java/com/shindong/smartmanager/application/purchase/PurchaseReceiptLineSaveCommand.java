package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;

public record PurchaseReceiptLineSaveCommand(
        long purchaseOrderLineId,
        long itemId,
        BigDecimal receiptQty,
        BigDecimal postedQty,
        BigDecimal unitPrice,
        BigDecimal amount
) {
}
