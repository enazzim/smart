package com.shindong.smartmanager.api.web.bom;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import java.math.BigDecimal;

public record UpdateItemCompositionRequest(
        @NotNull @Positive BigDecimal parentQuantity,
        @NotNull @Positive BigDecimal childQuantity
) {
}
