package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public record InventoryBalanceSlotView(
        long id,
        long itemId,
        long locationId,
        int fiscalYear,
        Long outputProcessId,
        Long inputProcessId,
        Long partnerId,
        BigDecimal stockQty,
        BigDecimal stockAmount
) {
}
