package com.shindong.smartmanager.application.bom;

/**
 * Ref: docs/step0/d4-bom-line.md §3
 */
public class BomHistoryProjector {

    private final ItemCompositionRepository itemCompositionRepository;

    public BomHistoryProjector(ItemCompositionRepository itemCompositionRepository) {
        this.itemCompositionRepository = itemCompositionRepository;
    }

    public void snapshotOnRegister(long itemCompositionId, String actorUserId) {
        itemCompositionRepository.appendChangeLog(itemCompositionId, "INITIAL", actorUserId);
    }

    public void snapshotOnUpdate(long itemCompositionId, String actorUserId) {
        itemCompositionRepository.appendChangeLog(itemCompositionId, "QUANTITY_CHANGE", actorUserId);
    }

    public void snapshotOnDelete(long itemCompositionId, String actorUserId) {
        itemCompositionRepository.appendChangeLog(itemCompositionId, "DELETE", actorUserId);
    }
}
