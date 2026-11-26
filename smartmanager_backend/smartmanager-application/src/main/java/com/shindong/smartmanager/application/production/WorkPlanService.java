package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.bom.BomTreeNode;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.workstandard.WorkStandardRepository;
import com.shindong.smartmanager.application.workstandard.WorkStandardView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import com.shindong.smartmanager.domain.production.WorkPlanStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class WorkPlanService {

    private final WorkPlanRepository workPlanRepository;
    private final ProductionPlanRepository productionPlanRepository;
    private final ProcessRepository processRepository;
    private final WorkStandardRepository workStandardRepository;
    private final ItemCompositionService itemCompositionService;
    private final ItemRepository itemRepository;
    private final WorkOrderRepository workOrderRepository;
    private final OutsourcingOrderRepository outsourcingOrderRepository;

    public WorkPlanService(
            WorkPlanRepository workPlanRepository,
            ProductionPlanRepository productionPlanRepository,
            ProcessRepository processRepository,
            WorkStandardRepository workStandardRepository,
            ItemCompositionService itemCompositionService,
            ItemRepository itemRepository,
            WorkOrderRepository workOrderRepository,
            OutsourcingOrderRepository outsourcingOrderRepository
    ) {
        this.workPlanRepository = workPlanRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.processRepository = processRepository;
        this.workStandardRepository = workStandardRepository;
        this.itemCompositionService = itemCompositionService;
        this.itemRepository = itemRepository;
        this.workOrderRepository = workOrderRepository;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
    }

    public List<ProductionPlanView> listPlanningTargets(
            Long itemId,
            LocalDate registeredFrom,
            LocalDate registeredTo
    ) {
        return productionPlanRepository.findAllActive(
                new ProductionPlanListCriteria(null, itemId, null, null, null, ProductionPlanMrpStatus.CALCULATED)
        ).stream()
                .filter(plan -> plan.workPlanStatus() == ProductionPlanWorkPlanStatus.NOT_PLANNED)
                .filter(plan -> plan.status() == ProductionPlanStatus.PLANNED
                        || plan.status() == ProductionPlanStatus.IN_PROGRESS)
                .filter(plan -> matchesRegisteredDate(plan.planNo(), registeredFrom, registeredTo))
                .toList();
    }

    private static boolean matchesRegisteredDate(String planNo, LocalDate from, LocalDate to) {
        if (from == null && to == null) {
            return true;
        }
        if (planNo == null || !planNo.startsWith("PP-") || planNo.length() < 11) {
            return false;
        }
        LocalDate registered;
        try {
            registered = LocalDate.parse(planNo.substring(3, 11), DateTimeFormatter.BASIC_ISO_DATE);
        } catch (DateTimeParseException ex) {
            return false;
        }
        if (from != null && registered.isBefore(from)) {
            return false;
        }
        return to == null || !registered.isAfter(to);
    }

    public List<WorkPlanView> list(WorkPlanListCriteria criteria) {
        List<WorkPlanView> lines = workPlanRepository.findAllActive(criteria);
        CancelBlocks blocks = cancelBlocks(lines);
        return lines.stream()
                .map(line -> enrichCancellable(line, blocks))
                .toList();
    }

    public WorkPlanCancelPlanResult cancelPlan(long productionPlanId, String actorUserId) {
        ProductionPlanView plan = productionPlanRepository.findActiveById(productionPlanId)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + productionPlanId));
        if (plan.workPlanStatus() != ProductionPlanWorkPlanStatus.PLANNED) {
            throw new IllegalArgumentException("작업계획이 수립되지 않은 생산계획입니다: " + plan.planNo());
        }

        List<WorkPlanView> lines = workPlanRepository.findActivePlannedByProductionPlanId(productionPlanId);
        if (lines.isEmpty()) {
            throw new IllegalArgumentException("취소할 작업계획이 없습니다: " + plan.planNo());
        }
        ensureCancellable(lines);

        workPlanRepository.cancelByProductionPlanId(productionPlanId, actorUserId);
        productionPlanRepository.updateWorkPlanStatus(
                productionPlanId,
                ProductionPlanWorkPlanStatus.NOT_PLANNED,
                actorUserId
        );
        return new WorkPlanCancelPlanResult(productionPlanId, plan.planNo(), lines.size());
    }

    public WorkPlanView cancelLine(long id, String actorUserId) {
        WorkPlanView line = workPlanRepository.findActivePlannedById(id)
                .orElseThrow(() -> new IllegalArgumentException("작업계획을 찾을 수 없습니다: " + id));
        ensureCancellable(List.of(line));

        workPlanRepository.cancelById(id, actorUserId);
        syncProductionPlanWorkPlanStatus(line.productionPlanId(), actorUserId);
        return toCancelledView(line);
    }

    private WorkPlanView toCancelledView(WorkPlanView line) {
        return new WorkPlanView(
                line.id(),
                line.productionPlanId(),
                line.planNo(),
                line.itemId(),
                line.itemNo(),
                line.itemName(),
                line.processSequenceId(),
                line.processSequenceNum(),
                line.processCode(),
                line.processName(),
                line.workDistinction(),
                line.workCenterId(),
                line.workCenterName(),
                line.plannedQty(),
                line.planStartDate(),
                line.planEndDate(),
                line.setupTime(),
                line.standardTime(),
                WorkPlanStatus.CANCELLED,
                false,
                line.createdAt(),
                line.createdBy()
        );
    }

    private void syncProductionPlanWorkPlanStatus(long productionPlanId, String actorUserId) {
        ProductionPlanWorkPlanStatus nextStatus =
                workPlanRepository.countActivePlannedByProductionPlanId(productionPlanId) > 0
                        ? ProductionPlanWorkPlanStatus.PLANNED
                        : ProductionPlanWorkPlanStatus.NOT_PLANNED;
        productionPlanRepository.updateWorkPlanStatus(productionPlanId, nextStatus, actorUserId);
    }

    private void ensureCancellable(List<WorkPlanView> lines) {
        CancelBlocks blocks = cancelBlocks(lines);
        for (WorkPlanView line : lines) {
            if (!isCancellable(line, blocks)) {
                throw new IllegalArgumentException(cancelBlockedMessage(line, blocks));
            }
        }
    }

    private CancelBlocks cancelBlocks(List<WorkPlanView> lines) {
        List<Long> ids = lines.stream().map(WorkPlanView::id).toList();
        if (ids.isEmpty()) {
            return new CancelBlocks(Set.of(), Set.of());
        }
        Set<Long> ordered = new HashSet<>();
        for (Map.Entry<Long, BigDecimal> entry : outsourcingOrderRepository.sumOrderedQtyByWorkPlanIds(ids).entrySet()) {
            if (entry.getValue() != null && entry.getValue().compareTo(BigDecimal.ZERO) > 0) {
                ordered.add(entry.getKey());
            }
        }
        return new CancelBlocks(workOrderRepository.findActiveIssuedWorkPlanIds(ids), ordered);
    }

    private boolean isCancellable(WorkPlanView line, CancelBlocks blocks) {
        if (line.status() != WorkPlanStatus.PLANNED) {
            return false;
        }
        if (line.workDistinction() == WorkDistinction.INHOUSE) {
            return !blocks.issuedWorkPlanIds().contains(line.id());
        }
        if (line.workDistinction() == WorkDistinction.OUTSOURCE) {
            return !blocks.orderedWorkPlanIds().contains(line.id());
        }
        return true;
    }

    private String cancelBlockedMessage(WorkPlanView line, CancelBlocks blocks) {
        String label = line.planNo() + " " + line.processSequenceNum() + ". " + line.processName();
        if (line.workDistinction() == WorkDistinction.INHOUSE && blocks.issuedWorkPlanIds().contains(line.id())) {
            return "작업지시가 있어 작업계획을 취소할 수 없습니다: " + label;
        }
        if (line.workDistinction() == WorkDistinction.OUTSOURCE && blocks.orderedWorkPlanIds().contains(line.id())) {
            return "외주발주가 있어 작업계획을 취소할 수 없습니다: " + label;
        }
        return "취소할 수 없는 작업계획이 포함되어 있습니다: " + label;
    }

    private WorkPlanView enrichCancellable(WorkPlanView line, CancelBlocks blocks) {
        return new WorkPlanView(
                line.id(),
                line.productionPlanId(),
                line.planNo(),
                line.itemId(),
                line.itemNo(),
                line.itemName(),
                line.processSequenceId(),
                line.processSequenceNum(),
                line.processCode(),
                line.processName(),
                line.workDistinction(),
                line.workCenterId(),
                line.workCenterName(),
                line.plannedQty(),
                line.planStartDate(),
                line.planEndDate(),
                line.setupTime(),
                line.standardTime(),
                line.status(),
                isCancellable(line, blocks),
                line.createdAt(),
                line.createdBy()
        );
    }

    private record CancelBlocks(Set<Long> issuedWorkPlanIds, Set<Long> orderedWorkPlanIds) {
    }

    public List<WorkPlanView> createPlans(List<Long> productionPlanIds, String actorUserId) {
        if (productionPlanIds == null || productionPlanIds.isEmpty()) {
            throw new IllegalArgumentException("생산계획을 선택해 주세요.");
        }
        List<WorkPlanView> created = new ArrayList<>();
        for (long planId : productionPlanIds) {
            created.addAll(createPlansForProductionPlan(planId, actorUserId));
        }
        return created;
    }

    private List<WorkPlanView> createPlansForProductionPlan(long planId, String actorUserId) {
        ProductionPlanView plan = productionPlanRepository.findActiveById(planId)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + planId));

        if (plan.mrpStatus() != ProductionPlanMrpStatus.CALCULATED) {
            throw new IllegalArgumentException("MRP 산출이 완료된 생산계획만 작업계획을 수립할 수 있습니다: " + plan.planNo());
        }
        if (plan.workPlanStatus() != ProductionPlanWorkPlanStatus.NOT_PLANNED) {
            throw new IllegalArgumentException("이미 작업계획이 수립된 생산계획입니다: " + plan.planNo());
        }
        if (plan.status() == ProductionPlanStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 생산계획은 작업계획을 수립할 수 없습니다: " + plan.planNo());
        }

        LocalDate planStartDate = plan.requestedDeliveryDate() != null
                ? plan.requestedDeliveryDate()
                : LocalDate.now();

        List<WorkPlanSaveCommand> saves = new ArrayList<>();
        for (ManufacturingTarget target : collectManufacturingTargets(plan).values()) {
            List<ProcessView> processes = processRepository
                    .findAllActiveByItemId(target.itemId(), ProcessVariant.plan).stream()
                    .sorted(Comparator.comparingInt(ProcessView::processSequenceNum))
                    .toList();
            List<WorkStandardView> standards = workStandardRepository.findActiveByItemId(target.itemId());

            for (ProcessView process : processes) {
                WorkStandardView standard = standards.stream()
                        .filter(ws -> ws.processSequenceId() == process.id())
                        .min(Comparator.comparingInt(WorkStandardView::priorityOrder))
                        .orElse(null);

                if (process.workDistinction() == WorkDistinction.INHOUSE) {
                    saves.add(buildSave(
                            plan, target, process, standard, WorkDistinction.INHOUSE, target.plannedQty(), planStartDate
                    ));
                } else if (process.workDistinction() == WorkDistinction.OUTSOURCE) {
                    saves.add(buildSave(
                            plan, target, process, standard, WorkDistinction.OUTSOURCE, target.plannedQty(), planStartDate
                    ));
                } else if (process.workDistinction() == WorkDistinction.SPLIT) {
                    BigDecimal outsourceQty = target.plannedQty()
                            .multiply(BigDecimal.valueOf(process.outsideOrderRate()))
                            .divide(BigDecimal.valueOf(100), 4, RoundingMode.HALF_UP);
                    BigDecimal inhouseQty = target.plannedQty().subtract(outsourceQty);
                    if (inhouseQty.compareTo(BigDecimal.ZERO) > 0) {
                        saves.add(buildSave(
                                plan, target, process, standard, WorkDistinction.INHOUSE, inhouseQty, planStartDate
                        ));
                    }
                    if (outsourceQty.compareTo(BigDecimal.ZERO) > 0) {
                        saves.add(buildSave(
                                plan, target, process, standard, WorkDistinction.OUTSOURCE, outsourceQty, planStartDate
                        ));
                    }
                }
            }
        }

        if (saves.isEmpty()) {
            throw new IllegalArgumentException("작업계획을 수립할 공정이 없습니다: " + plan.planNo());
        }

        List<WorkPlanView> result = workPlanRepository.saveAll(saves, actorUserId);
        productionPlanRepository.updateWorkPlanStatus(planId, ProductionPlanWorkPlanStatus.PLANNED, actorUserId);
        CancelBlocks blocks = cancelBlocks(result);
        return result.stream().map(line -> enrichCancellable(line, blocks)).toList();
    }

    private Map<Long, ManufacturingTarget> collectManufacturingTargets(ProductionPlanView plan) {
        BomTreeNode root = itemCompositionService.explode(plan.itemNo());
        Map<Long, ManufacturingTarget> merged = new LinkedHashMap<>();
        walkManufacturingTargets(root, plan.plannedQty(), merged);
        return merged;
    }

    private void walkManufacturingTargets(
            BomTreeNode node,
            BigDecimal planQty,
            Map<Long, ManufacturingTarget> merged
    ) {
        if (node.propertyClassification() == PropertyClassification.제품
                || node.propertyClassification() == PropertyClassification.공정품) {
            ItemView item = itemRepository.findActiveByItemNo(node.itemNum())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + node.itemNum()));
            BigDecimal lineQty = node.quantity().multiply(planQty).setScale(4, RoundingMode.HALF_UP);
            merged.compute(item.id(), (itemId, existing) -> {
                if (existing == null) {
                    return new ManufacturingTarget(item.id(), item.itemNo(), lineQty);
                }
                return new ManufacturingTarget(
                        existing.itemId(),
                        existing.itemNo(),
                        existing.plannedQty().add(lineQty)
                );
            });
        }
        if (node.children() != null) {
            for (BomTreeNode child : node.children()) {
                walkManufacturingTargets(child, planQty, merged);
            }
        }
    }

    private WorkPlanSaveCommand buildSave(
            ProductionPlanView plan,
            ManufacturingTarget target,
            ProcessView process,
            WorkStandardView standard,
            WorkDistinction workDistinction,
            BigDecimal plannedQty,
            LocalDate planStartDate
    ) {
        Long workCenterId = null;
        int setupTime = 0;
        int standardTime = 0;
        if (workDistinction == WorkDistinction.INHOUSE) {
            if (standard != null) {
                workCenterId = standard.workCenterId();
                setupTime = standard.setupTime();
                standardTime = standard.standardTime();
            } else if (process.workCenterId() != null) {
                workCenterId = process.workCenterId();
            }
            if (workCenterId == null) {
                throw new IllegalArgumentException(
                        "자가 공정에 작업장이 없습니다. 품목=" + target.itemNo()
                                + ", 공정=" + process.processName()
                );
            }
        }
        return new WorkPlanSaveCommand(
                plan.id(),
                process.id(),
                workCenterId,
                workDistinction,
                plannedQty,
                planStartDate,
                null,
                setupTime,
                standardTime
        );
    }

    private record ManufacturingTarget(long itemId, String itemNo, BigDecimal plannedQty) {
    }
}
