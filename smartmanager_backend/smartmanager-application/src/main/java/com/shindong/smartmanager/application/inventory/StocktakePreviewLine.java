package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public record StocktakePreviewLine(
        int rowNo,
        String itemNo,
        String itemName,
        String locationCode,
        Long outputProcessId,
        String processName,
        BigDecimal bookQty,
        BigDecimal countedQty,
        String action,
        BigDecimal movementQty,
        String reasonName,
        String error,
        Long inputProcessId,
        Long partnerId,
        String partnerNo,
        String lotNo,
        Long lotId
) {
}
