package com.shindong.smartmanager.application.production;

import java.util.List;
import java.util.Optional;

public interface MrpRepository {

    long nextSequenceByRunNoPrefix(String prefix);

    long createRun(String runNo, String actorUserId);

    void saveLines(List<MaterialRequirementLineSaveCommand> lines, String actorUserId);

    List<MrpRunView> findAllActiveRuns();

    Optional<MrpRunView> findActiveRunById(long id);

    List<MaterialRequirementLineView> findActiveLinesByRunId(long mrpRunId);

    List<MaterialRequirementLineView> findAllActiveLines();

    Optional<MaterialRequirementLineView> findActiveLineById(long id);

    List<MaterialRequirementLineView> findActiveLinesByProductionPlanId(long productionPlanId);

    List<Long> findDistinctPlanIdsByRunId(long mrpRunId);

    long countActiveLinesByProductionPlanId(long productionPlanId);

    long countActiveLinesByRunId(long mrpRunId);

    void deleteLinesByRunId(long mrpRunId);

    void deleteLinesByProductionPlanId(long productionPlanId);

    void deleteLineById(long lineId);

    void deleteRunById(long mrpRunId);

    boolean deleteRunIfEmpty(long mrpRunId);
}
