package com.shindong.smartmanager.application.inventory;

import java.util.Optional;

public interface StockMovementRepository {

    StockMovementView save(StockMovementView movement);

    Optional<Long> findActiveLotIdByReference(String referenceType, long referenceId);
}
