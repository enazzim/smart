package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;

public record OutsourcingReceiptLineView(
        long id,
        int lineNo,
        long outsourcingOrderLineId,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal receiptQty,
        BigDecimal postedQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long qualityInspectionId,
        boolean stockPosted,
        Long lotId
) {
}
