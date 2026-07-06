package com.shindong.smartmanager.application.inventory;

public interface InventoryStockBalanceRepository {

    InventoryBalanceSlotView ensureBalance(InventoryBalanceKey key);

    InventoryBalanceSlotView saveBalance(InventoryBalanceSlotView balance);
}
