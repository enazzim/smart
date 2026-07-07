package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesShipmentStatus;
import java.time.LocalDate;

public record SalesShipmentListCriteria(
        LocalDate shipmentDateFrom,
        LocalDate shipmentDateTo,
        String shipmentNo,
        String partnerName,
        SalesShipmentStatus status,
        boolean excludeCancelled
) {
}
