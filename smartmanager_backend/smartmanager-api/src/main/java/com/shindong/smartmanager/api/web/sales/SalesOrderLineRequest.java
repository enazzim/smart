package com.shindong.smartmanager.api.web.sales;

import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesOrderLineRequest(
        @NotNull Long itemId,
        @NotNull @DecimalMin("0.0001") BigDecimal orderQty,
        BigDecimal unitPrice,
        LocalDate deliveryDate
) {
}
