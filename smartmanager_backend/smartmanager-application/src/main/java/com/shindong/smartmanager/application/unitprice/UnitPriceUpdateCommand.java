package com.shindong.smartmanager.application.unitprice;

import java.math.BigDecimal;
import java.time.LocalDate;

public record UnitPriceUpdateCommand(
        BigDecimal orderRate,
        BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        LocalDate beginDate,
        LocalDate endDate,
        String updateReason
) {
}
