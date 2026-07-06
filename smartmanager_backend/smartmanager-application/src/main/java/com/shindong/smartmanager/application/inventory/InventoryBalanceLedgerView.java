package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;
import java.util.List;

public record InventoryBalanceLedgerView(
        long balanceId,
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        int fiscalYear,
        BigDecimal stockQty,
        BigDecimal stockAmount,
        List<InventoryBalanceMonthlyView> months
) {
}
