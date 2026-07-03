package com.shindong.smartmanager.application.unitprice;

/**
 * Ref: docs/step0/d4-unit-price.md §3.7
 */
public class UnitPriceHistoryProjector {

    private final UnitPriceRepository unitPriceRepository;

    public UnitPriceHistoryProjector(UnitPriceRepository unitPriceRepository) {
        this.unitPriceRepository = unitPriceRepository;
    }

    public void snapshotBeforeUpdate(long unitPriceId, String updateReason, String actorUserId) {
        unitPriceRepository.appendChangeLog(unitPriceId, updateReason, actorUserId);
    }
}
