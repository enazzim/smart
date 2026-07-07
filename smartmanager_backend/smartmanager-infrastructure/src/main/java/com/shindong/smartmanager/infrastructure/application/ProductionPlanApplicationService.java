package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.production.ProductionPlanCreateLineCommand;
import com.shindong.smartmanager.application.production.ProductionPlanListCriteria;
import com.shindong.smartmanager.application.production.ProductionPlanService;
import com.shindong.smartmanager.application.production.ProductionPlanStandaloneCommand;
import com.shindong.smartmanager.application.production.ProductionPlanView;
import com.shindong.smartmanager.application.sales.SalesOrderLineListView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class ProductionPlanApplicationService {

    private final ProductionPlanService productionPlanService;

    public ProductionPlanApplicationService(ProductionPlanService productionPlanService) {
        this.productionPlanService = productionPlanService;
    }

    @Transactional(readOnly = true)
    public List<SalesOrderLineListView> listCandidates() {
        return productionPlanService.listCandidates();
    }

    @Transactional(readOnly = true)
    public List<ProductionPlanView> list(ProductionPlanListCriteria criteria) {
        return productionPlanService.list(criteria);
    }

    @Transactional(readOnly = true)
    public ProductionPlanView get(long id) {
        return productionPlanService.get(id);
    }

    @Transactional
    public List<ProductionPlanView> createPlans(List<ProductionPlanCreateLineCommand> lines, String actorUserId) {
        return productionPlanService.createPlans(lines, actorUserId);
    }

    @Transactional
    public List<ProductionPlanView> createStandalonePlans(
            List<ProductionPlanStandaloneCommand> commands,
            String actorUserId
    ) {
        return productionPlanService.createStandalonePlans(commands, actorUserId);
    }

    @Transactional
    public ProductionPlanView cancelPlan(long id, String actorUserId) {
        return productionPlanService.cancelPlan(id, actorUserId);
    }
}
