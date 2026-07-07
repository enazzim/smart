package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;

public record CreateSalesShipmentLineCommand(
        long salesOrderLineId,
        BigDecimal shipmentQty
) {
}
