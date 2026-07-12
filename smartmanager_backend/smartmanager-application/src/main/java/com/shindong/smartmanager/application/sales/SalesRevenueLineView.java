package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;

public record SalesRevenueLineView(
        long id,
        int lineNo,
        long salesShipmentLineId,
        String shipmentNo,
        String orderNo,
        long partnerId,
        String partnerName,
        String itemNo,
        String itemName,
        BigDecimal revenueQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long lotId
) {
}
