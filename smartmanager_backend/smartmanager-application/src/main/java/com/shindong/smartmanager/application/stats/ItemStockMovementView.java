package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;
import java.time.LocalDate;

public record ItemStockMovementView(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        Integer outputProcessSequence,
        String outputProcessName,
        Long partnerId,
        String partnerName,
        String movementType,
        BigDecimal inQty,
        BigDecimal outQty,
        BigDecimal amount,
        String referenceType,
        long referenceId,
        LocalDate movementDate
) {
}
