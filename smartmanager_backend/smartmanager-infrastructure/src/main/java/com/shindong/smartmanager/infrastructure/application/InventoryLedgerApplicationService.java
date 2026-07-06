package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.inventory.InventoryBalanceLedgerView;
import com.shindong.smartmanager.application.inventory.InventoryBalanceListCriteria;
import com.shindong.smartmanager.application.inventory.InventoryLedgerQueryService;
import com.shindong.smartmanager.application.inventory.StockMovementListCriteria;
import com.shindong.smartmanager.application.inventory.StockMovementListItemView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class InventoryLedgerApplicationService {

    private final InventoryLedgerQueryService ledgerQueryService;

    public InventoryLedgerApplicationService(InventoryLedgerQueryService ledgerQueryService) {
        this.ledgerQueryService = ledgerQueryService;
    }

    @Transactional(readOnly = true)
    public List<StockMovementListItemView> listStockMovements(StockMovementListCriteria criteria) {
        return ledgerQueryService.listStockMovements(criteria);
    }

    @Transactional(readOnly = true)
    public List<InventoryBalanceLedgerView> listBalances(InventoryBalanceListCriteria criteria) {
        return ledgerQueryService.listBalances(criteria);
    }
}
