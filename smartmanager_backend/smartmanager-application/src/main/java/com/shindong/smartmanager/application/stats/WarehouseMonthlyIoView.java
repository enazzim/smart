package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;

public record WarehouseMonthlyIoView(
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        Integer outputProcessSequence,
        String outputProcessName,
        int fiscalYear,
        int fiscalMonth,
        BigDecimal carryInQty,
        BigDecimal inQty,
        BigDecimal outQty,
        BigDecimal endingQty
) {
}
