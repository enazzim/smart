package com.shindong.smartmanager.application.workcenter;

import java.time.Instant;
import java.util.List;
import java.util.Optional;

public interface WorkCenterRepository {

    long save(WorkCenterCommand command, String actorUserId);

    void update(long id, WorkCenterCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);

    List<WorkCenterView> findAllActive(String query);

    Optional<WorkCenterView> findActiveById(long id);

    boolean existsActiveByWcName(String wcName, Long excludeId);

    boolean isReferencedByActiveProcess(long workCenterId);
}
