package com.shindong.smartmanager.application.bom;

import java.math.BigDecimal;

public record ItemCompositionCommand(
        long parentItemId,
        long childItemId,
        BigDecimal parentQuantity,
        BigDecimal childQuantity
) {
}
