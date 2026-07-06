package com.shindong.smartmanager.application.inventory;

public record InventoryBalanceListCriteria(
        String itemNo,
        String locationCode,
        Integer fiscalYear
) {
}
