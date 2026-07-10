package com.shindong.smartmanager.application.inventory;

import java.util.List;
import java.util.Optional;

public interface MiscStockMovementRepository {

    long countByMovementNoPrefix(String prefix);

    MiscStockMovementView save(MiscStockMovementSaveCommand command, String movementNo, String actorUserId);

    Optional<MiscStockMovementView> findActiveById(long id);

    List<MiscStockMovementView> findAllActive(MiscStockMovementListCriteria criteria);

    MiscStockMovementView update(long id, MiscStockMovementSaveCommand command, String actorUserId);

    void delete(long id);
}
