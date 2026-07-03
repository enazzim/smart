package com.shindong.smartmanager.api.web.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;

public record CreateUnitPriceRequest(
        @NotNull CostType type,
        @NotBlank String itemNum,
        @NotNull Long companyId,
        Long beginProcessCodeId,
        Long endProcessCodeId,
        BigDecimal orderRate,
        @NotNull BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        @NotNull LocalDate beginDate,
        LocalDate endDate
) {
}
