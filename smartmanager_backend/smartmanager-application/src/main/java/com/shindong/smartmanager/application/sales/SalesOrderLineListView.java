package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesOrderLineListView(
        long orderId,
        long lineId,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        LocalDate requestedDeliveryDate,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal unitPrice,
        BigDecimal orderQty,
        BigDecimal amount,
        SalesFulfillmentRoute fulfillmentRoute,
        SalesLineFulfillmentStatus fulfillmentStatus,
        SalesLineDeliveryStatus deliveryStatus,
        SalesOrderStatus orderStatus,
        boolean orderEditable
) {
}
