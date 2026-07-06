package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.production.ProductionPlanView;
import com.shindong.smartmanager.application.production.WorkPlanCancelPlanResult;
import com.shindong.smartmanager.application.production.WorkPlanListCriteria;
import com.shindong.smartmanager.application.production.WorkPlanService;
import com.shindong.smartmanager.application.production.WorkPlanView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class WorkPlanApplicationService {

    private final WorkPlanService workPlanService;

    public WorkPlanApplicationService(WorkPlanService workPlanService) {
        this.workPlanService = workPlanService;
    }

    @Transactional(readOnly = true)
    public List<ProductionPlanView> listPlanningTargets() {
        return workPlanService.listPlanningTargets();
    }

    @Transactional(readOnly = true)
    public List<WorkPlanView> list(WorkPlanListCriteria criteria) {
        return workPlanService.list(criteria);
    }

    @Transactional
    public List<WorkPlanView> createPlans(List<Long> productionPlanIds, String actorUserId) {
        return workPlanService.createPlans(productionPlanIds, actorUserId);
    }

    @Transactional
    public WorkPlanCancelPlanResult cancelPlan(long productionPlanId, String actorUserId) {
        return workPlanService.cancelPlan(productionPlanId, actorUserId);
    }

    @Transactional
    public WorkPlanView cancelLine(long id, String actorUserId) {
        return workPlanService.cancelLine(id, actorUserId);
    }
}
