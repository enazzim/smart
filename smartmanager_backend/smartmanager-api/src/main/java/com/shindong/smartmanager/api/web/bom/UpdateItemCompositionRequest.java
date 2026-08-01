package com.shindong.smartmanager.api.web.bom;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import jakarta.validation.constraints.PositiveOrZero;
import java.math.BigDecimal;

public record UpdateItemCompositionRequest(
        @NotNull @Positive BigDecimal parentQuantity,
        @NotNull @PositiveOrZero BigDecimal childQuantity
) {
}
