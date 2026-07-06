package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseOrderLineCommand(
        long itemId,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        Long requirementLineId,
        LocalDate requestedDeliveryDate
) {
    public PurchaseOrderLineCommand(long itemId, BigDecimal orderQty, BigDecimal unitPrice, Long requirementLineId) {
        this(itemId, orderQty, unitPrice, requirementLineId, null);
    }
}
