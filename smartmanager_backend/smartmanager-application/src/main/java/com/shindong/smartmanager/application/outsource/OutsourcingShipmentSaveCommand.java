package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentType;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingShipmentSaveCommand(
        String shipmentNo,
        LocalDate shipmentDate,
        OutsourcingShipmentType shipmentType,
        Long partnerId,
        List<OutsourcingShipmentLineSaveCommand> lines
) {
}
