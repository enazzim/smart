package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;

public record SalesRevenueLineSaveCommand(
        long salesShipmentLineId,
        BigDecimal revenueQty,
        long itemId,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long lotId
) {
}
