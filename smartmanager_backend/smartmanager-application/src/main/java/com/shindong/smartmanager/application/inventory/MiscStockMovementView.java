package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementStatus;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record MiscStockMovementView(
        long id,
        String movementNo,
        LocalDate movementDate,
        MiscStockMovementDirection movementDirection,
        long itemId,
        String itemNo,
        String itemName,
        PropertyClassification propertyClassification,
        String locationCode,
        String locationLabel,
        Long outputProcessId,
        Short outputProcessSequence,
        String outputProcessName,
        BigDecimal qty,
        Long reasonCodeId,
        String reasonLabel,
        String note,
        MiscStockMovementStatus status,
        Instant createdAt
) {
}
