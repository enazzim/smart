package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreateOutsourcingShipmentCommand(
        LocalDate shipmentDate,
        List<CreateOutsourcingShipmentLineCommand> lines
) {
}
