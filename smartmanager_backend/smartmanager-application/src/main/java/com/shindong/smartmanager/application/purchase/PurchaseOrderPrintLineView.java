package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseOrderPrintLineView(
        int lineNo,
        String itemNo,
        String itemName,
        String standard,
        String unit,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate requestedDeliveryDate
) {
}
