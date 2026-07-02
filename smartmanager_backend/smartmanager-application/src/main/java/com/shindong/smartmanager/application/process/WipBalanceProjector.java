package com.shindong.smartmanager.application.process;

import java.time.Year;

/**
 * Ref: docs/step0/domain-event-projector-matrix.md §4, §4.5
 */
public class WipBalanceProjector {

    private final InventoryBalanceRepository inventoryBalanceRepository;

    public WipBalanceProjector(InventoryBalanceRepository inventoryBalanceRepository) {
        this.inventoryBalanceRepository = inventoryBalanceRepository;
    }

    public void ensure(long itemId, long outputProcessId, String actorUserId) {
        int fiscalYear = Year.now().getValue();
        inventoryBalanceRepository.ensureWipBalance(itemId, fiscalYear, outputProcessId, actorUserId);
    }

    public void reconcile(long itemId, long outputProcessId, String actorUserId) {
        ensure(itemId, outputProcessId, actorUserId);
    }

    public void updateItemId(long outputProcessId, long itemId, String actorUserId) {
        inventoryBalanceRepository.updateWipItemId(outputProcessId, itemId, actorUserId);
    }

    public void deactivate(long outputProcessId, String actorUserId) {
        inventoryBalanceRepository.deactivateWipBalance(outputProcessId, actorUserId);
    }
}
