package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesShipmentStatus;
import java.time.LocalDate;
import java.util.List;

public record SalesShipmentSaveCommand(
        String shipmentNo,
        long partnerId,
        LocalDate shipmentDate,
        Long salesOrderId,
        List<SalesShipmentLineSaveCommand> lines
) {
}
