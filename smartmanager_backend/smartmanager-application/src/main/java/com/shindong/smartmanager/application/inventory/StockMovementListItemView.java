package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record StockMovementListItemView(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        Integer outputProcessSequence,
        String outputProcessName,
        StockMovementType movementType,
        BigDecimal qty,
        BigDecimal amount,
        String referenceType,
        long referenceId,
        LocalDate movementDate,
        int fiscalYear,
        int fiscalMonth
) {
}
