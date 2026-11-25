package com.shindong.smartmanager.application.inventory;

import java.util.List;
import java.util.Optional;

public interface StocktakeLotQuery {

    Optional<Long> findLotId(long itemId, String lotNo);

    List<StocktakeLotQty> listThrough(
            long itemId,
            String locationCode,
            int fiscalYear,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId
    );
}
