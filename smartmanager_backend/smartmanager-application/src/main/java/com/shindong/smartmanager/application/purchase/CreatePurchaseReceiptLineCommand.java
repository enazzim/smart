package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;

public record CreatePurchaseReceiptLineCommand(
        long purchaseOrderLineId,
        BigDecimal receiptQty,
        String lotNo,
        boolean autoGenerateLot
) {
    public CreatePurchaseReceiptLineCommand(long purchaseOrderLineId, BigDecimal receiptQty) {
        this(purchaseOrderLineId, receiptQty, null, false);
    }

    public CreatePurchaseReceiptLineCommand {
        if (receiptQty == null || receiptQty.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("입고 수량은 0보다 커야 합니다.");
        }
    }
}
