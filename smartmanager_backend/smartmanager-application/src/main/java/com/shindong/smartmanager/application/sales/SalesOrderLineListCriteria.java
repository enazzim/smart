package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import java.time.LocalDate;

public record SalesOrderLineListCriteria(
        Long partnerId,
        Long itemId,
        LocalDate requestedDeliveryDateFrom,
        LocalDate requestedDeliveryDateTo,
        SalesLineFulfillmentStatus fulfillmentStatus,
        SalesLineDeliveryStatus deliveryStatus
) {
}
