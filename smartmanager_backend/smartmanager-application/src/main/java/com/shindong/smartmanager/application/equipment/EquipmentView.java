package com.shindong.smartmanager.application.equipment;

import java.time.Instant;

public record EquipmentView(
        long id,
        String equipmentNum,
        String equipmentName,
        long equipmentCategoryId,
        String equipmentCategoryName,
        String categoryCode,
        Long workCenterId,
        String wcName,
        int designShot,
        int initialShot,
        int workShot,
        int accumulatedShot,
        boolean replacementDue,
        Instant createdAt
) {
}
