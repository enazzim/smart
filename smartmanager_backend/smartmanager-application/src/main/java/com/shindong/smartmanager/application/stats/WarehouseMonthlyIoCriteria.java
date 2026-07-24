package com.shindong.smartmanager.application.stats;

public record WarehouseMonthlyIoCriteria(
        String locationCode,
        Long itemId,
        String itemNo,
        int fiscalYear,
        int fiscalMonth
) {
}
