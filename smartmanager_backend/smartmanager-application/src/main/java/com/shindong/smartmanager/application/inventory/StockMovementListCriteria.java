package com.shindong.smartmanager.application.inventory;

import java.time.LocalDate;

public record StockMovementListCriteria(
        Long itemId,
        String itemNo,
        String locationCode,
        String referenceType,
        LocalDate movementDateFrom,
        LocalDate movementDateTo
) {
}
