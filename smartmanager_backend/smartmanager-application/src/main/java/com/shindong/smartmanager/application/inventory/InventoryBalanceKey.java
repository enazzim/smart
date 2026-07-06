package com.shindong.smartmanager.application.inventory;

public record InventoryBalanceKey(
        long itemId,
        long locationId,
        int fiscalYear,
        Long outputProcessId,
        Long inputProcessId,
        Long partnerId
) {
}
