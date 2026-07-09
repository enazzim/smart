package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.outsource.CreateOutsourcingOrderFromWorkPlanCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderListCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderPrintView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderService;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderView;
import com.shindong.smartmanager.application.outsource.WorkPlanOutsourceCandidateView;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class OutsourcingOrderApplicationService {

    private final OutsourcingOrderService outsourcingOrderService;

    public OutsourcingOrderApplicationService(OutsourcingOrderService outsourcingOrderService) {
        this.outsourcingOrderService = outsourcingOrderService;
    }

    @Transactional(readOnly = true)
    public List<OutsourcingOrderView> list(OutsourcingOrderListCriteria criteria) {
        return outsourcingOrderService.list(criteria);
    }

    @Transactional(readOnly = true)
    public OutsourcingOrderView get(long id) {
        return outsourcingOrderService.get(id);
    }

    @Transactional(readOnly = true)
    public OutsourcingOrderPrintView getPrintView(long id) {
        return outsourcingOrderService.getPrintView(id);
    }

    @Transactional(readOnly = true)
    public List<OutsourcingOrderPrintView> getBatchPrintViews(List<Long> orderIds) {
        return outsourcingOrderService.getBatchPrintViews(orderIds);
    }

    @Transactional(readOnly = true)
    public List<WorkPlanOutsourceCandidateView> listWorkPlanCandidates(LocalDate orderDate) {
        return outsourcingOrderService.listWorkPlanCandidates(orderDate);
    }

    @Transactional(readOnly = true)
    public String previewNextOrderNo(LocalDate orderDate) {
        return outsourcingOrderService.previewNextOrderNo(orderDate);
    }

    @Transactional
    public OutsourcingOrderView register(OutsourcingOrderCommand command, String actorUserId) {
        return outsourcingOrderService.register(command, actorUserId);
    }

    @Transactional
    public OutsourcingOrderView registerFromWorkPlan(
            CreateOutsourcingOrderFromWorkPlanCommand command,
            String actorUserId
    ) {
        return outsourcingOrderService.registerFromWorkPlan(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        outsourcingOrderService.cancel(id, actorUserId);
    }
}
