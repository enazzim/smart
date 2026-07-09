package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcPurchaseReceiptView(
        long id,
        String receiptNo,
        long etcPurchaseOrderId,
        String orderNo,
        long partnerId,
        String partnerName,
        String itemName,
        BigDecimal receiptQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate receiptDate,
        int fiscalYear,
        int fiscalMonth
) {
}
