package com.shindong.smartmanager.api.web.bom;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import jakarta.validation.constraints.PositiveOrZero;
import java.math.BigDecimal;

public record CreateItemCompositionRequest(
        @NotBlank String parentItemNum,
        @NotBlank String childItemNum,
        @NotNull @Positive BigDecimal parentQuantity,
        @NotNull @PositiveOrZero BigDecimal childQuantity
) {
}
