package com.shindong.smartmanager.api.web.unitprice;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;

public record UpdateUnitPriceRequest(
        @NotNull BigDecimal orderRate,
        @NotNull BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        @NotNull LocalDate beginDate,
        LocalDate endDate,
        @NotBlank String updateReason
) {
}
