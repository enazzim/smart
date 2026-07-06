package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.production.MaterialRequirementGroupedResult;
import com.shindong.smartmanager.application.production.MaterialRequirementLineView;
import com.shindong.smartmanager.application.production.MrpCancelPlanResult;
import com.shindong.smartmanager.application.production.MrpRunView;
import com.shindong.smartmanager.application.production.MrpService;
import com.shindong.smartmanager.application.production.ProductionPlanView;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class MrpApplicationService {

    private final MrpService mrpService;

    public MrpApplicationService(MrpService mrpService) {
        this.mrpService = mrpService;
    }

    @Transactional(readOnly = true)
    public List<ProductionPlanView> listCalculationTargets() {
        return mrpService.listCalculationTargets();
    }

    @Transactional(readOnly = true)
    public List<MrpRunView> listRuns() {
        return mrpService.listRuns();
    }

    @Transactional(readOnly = true)
    public MrpRunView getRun(long id) {
        return mrpService.getRun(id);
    }

    @Transactional(readOnly = true)
    public List<MaterialRequirementLineView> listLinesByRun(long mrpRunId) {
        return mrpService.listLinesByRun(mrpRunId);
    }

    @Transactional(readOnly = true)
    public List<MaterialRequirementLineView> listAllLines() {
        return mrpService.listAllLines();
    }

    @Transactional(readOnly = true)
    public MaterialRequirementGroupedResult listGroupedLines(Optional<Long> mrpRunId) {
        return mrpService.listGroupedLines(mrpRunId);
    }

    @Transactional
    public MrpRunView calculate(List<Long> productionPlanIds, String actorUserId) {
        return mrpService.calculate(productionPlanIds, actorUserId);
    }

    @Transactional
    public MrpRunView cancelRun(long runId, String actorUserId) {
        return mrpService.cancelRun(runId, actorUserId);
    }

    @Transactional
    public MrpCancelPlanResult cancelPlan(long productionPlanId, String actorUserId) {
        return mrpService.cancelPlan(productionPlanId, actorUserId);
    }

    @Transactional
    public MaterialRequirementLineView cancelLine(long lineId, String actorUserId) {
        return mrpService.cancelLine(lineId, actorUserId);
    }
}
