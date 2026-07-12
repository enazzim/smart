package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;

public record SalesShipmentLineSaveCommand(
        long salesOrderLineId,
        BigDecimal shipmentQty,
        long itemId,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long lotId
) {
}
