package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesShipmentStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record SalesShipmentView(
        long id,
        String shipmentNo,
        long partnerId,
        String partnerName,
        LocalDate shipmentDate,
        Long salesOrderId,
        SalesShipmentStatus status,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<SalesShipmentLineView> lines
) {
}
