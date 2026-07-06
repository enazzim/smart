package com.shindong.smartmanager.application.inventory;

import java.time.Year;
import java.util.List;

public class InventoryLedgerQueryService {

    private final InventoryLedgerRepository ledgerRepository;

    public InventoryLedgerQueryService(InventoryLedgerRepository ledgerRepository) {
        this.ledgerRepository = ledgerRepository;
    }

    public List<StockMovementListItemView> listStockMovements(StockMovementListCriteria criteria) {
        return ledgerRepository.findStockMovements(criteria);
    }

    public List<InventoryBalanceLedgerView> listBalances(InventoryBalanceListCriteria criteria) {
        InventoryBalanceListCriteria effective = criteria != null
                ? criteria
                : new InventoryBalanceListCriteria(null, null, null);
        if (effective.fiscalYear() == null) {
            effective = new InventoryBalanceListCriteria(
                    effective.itemNo(),
                    effective.locationCode(),
                    Year.now().getValue()
            );
        }
        return ledgerRepository.findBalances(effective);
    }
}
