package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesShipmentLineRevenueContext(
        long shipmentId,
        long shipmentLineId,
        String shipmentNo,
        LocalDate shipmentDate,
        long partnerId,
        String partnerName,
        long salesOrderLineId,
        String orderNo,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal shipmentQty,
        BigDecimal invoicedQty,
        BigDecimal unitPrice
) {
    public BigDecimal remainingQty() {
        return shipmentQty.subtract(invoicedQty);
    }
}
