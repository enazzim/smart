package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.sales.SalesOrderLineListCriteria;
import com.shindong.smartmanager.application.sales.SalesOrderLineListView;
import com.shindong.smartmanager.application.sales.SalesOrderRepository;
import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;

public class ProductionPlanService {

    private final ProductionPlanRepository productionPlanRepository;
    private final SalesOrderRepository salesOrderRepository;

    public ProductionPlanService(
            ProductionPlanRepository productionPlanRepository,
            SalesOrderRepository salesOrderRepository
    ) {
        this.productionPlanRepository = productionPlanRepository;
        this.salesOrderRepository = salesOrderRepository;
    }

    public List<SalesOrderLineListView> listCandidates() {
        Set<Long> plannedLineIds = productionPlanRepository.findActiveSalesOrderLineIds();
        return salesOrderRepository.findLineList(emptyLineCriteria()).stream()
                .filter(line -> isPlanCandidate(line, plannedLineIds))
                .toList();
    }

    public List<ProductionPlanView> list(ProductionPlanListCriteria criteria) {
        return productionPlanRepository.findAllActive(criteria != null ? criteria : emptyPlanCriteria());
    }

    public ProductionPlanView get(long id) {
        return productionPlanRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + id));
    }

    public List<ProductionPlanView> createPlans(List<ProductionPlanCreateLineCommand> lines, String actorUserId) {
        if (lines == null || lines.isEmpty()) {
            throw new IllegalArgumentException("수립할 수주 라인을 1건 이상 선택하세요.");
        }
        List<ProductionPlanView> created = new ArrayList<>();
        for (ProductionPlanCreateLineCommand lineCommand : lines) {
            created.add(createPlan(lineCommand, actorUserId));
        }
        return created;
    }

    public ProductionPlanView cancelPlan(long id, String actorUserId) {
        ProductionPlanView plan = get(id);
        if (plan.producedQty() != null && plan.producedQty().compareTo(BigDecimal.ZERO) > 0) {
            throw new IllegalArgumentException("생산 실적이 있는 생산계획은 취소할 수 없습니다.");
        }
        if (plan.status() != ProductionPlanStatus.PLANNED && plan.status() != ProductionPlanStatus.IN_PROGRESS) {
            throw new IllegalArgumentException("계획 또는 진행 상태의 생산계획만 취소할 수 있습니다.");
        }
        if (plan.mrpStatus() != ProductionPlanMrpStatus.NOT_CALCULATED) {
            throw new IllegalArgumentException("자재소요가 산출된 생산계획은 취소할 수 없습니다.");
        }
        if (plan.workPlanStatus() != ProductionPlanWorkPlanStatus.NOT_PLANNED) {
            throw new IllegalArgumentException("작업계획이 수립된 생산계획은 취소할 수 없습니다.");
        }

        productionPlanRepository.deleteById(id);

        if (!productionPlanRepository.existsActiveBySalesOrderLineId(plan.salesOrderLineId())) {
            SalesOrderLineListView line = salesOrderRepository.findLineListItem(plan.salesOrderLineId())
                    .orElse(null);
            if (line != null && line.fulfillmentStatus() == SalesLineFulfillmentStatus.IN_PROGRESS) {
                salesOrderRepository.updateLineFulfillmentStatus(
                        plan.salesOrderLineId(),
                        SalesLineFulfillmentStatus.WAITING,
                        actorUserId
                );
            }
        }

        return plan;
    }

    private ProductionPlanView createPlan(ProductionPlanCreateLineCommand lineCommand, String actorUserId) {
        long salesOrderLineId = lineCommand.salesOrderLineId();
        SalesOrderLineListView line = salesOrderRepository.findLineListItem(salesOrderLineId)
                .orElseThrow(() -> new IllegalArgumentException("수주 라인을 찾을 수 없습니다: " + salesOrderLineId));

        Set<Long> plannedLineIds = productionPlanRepository.findActiveSalesOrderLineIds();
        validatePlanCandidate(line, plannedLineIds);

        if (productionPlanRepository.existsActiveBySalesOrderLineId(salesOrderLineId)) {
            throw new IllegalArgumentException("이미 생산계획이 수립된 수주 라인입니다: " + salesOrderLineId);
        }

        BigDecimal plannedQty = resolvePlannedQty(lineCommand.plannedQty(), line.orderQty());

        String planNo = nextPlanNo(LocalDate.now());
        ProductionPlanSaveCommand command = new ProductionPlanSaveCommand(
                line.orderId(),
                line.lineId(),
                line.itemId(),
                plannedQty,
                line.requestedDeliveryDate()
        );
        long planId = productionPlanRepository.save(command, planNo, actorUserId);
        salesOrderRepository.updateLineFulfillmentStatus(
                salesOrderLineId,
                SalesLineFulfillmentStatus.IN_PROGRESS,
                actorUserId
        );
        return productionPlanRepository.findActiveById(planId)
                .orElseThrow(() -> new IllegalStateException("생산계획 저장 후 조회에 실패했습니다: " + planId));
    }

    private BigDecimal resolvePlannedQty(BigDecimal requestedQty, BigDecimal orderQty) {
        BigDecimal plannedQty = requestedQty != null ? requestedQty : orderQty;
        if (plannedQty == null || plannedQty.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("계획수량은 0보다 커야 합니다.");
        }
        return plannedQty;
    }

    private void validatePlanCandidate(SalesOrderLineListView line, Set<Long> plannedLineIds) {
        if (line.orderStatus() == SalesOrderStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 수주 라인은 생산계획을 수립할 수 없습니다.");
        }
        if (line.fulfillmentRoute() != SalesFulfillmentRoute.MANUFACTURING) {
            throw new IllegalArgumentException("생산 라우트(제품·공정품) 수주 라인만 생산계획을 수립할 수 있습니다.");
        }
        if (line.fulfillmentStatus() != SalesLineFulfillmentStatus.WAITING) {
            throw new IllegalArgumentException("이행상태가 대기인 수주 라인만 생산계획을 수립할 수 있습니다.");
        }
        if (line.deliveryStatus() == SalesLineDeliveryStatus.COMPLETED) {
            throw new IllegalArgumentException("납품 완료된 수주 라인은 생산계획을 수립할 수 없습니다.");
        }
        if (plannedLineIds.contains(line.lineId())) {
            throw new IllegalArgumentException("이미 생산계획이 수립된 수주 라인입니다.");
        }
    }

    private boolean isPlanCandidate(SalesOrderLineListView line, Set<Long> plannedLineIds) {
        if (line.orderStatus() == SalesOrderStatus.CANCELLED) {
            return false;
        }
        if (line.fulfillmentRoute() != SalesFulfillmentRoute.MANUFACTURING) {
            return false;
        }
        if (line.fulfillmentStatus() != SalesLineFulfillmentStatus.WAITING) {
            return false;
        }
        if (line.deliveryStatus() == SalesLineDeliveryStatus.COMPLETED) {
            return false;
        }
        return !plannedLineIds.contains(line.lineId());
    }

    private String nextPlanNo(LocalDate planDate) {
        String prefix = "PP-" + planDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = productionPlanRepository.nextSequenceByPlanNoPrefix(prefix);
        return prefix + String.format("%03d", seq);
    }

    private SalesOrderLineListCriteria emptyLineCriteria() {
        return new SalesOrderLineListCriteria(null, null, null, null, null, null);
    }

    private ProductionPlanListCriteria emptyPlanCriteria() {
        return new ProductionPlanListCriteria(null, null, null, null, null, null);
    }
}
