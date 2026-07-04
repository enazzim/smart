package com.shindong.smartmanager.application.equipment;

import java.util.Optional;

public interface EquipmentCategoryLookup {

    Optional<CategoryInfo> findActiveCategory(long categoryId);

    record CategoryInfo(long id, String smallCode, String smallName) {
    }
}
