package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesOrderLineShipmentContext(
        long salesOrderId,
        long salesOrderLineId,
        String orderNo,
        long partnerId,
        String partnerName,
        SalesOrderStatus orderStatus,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal unitPrice,
        LocalDate deliveryDate
) {
    public BigDecimal remainingQty() {
        return orderQty.subtract(shippedQty);
    }
}
