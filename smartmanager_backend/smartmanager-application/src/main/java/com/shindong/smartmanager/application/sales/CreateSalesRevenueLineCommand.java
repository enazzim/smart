package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;

public record CreateSalesRevenueLineCommand(
        long salesShipmentLineId,
        BigDecimal revenueQty
) {
}
