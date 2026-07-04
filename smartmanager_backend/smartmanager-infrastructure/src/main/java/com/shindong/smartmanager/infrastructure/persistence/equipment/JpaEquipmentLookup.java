package com.shindong.smartmanager.infrastructure.persistence.equipment;

import com.shindong.smartmanager.application.equipment.EquipmentLookup;
import org.springframework.stereotype.Repository;

@Repository
public class JpaEquipmentLookup implements EquipmentLookup {

    private final SpringDataEquipmentRepository equipmentRepository;

    public JpaEquipmentLookup(SpringDataEquipmentRepository equipmentRepository) {
        this.equipmentRepository = equipmentRepository;
    }

    @Override
    public boolean existsActive(long equipmentId) {
        return equipmentRepository.findByIdAndRecordingState(equipmentId, 1).isPresent();
    }

    @Override
    public String findActiveName(long equipmentId) {
        return equipmentRepository.findByIdAndRecordingState(equipmentId, 1)
                .map(EquipmentJpaEntity::getEquipmentName)
                .orElse(null);
    }
}
