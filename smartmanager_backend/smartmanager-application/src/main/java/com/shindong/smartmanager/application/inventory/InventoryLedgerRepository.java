package com.shindong.smartmanager.application.inventory;

import java.util.List;

public interface InventoryLedgerRepository {

    List<StockMovementListItemView> findStockMovements(StockMovementListCriteria criteria);

    List<InventoryBalanceLedgerView> findBalances(InventoryBalanceListCriteria criteria);
}
