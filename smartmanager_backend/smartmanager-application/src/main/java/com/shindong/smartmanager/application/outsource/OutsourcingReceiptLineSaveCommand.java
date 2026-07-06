package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;

public record OutsourcingReceiptLineSaveCommand(
        long outsourcingOrderLineId,
        long itemId,
        BigDecimal receiptQty,
        BigDecimal postedQty,
        BigDecimal unitPrice,
        BigDecimal amount
) {
}
