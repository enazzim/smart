package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.bom.BomTreeNode;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.purchase.PurchaseOrderRepository;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.domain.production.MrpGroupingMode;
import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;

public class MrpService {

    private static final String PURCHASE_ORDER_BLOCK_RUN_OR_PLAN =
            "발주된 자재소요가 포함되어 취소할 수 없습니다.";
    private static final String PURCHASE_ORDER_BLOCK_LINE =
            "발주된 자재소요는 취소할 수 없습니다.";

    private final MrpRepository mrpRepository;
    private final ProductionPlanRepository productionPlanRepository;
    private final ItemCompositionService itemCompositionService;
    private final ItemRepository itemRepository;
    private final PurchaseOrderRepository purchaseOrderRepository;
    private final SystemSettingService systemSettingService;

    public MrpService(
            MrpRepository mrpRepository,
            ProductionPlanRepository productionPlanRepository,
            ItemCompositionService itemCompositionService,
            ItemRepository itemRepository,
            PurchaseOrderRepository purchaseOrderRepository,
            SystemSettingService systemSettingService
    ) {
        this.mrpRepository = mrpRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.itemCompositionService = itemCompositionService;
        this.itemRepository = itemRepository;
        this.purchaseOrderRepository = purchaseOrderRepository;
        this.systemSettingService = systemSettingService;
    }

    public List<ProductionPlanView> listCalculationTargets() {
        return productionPlanRepository.findAllActive(
                new ProductionPlanListCriteria(null, null, null, null, null, ProductionPlanMrpStatus.NOT_CALCULATED)
        ).stream()
                .filter(plan -> plan.status() == ProductionPlanStatus.PLANNED
                        || plan.status() == ProductionPlanStatus.IN_PROGRESS)
                .toList();
    }

    public List<MrpRunView> listRuns() {
        return mrpRepository.findAllActiveRuns().stream()
                .map(this::enrichRunCancellable)
                .toList();
    }

    public MrpRunView getRun(long id) {
        return mrpRepository.findActiveRunById(id)
                .map(this::enrichRunCancellable)
                .orElseThrow(() -> new IllegalArgumentException("자재소요 산출 이력을 찾을 수 없습니다: " + id));
    }

    public List<MaterialRequirementLineView> listLinesByRun(long mrpRunId) {
        getRun(mrpRunId);
        return enrichLineCancellable(mrpRepository.findActiveLinesByRunId(mrpRunId));
    }

    public List<MaterialRequirementLineView> listAllLines() {
        return enrichLineCancellable(mrpRepository.findAllActiveLines());
    }

    public MaterialRequirementGroupedResult listGroupedLines(Optional<Long> mrpRunId) {
        MrpGroupingMode groupingMode = systemSettingService.resolveMrpGroupingMode();
        List<MaterialRequirementLineView> lines = mrpRunId.isPresent()
                ? listLinesByRun(mrpRunId.get())
                : listAllLines();

        if (groupingMode == MrpGroupingMode.BY_PLAN) {
            return new MaterialRequirementGroupedResult(groupingMode, lines, List.of());
        }
        return new MaterialRequirementGroupedResult(groupingMode, List.of(), groupByComponent(lines));
    }

    private List<MaterialRequirementComponentGroupView> groupByComponent(List<MaterialRequirementLineView> lines) {
        Map<Long, List<MaterialRequirementLineView>> grouped = new LinkedHashMap<>();
        for (MaterialRequirementLineView line : lines) {
            grouped.computeIfAbsent(line.componentItemId(), ignored -> new ArrayList<>()).add(line);
        }

        return grouped.entrySet().stream()
                .map(entry -> {
                    List<MaterialRequirementLineView> details = entry.getValue();
                    MaterialRequirementLineView first = details.get(0);
                    BigDecimal totalGrossQty = details.stream()
                            .map(MaterialRequirementLineView::grossQty)
                            .reduce(BigDecimal.ZERO, BigDecimal::add);
                    return new MaterialRequirementComponentGroupView(
                            first.componentItemId(),
                            first.componentItemNo(),
                            first.componentItemName(),
                            first.componentPropertyClassification(),
                            first.unit(),
                            totalGrossQty,
                            details.size(),
                            details
                    );
                })
                .toList();
    }

