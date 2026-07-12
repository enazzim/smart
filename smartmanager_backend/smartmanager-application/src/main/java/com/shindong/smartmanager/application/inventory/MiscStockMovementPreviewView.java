package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;

public record MiscStockMovementPreviewView(
        long itemId,
        String itemNo,
        String itemName,
        PropertyClassification propertyClassification,
        String locationCode,
        String locationLabel,
        Long outputProcessId,
        Short outputProcessSequence,
        String outputProcessName,
        boolean processRequired,
        BigDecimal onHandQty,
        boolean lotTracked
) {
}
