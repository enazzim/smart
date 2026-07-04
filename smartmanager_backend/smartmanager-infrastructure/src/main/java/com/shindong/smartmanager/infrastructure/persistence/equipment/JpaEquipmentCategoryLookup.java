package com.shindong.smartmanager.infrastructure.persistence.equipment;

import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.equipment.EquipmentCategoryLookup;
import org.springframework.stereotype.Repository;

@Repository
public class JpaEquipmentCategoryLookup implements EquipmentCategoryLookup {

    private static final String CODE_GROUP = "EQUIPMENT_CLASS";

    private final CodeGroupOptionsRepository codeGroupOptionsRepository;

    public JpaEquipmentCategoryLookup(CodeGroupOptionsRepository codeGroupOptionsRepository) {
        this.codeGroupOptionsRepository = codeGroupOptionsRepository;
    }

    @Override
    public java.util.Optional<CategoryInfo> findActiveCategory(long categoryId) {
        return codeGroupOptionsRepository.findActiveOptions(CODE_GROUP).stream()
                .filter(option -> option.id() == categoryId)
                .findFirst()
                .map(option -> new CategoryInfo(option.id(), option.code(), option.name()));
    }
}
