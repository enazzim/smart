package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentStatus;
import java.time.LocalDate;

public record OutsourcingShipmentListCriteria(
        LocalDate shipmentDateFrom,
        LocalDate shipmentDateTo,
        String shipmentNo,
        String partnerName,
        OutsourcingShipmentStatus status,
        boolean excludeCancelled
) {
}
