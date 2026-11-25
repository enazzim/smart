package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public record StocktakeBookLine(
        String itemNo,
        String itemName,
        String propertyClassification,
        String locationCode,
        String locationName,
        Long outputProcessId,
        String processName,
        BigDecimal bookQty,
        String error,
        String partnerNo,
        String partnerName,
        Long inputProcessId,
        String lotNo,
        long itemId,
        boolean lotTracked
) {
}
