package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreateSalesShipmentCommand(
        LocalDate shipmentDate,
        List<CreateSalesShipmentLineCommand> lines
) {
}
