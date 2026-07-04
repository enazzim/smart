package com.shindong.smartmanager.api.web.system.closing;

import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotNull;

public record CloseMonthClosingRequest(
        @NotNull @Min(2000) @Max(2100) Integer fiscalYear,
        @NotNull @Min(1) @Max(12) Integer fiscalMonth
) {
}
