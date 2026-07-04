package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesOrderLineCommand(
        long itemId,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        LocalDate deliveryDate
) {
}
