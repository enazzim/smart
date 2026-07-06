package com.shindong.smartmanager.application.inventory;

import java.util.Optional;

public interface InventoryLocationQueryRepository {

    Optional<Long> findActiveLocationIdByCode(String locationCode);
}
