package com.shindong.smartmanager.application.system.imports;

import java.math.BigDecimal;

public record ItemCompositionImportRow(
        String parentItemNum,
        String childItemNum,
        BigDecimal parentQuantity,
        BigDecimal childQuantity
) {
}
