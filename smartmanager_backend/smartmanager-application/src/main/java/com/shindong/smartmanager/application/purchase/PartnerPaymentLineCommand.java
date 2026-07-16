package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;

public record PartnerPaymentLineCommand(
        long itemId,
        Long purchaseOrderLineId,
        Long outsourcingOrderLineId,
        BigDecimal supplyAmount,
        BigDecimal vatAmount
) {
}
