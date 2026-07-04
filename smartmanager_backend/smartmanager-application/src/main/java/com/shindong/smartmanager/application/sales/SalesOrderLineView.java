package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesOrderLineView(
        long id,
        int lineNo,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate deliveryDate,
        SalesFulfillmentRoute fulfillmentRoute,
        SalesLineFulfillmentStatus fulfillmentStatus,
        SalesLineDeliveryStatus deliveryStatus
) {
}
