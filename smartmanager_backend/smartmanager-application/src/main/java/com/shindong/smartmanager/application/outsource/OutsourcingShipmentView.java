package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentType;
import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingShipmentView(
        long id,
        String shipmentNo,
        LocalDate shipmentDate,
        OutsourcingShipmentType shipmentType,
        Long partnerId,
        String partnerName,
        OutsourcingShipmentStatus status,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<OutsourcingShipmentLineView> lines
) {
}
