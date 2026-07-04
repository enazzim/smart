package com.shindong.smartmanager.api.web.equipment;

import com.shindong.smartmanager.application.equipment.EquipmentView;
import java.time.Instant;

public record EquipmentResponse(
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
    static EquipmentResponse from(EquipmentView view) {
        return new EquipmentResponse(
                view.id(),
                view.equipmentNum(),
                view.equipmentName(),
                view.equipmentCategoryId(),
                view.equipmentCategoryName(),
                view.categoryCode(),
                view.workCenterId(),
                view.wcName(),
                view.designShot(),
                view.initialShot(),
                view.workShot(),
                view.accumulatedShot(),
                view.replacementDue(),
                view.createdAt()
        );
    }
}
