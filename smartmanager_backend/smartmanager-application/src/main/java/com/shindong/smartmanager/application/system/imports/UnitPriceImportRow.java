package com.shindong.smartmanager.application.system.imports;

import com.shindong.smartmanager.domain.pricing.CostType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record UnitPriceImportRow(
        CostType costType,
        String itemNum,
        String businessRegNo,
        String beginProcessSmallCode,
        String endProcessSmallCode,
        BigDecimal orderRate,
        BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        LocalDate beginDate,
        LocalDate endDate
) {
}
