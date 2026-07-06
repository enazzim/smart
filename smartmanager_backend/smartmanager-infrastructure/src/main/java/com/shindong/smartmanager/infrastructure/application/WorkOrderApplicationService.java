package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.production.WorkOrderListCriteria;
import com.shindong.smartmanager.application.production.WorkOrderService;
import com.shindong.smartmanager.application.production.WorkOrderView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class WorkOrderApplicationService {

    private final WorkOrderService workOrderService;

    public WorkOrderApplicationService(WorkOrderService workOrderService) {
        this.workOrderService = workOrderService;
    }

    @Transactional(readOnly = true)
    public List<WorkOrderView> listOrderTargets() {
        return workOrderService.listOrderTargets();
    }

    @Transactional(readOnly = true)
    public List<WorkOrderView> list(WorkOrderListCriteria criteria) {
        return workOrderService.list(criteria);
    }

    @Transactional
    public List<WorkOrderView> createOrders(List<Long> workPlanIds, String actorUserId) {
        return workOrderService.createOrders(workPlanIds, actorUserId);
    }

    @Transactional
    public WorkOrderView cancel(long id, String actorUserId) {
        return workOrderService.cancel(id, actorUserId);
    }
}
