package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreateOutsourcingAdvanceShipmentCommand(
        LocalDate shipmentDate,
        long partnerId,
        List<CreateOutsourcingAdvanceShipmentLineCommand> lines
) {
}
