package com.shindong.smartmanager.application.equipment;

public record EquipmentUpdateCommand(
        String equipmentName,
        long equipmentCategoryId,
        Long workCenterId,
        int designShot,
        int initialShot
) {
}
