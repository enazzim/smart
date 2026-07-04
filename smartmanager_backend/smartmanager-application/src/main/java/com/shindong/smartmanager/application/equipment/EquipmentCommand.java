package com.shindong.smartmanager.application.equipment;

public record EquipmentCommand(
        String equipmentNum,
        String equipmentName,
        long equipmentCategoryId,
        Long workCenterId,
        int designShot,
        int initialShot
) {
}
