package com.shindong.smartmanager.application.stats;

import java.time.LocalDate;

public record ItemStockMovementCriteria(
        Long itemId,
        String itemNo,
        Long companyId,
        String locationCode,
        Long outputProcessId,
        LocalDate movementDateFrom,
        LocalDate movementDateTo
) {
}
