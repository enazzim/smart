package com.shindong.smartmanager.application.inventory;

import java.time.LocalDate;

public record MiscStockMovementListCriteria(
        String itemNo,
        String itemName,
        LocalDate movementDateFrom,
        LocalDate movementDateTo
) {
}
