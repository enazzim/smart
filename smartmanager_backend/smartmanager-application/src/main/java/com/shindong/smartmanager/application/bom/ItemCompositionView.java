package com.shindong.smartmanager.application.bom;

import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record ItemCompositionView(
        long id,
        long parentItemId,
        String parentItemNo,
        String parentItemName,
        long childItemId,
        String childItemNo,
        String childItemName,
        BigDecimal parentQuantity,
        BigDecimal childQuantity,
        LocalDate beginDate,
        LocalDate endDate,
        Instant createdAt
) {
}
