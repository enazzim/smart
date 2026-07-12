package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import java.math.BigDecimal;
import java.time.LocalDate;

public record MiscStockMovementSaveCommand(
        LocalDate movementDate,
        MiscStockMovementDirection movementDirection,
        long itemId,
        String locationCode,
        Long outputProcessId,
        BigDecimal qty,
        Long reasonCodeId,
        String note,
        Long lotId
) {
}
