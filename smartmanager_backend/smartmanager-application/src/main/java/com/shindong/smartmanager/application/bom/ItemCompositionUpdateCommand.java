package com.shindong.smartmanager.application.bom;

import java.math.BigDecimal;

public record ItemCompositionUpdateCommand(
        BigDecimal parentQuantity,
        BigDecimal childQuantity
) {
}
