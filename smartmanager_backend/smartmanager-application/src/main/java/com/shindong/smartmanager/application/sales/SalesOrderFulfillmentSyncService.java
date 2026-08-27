package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessSequenceNavigator;
import com.shindong.smartmanager.application.production.ProductionPlanRepository;
import com.shindong.smartmanager.application.production.ProductionPlanView;
import com.shindong.smartmanager.application.production.WorkOrderRepository;
import com.shindong.smartmanager.application.production.WorkPlanRepository;
import com.shindong.smartmanager.application.production.WorkPlanView;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.math.BigDecimal;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.Optional;

/**
 * 제조 수주 라인({@link SalesFulfillmentRoute#MANUFACTURING})의 생산 이행 상태를
 * 최종공정 실적(자가 작업일보 / 외주입고 posted) 기준으로 자동 완료·롤백한다.
 * {@link SalesLineFulfillmentStatus#FORCE_COMPLETED}는 덮어쓰지 않는다.
 */
public class SalesOrderFulfillmentSyncService {

    private final SalesOrderRepository salesOrderRepository;
    private final ProductionPlanRepository productionPlanRepository;
    private final WorkPlanRepository workPlanRepository;
    private final WorkOrderRepository workOrderRepository;
    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final ProcessRepository processRepository;

    public SalesOrderFulfillmentSyncService(
            SalesOrderRepository salesOrderRepository,
            ProductionPlanRepository productionPlanRepository,
            WorkPlanRepository workPlanRepository,
            WorkOrderRepository workOrderRepository,
            OutsourcingOrderRepository outsourcingOrderRepository,
            ProcessRepository processRepository
    ) {
        this.salesOrderRepository = salesOrderRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.workPlanRepository = workPlanRepository;
        this.workOrderRepository = workOrderRepository;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.processRepository = processRepository;
    }

    public void syncBySalesOrderLineId(long salesOrderLineId, String actorUserId) {
        Optional<SalesOrderLineListView> lineOpt = salesOrderRepository.findLineListItem(salesOrderLineId);
        if (lineOpt.isEmpty()) {
            return;
        }
        SalesOrderLineListView line = lineOpt.get();
        if (line.orderStatus() != SalesOrderStatus.CONFIRMED) {
            return;
        }
        if (line.fulfillmentRoute() != SalesFulfillmentRoute.MANUFACTURING) {
            return;
        }
        if (line.fulfillmentStatus() == SalesLineFulfillmentStatus.FORCE_COMPLETED
                || line.fulfillmentStatus() == SalesLineFulfillmentStatus.WAITING) {
            return;
        }

        Optional<ProductionPlanView> planOpt = productionPlanRepository.findActiveBySalesOrderLineId(salesOrderLineId);
        if (planOpt.isEmpty()) {
            return;
        }
        ProductionPlanView plan = planOpt.get();
        List<WorkPlanView> workPlans = workPlanRepository.findActivePlannedByProductionPlanId(plan.id());
        if (workPlans.isEmpty()) {
            return;
        }

        short maxSeq = workPlans.stream()
                .map(WorkPlanView::processSequenceNum)
                .max(Comparator.naturalOrder())
                .orElse((short) 0);
        if (!ProcessSequenceNavigator.isFinalProcess(processRepository, plan.itemId(), maxSeq)) {
            return;
        }

        List<WorkPlanView> finalPlans = workPlans.stream()
                .filter(wp -> wp.processSequenceNum() == maxSeq)
                .toList();
        BigDecimal fulfilledQty = sumFulfilledQty(finalPlans);
        BigDecimal orderQty = line.orderQty() != null ? line.orderQty() : BigDecimal.ZERO;

        SalesLineFulfillmentStatus target;
        if (orderQty.compareTo(BigDecimal.ZERO) > 0 && fulfilledQty.compareTo(orderQty) >= 0) {
            target = SalesLineFulfillmentStatus.COMPLETED;
        } else {
            target = SalesLineFulfillmentStatus.IN_PROGRESS;
        }

        if (line.fulfillmentStatus() == target) {
            return;
        }
        salesOrderRepository.updateLineFulfillmentStatus(salesOrderLineId, target, actorUserId);
    }

    public void syncByProductionPlanId(long productionPlanId, String actorUserId) {
        productionPlanRepository.findActiveById(productionPlanId)
                .map(ProductionPlanView::salesOrderLineId)
                .filter(lineId -> lineId != null)
                .ifPresent(lineId -> syncBySalesOrderLineId(lineId, actorUserId));
    }

    public void syncByWorkPlanId(long workPlanId, String actorUserId) {
        workPlanRepository.findActiveById(workPlanId)
                .map(WorkPlanView::productionPlanId)
                .ifPresent(planId -> syncByProductionPlanId(planId, actorUserId));
    }

    private BigDecimal sumFulfilledQty(List<WorkPlanView> finalPlans) {
        List<Long> outsourcePlanIds = finalPlans.stream()
                .filter(wp -> wp.workDistinction() == WorkDistinction.OUTSOURCE)
                .map(WorkPlanView::id)
                .toList();
        Map<Long, BigDecimal> receivedByPlan = outsourcingOrderRepository.sumReceivedQtyByWorkPlanIds(outsourcePlanIds);

        BigDecimal total = BigDecimal.ZERO;
        for (WorkPlanView wp : finalPlans) {
            if (wp.workDistinction() == WorkDistinction.OUTSOURCE) {
                total = total.add(receivedByPlan.getOrDefault(wp.id(), BigDecimal.ZERO));
            } else {
                total = total.add(workOrderRepository.sumActiveReportedQtyByWorkPlanId(wp.id()));
            }
        }
        return total;
    }
}
