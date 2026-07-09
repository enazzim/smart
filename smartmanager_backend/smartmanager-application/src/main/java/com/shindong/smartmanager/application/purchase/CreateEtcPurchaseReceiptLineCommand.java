package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;

public record CreateEtcPurchaseReceiptLineCommand(
        long etcPurchaseOrderId,
        BigDecimal receiptQty
) {
}
