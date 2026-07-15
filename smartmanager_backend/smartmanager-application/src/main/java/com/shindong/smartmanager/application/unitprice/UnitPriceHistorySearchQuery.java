package com.shindong.smartmanager.application.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
import java.time.LocalDate;

public record UnitPriceHistorySearchQuery(
        CostType costType,
        Long companyId,
        Long itemId,
        LocalDate changedFrom,
        LocalDate changedTo,
        String changedBy
) {
}
