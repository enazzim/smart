package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

public record OutsourcingShipmentSaveCommand(
        String shipmentNo,
        LocalDate shipmentDate,
        List<OutsourcingShipmentLineSaveCommand> lines
) {
}
