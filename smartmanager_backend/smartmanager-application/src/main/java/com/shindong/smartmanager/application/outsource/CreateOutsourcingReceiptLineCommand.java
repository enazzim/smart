package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;

public record CreateOutsourcingReceiptLineCommand(
        long outsourcingOrderLineId,
        BigDecimal receiptQty
) {
}
