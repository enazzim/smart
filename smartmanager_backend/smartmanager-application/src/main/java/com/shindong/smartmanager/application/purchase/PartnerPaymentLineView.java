package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;

public record PartnerPaymentLineView(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        Long purchaseOrderLineId,
        Long outsourcingOrderLineId,
        String orderNo,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        BigDecimal offsetAmount,
        BigDecimal remainingAmount
) {
}
