package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public interface InventoryBalanceMonthlyRepository {

    void applyMovement(long inventoryBalanceId, int monthNum, BigDecimal inQty, BigDecimal inAmount,
                       BigDecimal outQty, BigDecimal outAmount, BigDecimal stockQtyDelta);
}