    public MrpRunView calculate(List<Long> productionPlanIds, String actorUserId) {
        if (productionPlanIds == null || productionPlanIds.isEmpty()) {
            throw new IllegalArgumentException("산출할 생산계획을 1건 이상 선택하세요.");
        }

        Set<Long> uniquePlanIds = new LinkedHashSet<>(productionPlanIds);
        List<ProductionPlanView> plans = new ArrayList<>();
        for (Long planId : uniquePlanIds) {
            ProductionPlanView plan = productionPlanRepository.findActiveById(planId)
                    .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + planId));
            validateCalculationTarget(plan);
            plans.add(plan);
        }

        String runNo = nextRunNo(LocalDate.now());
        long runId = mrpRepository.createRun(runNo, actorUserId);

        List<MaterialRequirementLineSaveCommand> lines = new ArrayList<>();
        for (ProductionPlanView plan : plans) {
            lines.addAll(buildRequirementLines(runId, plan));
        }

        if (lines.isEmpty()) {
            throw new IllegalStateException("자재소요 산출 결과가 없습니다.");
        }

        mrpRepository.saveLines(lines, actorUserId);
        for (ProductionPlanView plan : plans) {
            productionPlanRepository.updateMrpStatus(plan.id(), ProductionPlanMrpStatus.CALCULATED, actorUserId);
        }

        return getRun(runId);
    }

    public MrpRunView cancelRun(long runId, String actorUserId) {
        MrpRunView run = getRun(runId);
        List<Long> planIds = mrpRepository.findDistinctPlanIdsByRunId(runId);
        if (planIds.isEmpty()) {
            mrpRepository.deleteRunById(runId);
            return run;
        }
        for (Long planId : planIds) {
            ProductionPlanView plan = productionPlanRepository.findActiveById(planId)
                    .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + planId));
            validateCancellationTarget(plan);
        }
        ensureNoPurchaseOrdersForRun(runId);

        mrpRepository.deleteLinesByRunId(runId);
        mrpRepository.deleteRunById(runId);
        for (Long planId : planIds) {
            productionPlanRepository.updateMrpStatus(planId, ProductionPlanMrpStatus.NOT_CALCULATED, actorUserId);
        }
        return run;
    }

    public MrpCancelPlanResult cancelPlan(long productionPlanId, String actorUserId) {
        ProductionPlanView plan = productionPlanRepository.findActiveById(productionPlanId)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + productionPlanId));
        if (plan.mrpStatus() == ProductionPlanMrpStatus.NOT_CALCULATED
                && mrpRepository.countActiveLinesByProductionPlanId(productionPlanId) == 0) {
            throw new IllegalArgumentException("취소할 자재소요가 없습니다: " + plan.planNo());
        }
        validateCancellationTarget(plan);
        List<MaterialRequirementLineView> planLines =
                mrpRepository.findActiveLinesByProductionPlanId(productionPlanId);
        ensureAllLinesCancellable(planLines);
        ensureNoPurchaseOrdersForPlan(productionPlanId);

        Set<Long> runIds = new LinkedHashSet<>();
        for (MaterialRequirementLineView line : planLines) {
            runIds.add(line.mrpRunId());
        }

        mrpRepository.deleteLinesByProductionPlanId(productionPlanId);
        productionPlanRepository.updateMrpStatus(productionPlanId, ProductionPlanMrpStatus.NOT_CALCULATED, actorUserId);
        for (Long runId : runIds) {
            mrpRepository.deleteRunIfEmpty(runId);
        }
        return new MrpCancelPlanResult(plan.id(), plan.planNo());
    }

    public MaterialRequirementLineView cancelLine(long lineId, String actorUserId) {
        MaterialRequirementLineView line = mrpRepository.findActiveLineById(lineId)
                .orElseThrow(() -> new IllegalArgumentException("자재소요 라인을 찾을 수 없습니다: " + lineId));
        ProductionPlanView plan = productionPlanRepository.findActiveById(line.productionPlanId())
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + line.productionPlanId()));
        validateCancellationTarget(plan);
        ensureNoPurchaseOrderForLine(lineId);

        long runId = line.mrpRunId();
        mrpRepository.deleteLineById(lineId);
        syncMrpStatusAfterLineChange(line.productionPlanId(), actorUserId);
        mrpRepository.deleteRunIfEmpty(runId);

        return line;
    }

    private void syncMrpStatusAfterLineChange(long productionPlanId, String actorUserId) {
        ProductionPlanMrpStatus nextStatus = mrpRepository.countActiveLinesByProductionPlanId(productionPlanId) > 0
                ? ProductionPlanMrpStatus.CALCULATED
                : ProductionPlanMrpStatus.NOT_CALCULATED;
        productionPlanRepository.updateMrpStatus(productionPlanId, nextStatus, actorUserId);
    }

    private MrpRunView enrichRunCancellable(MrpRunView run) {
        boolean cancellable = isRunCancellable(run.id());
        return new MrpRunView(
                run.id(),
                run.runNo(),
                run.planCount(),
                run.lineCount(),
                run.createdAt(),
                run.createdBy(),
                cancellable
        );
    }

    private List<MaterialRequirementLineView> enrichLineCancellable(List<MaterialRequirementLineView> lines) {
        return lines.stream()
                .map(line -> new MaterialRequirementLineView(
                        line.id(),
                        line.mrpRunId(),
                        line.runNo(),
                        line.productionPlanId(),
                        line.planNo(),
                        line.parentItemId(),
                        line.parentItemNo(),
                        line.parentItemName(),
                        line.componentItemId(),
                        line.componentItemNo(),
                        line.componentItemName(),
                        line.componentPropertyClassification(),
                        line.unit(),
                        line.bomUnitQty(),
                        line.plannedQty(),
                        line.grossQty(),
                        line.createdAt(),
                        isLineCancellable(line)
                ))
                .toList();
    }

    private boolean isLineCancellable(MaterialRequirementLineView line) {
        if (!isPlanStateCancellable(line.productionPlanId())) {
            return false;
        }
        return !purchaseOrderRepository.existsActiveOrderReferencingRequirementLine(line.id());
    }

    private boolean isRunCancellable(long runId) {
        List<Long> planIds = mrpRepository.findDistinctPlanIdsByRunId(runId);
        if (planIds.isEmpty()) {
            return false;
        }
        for (Long planId : planIds) {
            if (!isPlanCancellable(planId)) {
                return false;
            }
        }
        return !hasPurchaseOrdersForRun(runId);
    }

    private boolean isPlanCancellable(long productionPlanId) {
        if (!isPlanStateCancellable(productionPlanId)) {
            return false;
        }
        List<MaterialRequirementLineView> lines = mrpRepository.findActiveLinesByProductionPlanId(productionPlanId);
        if (lines.isEmpty()) {
            return false;
        }
        return lines.stream().allMatch(this::isLineCancellable);
    }

    private void ensureAllLinesCancellable(List<MaterialRequirementLineView> lines) {
        for (MaterialRequirementLineView line : lines) {
            if (!isLineCancellable(line)) {
                if (!purchaseOrderRepository.existsActiveOrderReferencingRequirementLine(line.id())) {
                    throw new IllegalArgumentException(
                            "생산계획 " + line.planNo() + "의 일부 자재소요는 취소할 수 없습니다."
                    );
                }
                throw new IllegalArgumentException(PURCHASE_ORDER_BLOCK_RUN_OR_PLAN);
            }
        }
    }

    private boolean isPlanStateCancellable(long productionPlanId) {
        return productionPlanRepository.findActiveById(productionPlanId)
                .map(this::isCancellationTargetValid)
                .orElse(false);
    }

    private void ensureNoPurchaseOrdersForRun(long runId) {
        if (hasPurchaseOrdersForRun(runId)) {
            throw new IllegalArgumentException(PURCHASE_ORDER_BLOCK_RUN_OR_PLAN);
        }
    }

    private void ensureNoPurchaseOrdersForPlan(long productionPlanId) {
        if (hasPurchaseOrdersForPlan(productionPlanId)) {
            throw new IllegalArgumentException(PURCHASE_ORDER_BLOCK_RUN_OR_PLAN);
        }
    }

    private void ensureNoPurchaseOrderForLine(long requirementLineId) {
        if (purchaseOrderRepository.existsActiveOrderReferencingRequirementLine(requirementLineId)) {
            throw new IllegalArgumentException(PURCHASE_ORDER_BLOCK_LINE);
        }
    }

    private boolean hasPurchaseOrdersForRun(long runId) {
        List<Long> lineIds = requirementLineIds(mrpRepository.findActiveLinesByRunId(runId));
        return purchaseOrderRepository.existsActiveOrderReferencingAnyRequirementLines(lineIds);
    }

    private boolean hasPurchaseOrdersForPlan(long productionPlanId) {
        List<Long> lineIds = requirementLineIds(
                mrpRepository.findActiveLinesByProductionPlanId(productionPlanId)
        );
        return purchaseOrderRepository.existsActiveOrderReferencingAnyRequirementLines(lineIds);
    }

    private List<Long> requirementLineIds(List<MaterialRequirementLineView> lines) {
        return lines.stream().map(MaterialRequirementLineView::id).toList();
    }

    private void validateCancellationTarget(ProductionPlanView plan) {
        if (!isCancellationTargetValid(plan)) {
            if (plan.workPlanStatus() != ProductionPlanWorkPlanStatus.NOT_PLANNED) {
                throw new IllegalArgumentException("작업계획이 수립된 생산계획의 자재소요는 취소할 수 없습니다: " + plan.planNo());
            }
            if (plan.producedQty() != null && plan.producedQty().compareTo(BigDecimal.ZERO) > 0) {
                throw new IllegalArgumentException("생산 실적이 있는 생산계획의 자재소요는 취소할 수 없습니다: " + plan.planNo());
            }
            if (plan.status() != ProductionPlanStatus.PLANNED && plan.status() != ProductionPlanStatus.IN_PROGRESS) {
                throw new IllegalArgumentException("계획 또는 진행 상태의 생산계획만 자재소요를 취소할 수 있습니다: " + plan.planNo());
            }
            throw new IllegalArgumentException("자재소요를 취소할 수 없습니다: " + plan.planNo());
        }
    }

    private boolean isCancellationTargetValid(ProductionPlanView plan) {
        if (plan.workPlanStatus() != ProductionPlanWorkPlanStatus.NOT_PLANNED) {
            return false;
        }
        if (plan.producedQty() != null && plan.producedQty().compareTo(BigDecimal.ZERO) > 0) {
            return false;
        }
        return plan.status() == ProductionPlanStatus.PLANNED || plan.status() == ProductionPlanStatus.IN_PROGRESS;
    }

    private void validateCalculationTarget(ProductionPlanView plan) {
        if (plan.mrpStatus() != ProductionPlanMrpStatus.NOT_CALCULATED) {
            throw new IllegalArgumentException("이미 자재소요가 산출된 생산계획입니다: " + plan.planNo());
        }
        if (plan.status() != ProductionPlanStatus.PLANNED && plan.status() != ProductionPlanStatus.IN_PROGRESS) {
            throw new IllegalArgumentException("계획 또는 진행 상태의 생산계획만 산출할 수 있습니다: " + plan.planNo());
        }
    }

    private List<MaterialRequirementLineSaveCommand> buildRequirementLines(long runId, ProductionPlanView plan) {
        BomTreeNode tree = itemCompositionService.explode(plan.itemNo());
        List<BomTreeNode> leaves = collectLeaves(tree);
        if (leaves.isEmpty()) {
            throw new IllegalArgumentException(
                    "BOM이 없어 자재소요를 산출할 수 없습니다: " + plan.itemNo() + " (계획 " + plan.planNo() + ")"
            );
        }

        List<MaterialRequirementLineSaveCommand> lines = new ArrayList<>();
        for (BomTreeNode leaf : leaves) {
            ItemView component = itemRepository.findActiveByItemNo(leaf.itemNum())
                    .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다: " + leaf.itemNum()));
            BigDecimal grossQty = leaf.quantity()
                    .multiply(plan.plannedQty())
                    .setScale(4, RoundingMode.HALF_UP);
            lines.add(new MaterialRequirementLineSaveCommand(
                    runId,
                    plan.id(),
                    plan.itemId(),
                    component.id(),
                    leaf.quantity(),
                    plan.plannedQty(),
                    grossQty
            ));
        }
        return lines;
    }

    private List<BomTreeNode> collectLeaves(BomTreeNode node) {
        if (node.children() == null || node.children().isEmpty()) {
            if (node.level() == 0) {
                return List.of();
            }
            return List.of(node);
        }
        List<BomTreeNode> leaves = new ArrayList<>();
        for (BomTreeNode child : node.children()) {
            leaves.addAll(collectLeaves(child));
        }
        return leaves;
    }

    private String nextRunNo(LocalDate runDate) {
        String prefix = "MRP-" + runDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = mrpRepository.nextSequenceByRunNoPrefix(prefix);
        return prefix + String.format("%03d", seq);
    }
}
