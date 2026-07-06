package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public record InventoryBalanceMonthlyView(
        int monthNum,
        BigDecimal inQty,
        BigDecimal outQty,
        BigDecimal stockQty
) {
}
