package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;

public record SalesShipmentLineView(
        long id,
        int lineNo,
        long salesOrderLineId,
        String orderNo,
        long partnerId,
        String partnerName,
        String itemNo,
        String itemName,
        BigDecimal shipmentQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long lotId
) {
}
