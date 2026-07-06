package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;

public record PurchaseReceiptLineView(
        long id,
        int lineNo,
        long purchaseOrderLineId,
        long itemId,
        String itemNum,
        String itemName,
        BigDecimal receiptQty,
        BigDecimal postedQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long qualityInspectionId,
        boolean postedImmediately
) {
}
