package com.shindong.smartmanager.application.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record UnitPriceChangeLogView(
        long id,
        long unitPriceId,
        CostType costType,
        long itemId,
        String itemNo,
        String itemName,
        long companyId,
        String companyName,
        Long beginProcessCodeId,
        String beginProcessCode,
        String beginProcessName,
        Long endProcessCodeId,
        String endProcessCode,
        String endProcessName,
        BigDecimal orderRate,
        BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        LocalDate beginDate,
        LocalDate endDate,
        String updateReason,
        String changedBy,
        String changedById,
        Instant changedAt
) {
}
