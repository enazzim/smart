package com.shindong.smartmanager.application.inventory;

public record InventoryBalanceListCriteria(
        Long itemId,
        String itemNo,
        String locationCode,
        Integer fiscalYear
) {
}
