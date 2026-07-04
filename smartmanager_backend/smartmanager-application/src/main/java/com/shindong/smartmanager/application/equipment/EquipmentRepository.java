package com.shindong.smartmanager.application.equipment;

import java.util.List;
import java.util.Optional;

public interface EquipmentRepository {

    long save(EquipmentCommand command, String actorUserId);

    void update(long id, EquipmentUpdateCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);

    List<EquipmentView> findAllActive(String query);

    Optional<EquipmentView> findActiveById(long id);

    boolean existsActiveByEquipmentNum(String equipmentNum, Long excludeId);

    boolean isReferencedByActiveWorkStandard(long equipmentId);

    boolean existsByWorkCenterIdAndRecordingState(long workCenterId);
}
