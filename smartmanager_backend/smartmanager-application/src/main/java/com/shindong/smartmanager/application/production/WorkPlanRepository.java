package com.shindong.smartmanager.application.production;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

public interface WorkPlanRepository {

    List<WorkPlanView> saveAll(List<WorkPlanSaveCommand> commands, String actorUserId);

    List<WorkPlanView> findAllActive(WorkPlanListCriteria criteria);

    Optional<WorkPlanView> findActivePlannedById(long id);

    Optional<WorkPlanView> findActiveById(long id);

    List<WorkPlanView> findActivePlannedByProductionPlanId(long productionPlanId);

    long countActivePlannedByProductionPlanId(long productionPlanId);

    void cancelById(long id, String actorUserId);

    void cancelByProductionPlanId(long productionPlanId, String actorUserId);

    List<WorkPlanView> findActivePlannedForLoad(Long workCenterId, LocalDate from, LocalDate to);
}
