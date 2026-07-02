package com.shindong.smartmanager.application.process;

public interface InventoryBalanceRepository {

    void ensureWipBalance(long itemId, int fiscalYear, long outputProcessId, String actorUserId);

    void updateWipItemId(long outputProcessId, long itemId, String actorUserId);

    void deactivateWipBalance(long outputProcessId, String actorUserId);
}
