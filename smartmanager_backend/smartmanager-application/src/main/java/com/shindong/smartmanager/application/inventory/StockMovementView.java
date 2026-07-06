package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record StockMovementView(
        long id,
        long inventoryBalanceId,
        long itemId,
        long locationId,
        int fiscalYear,
        int fiscalMonth,
        StockMovementType movementType,
        BigDecimal qty,
        BigDecimal amount,
        String referenceType,
        long referenceId,
        LocalDate movementDate
) {
}
