package com.shindong.smartmanager.application.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record UnitPriceCommand(
        CostType costType,
        long itemId,
        long companyId,
        Long beginProcessCodeId,
        Long endProcessCodeId,
        BigDecimal orderRate,
        BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        LocalDate beginDate,
        LocalDate endDate
) {
}
