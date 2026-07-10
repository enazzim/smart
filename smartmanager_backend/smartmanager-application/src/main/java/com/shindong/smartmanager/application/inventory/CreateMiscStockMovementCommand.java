package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import java.math.BigDecimal;
import java.time.LocalDate;

public record CreateMiscStockMovementCommand(
        LocalDate movementDate,
        MiscStockMovementDirection movementDirection,
        long itemId,
        Long processSequenceId,
        BigDecimal qty,
        Long reasonCodeId,
        String note
) {
}
