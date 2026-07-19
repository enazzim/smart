package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.purchase.PurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.PurchaseOrderLineCommand;
import com.shindong.smartmanager.application.purchase.PurchaseOrderLineView;
import com.shindong.smartmanager.application.purchase.PurchaseOrderListCriteria;
import com.shindong.smartmanager.application.purchase.PurchaseOrderRepository;
import com.shindong.smartmanager.application.purchase.PurchaseOrderService;
import com.shindong.smartmanager.application.purchase.PurchaseOrderView;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.production.MaterialRequirementLineJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.production.MrpRunJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.production.ProductionPlanJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.production.SpringDataMaterialRequirementLineRepository;
import com.shindong.smartmanager.infrastructure.persistence.production.SpringDataMrpRunRepository;
import com.shindong.smartmanager.infrastructure.persistence.production.SpringDataProductionPlanRepository;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPurchaseOrderRepository implements PurchaseOrderRepository {

    private static final int ACTIVE = 1;

    private final SpringDataPurchaseOrderRepository orderRepository;
    private final SpringDataPurchaseOrderLineRepository lineRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataMaterialRequirementLineRepository requirementLineRepository;
    private final SpringDataProductionPlanRepository productionPlanRepository;
    private final SpringDataMrpRunRepository mrpRunRepository;
    private final ItemRepository itemLookup;

    public JpaPurchaseOrderRepository(
            SpringDataPurchaseOrderRepository orderRepository,
            SpringDataPurchaseOrderLineRepository lineRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            SpringDataMaterialRequirementLineRepository requirementLineRepository,
            SpringDataProductionPlanRepository productionPlanRepository,
            SpringDataMrpRunRepository mrpRunRepository,
            ItemRepository itemLookup
    ) {
        this.orderRepository = orderRepository;
        this.lineRepository = lineRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.requirementLineRepository = requirementLineRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.mrpRunRepository = mrpRunRepository;
        this.itemLookup = itemLookup;
    }

    @Override
    public boolean existsActiveOrderReferencingRequirementLine(long requirementLineId) {
        if (requirementLineId <= 0) {
            return false;
        }
        return lineRepository.existsActiveOrderReferencingRequirementLine(
                requirementLineId,
                ACTIVE,
                PurchaseOrderStatus.CANCELLED
        );
    }

    @Override
    public boolean existsActiveOrderReferencingAnyRequirementLines(Collection<Long> requirementLineIds) {
        if (requirementLineIds == null || requirementLineIds.isEmpty()) {
            return false;
        }
        return lineRepository.existsActiveOrderReferencingAnyRequirementLines(
                requirementLineIds,
                ACTIVE,
                PurchaseOrderStatus.CANCELLED
        );
    }

    @Override
    public Set<Long> findReferencedRequirementLineIds(Collection<Long> requirementLineIds) {
        if (requirementLineIds == null || requirementLineIds.isEmpty()) {
            return Set.of();
        }
        return lineRepository.findReferencedRequirementLineIds(
                requirementLineIds,
                ACTIVE,
                PurchaseOrderStatus.CANCELLED
        );
    }

    @Override
    public BigDecimal sumOrderedQtyByRequirementLineId(long requirementLineId) {
        if (requirementLineId <= 0) {
            return BigDecimal.ZERO;
        }
        return lineRepository.sumOrderedQtyByRequirementLineId(
                requirementLineId,
                ACTIVE,
                PurchaseOrderStatus.CANCELLED
        );
    }

    @Override
    public Map<Long, BigDecimal> sumOrderedQtyByRequirementLineIds(Collection<Long> requirementLineIds) {
        if (requirementLineIds == null || requirementLineIds.isEmpty()) {
            return Map.of();
        }
        Map<Long, BigDecimal> totals = new HashMap<>();
        for (Object[] row : lineRepository.sumOrderedQtyGroupedByRequirementLineId(
                requirementLineIds,
                ACTIVE,
                PurchaseOrderStatus.CANCELLED
        )) {
            totals.put((Long) row[0], (BigDecimal) row[1]);
        }
        return totals;
    }

    @Override
    public long countByOrderNoPrefix(String prefix) {
        return orderRepository.countByOrderNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    public boolean existsActiveByOrderNo(String orderNo) {
        return orderRepository.existsByOrderNoAndRecordingState(orderNo, ACTIVE);
    }

    @Override
    @Transactional
    public long save(PurchaseOrderCommand command, String orderNo, String actorUserId) {
        Instant now = Instant.now();
        PurchaseOrderJpaEntity entity = new PurchaseOrderJpaEntity();
        entity.setOrderNo(orderNo);
        entity.setPartnerId(command.partnerId());
        entity.setOrderDate(command.orderDate());
        entity.setSourceType(command.sourceType());
        entity.setStatus(PurchaseOrderStatus.DRAFT);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        PurchaseOrderJpaEntity saved = orderRepository.save(entity);
        insertLines(saved.getId(), command, actorUserId, now);
        return saved.getId();
    }

    @Override
    @Transactional
    public void updateHeader(long purchaseOrderId, PurchaseOrderCommand command, String actorUserId) {
        PurchaseOrderJpaEntity entity = requireActiveOrder(purchaseOrderId);
        Instant now = Instant.now();
        entity.setPartnerId(command.partnerId());
        entity.setOrderDate(command.orderDate());
        entity.setSourceType(command.sourceType());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    @Transactional
    public void replaceLines(long purchaseOrderId, PurchaseOrderCommand command, String actorUserId) {
        lineRepository.deleteByPurchaseOrderId(purchaseOrderId);
        lineRepository.flush();
        insertLines(purchaseOrderId, command, actorUserId, Instant.now());
    }

    @Override
    @Transactional
    public void updateStatus(long purchaseOrderId, PurchaseOrderStatus status, String actorUserId) {
        PurchaseOrderJpaEntity entity = requireActiveOrder(purchaseOrderId);
        Instant now = Instant.now();
        entity.setStatus(status);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    @Transactional
    public void clearRequirementLineReferences(long purchaseOrderId, String actorUserId) {
        Instant now = Instant.now();
        for (PurchaseOrderLineJpaEntity line : lineRepository.findActiveLinesWithRequirementReference(
                purchaseOrderId,
                ACTIVE
        )) {
            line.setRequirementLineId(null);
            line.setUpdatedBy(actorUserId);
            line.setUpdatedById(actorUserId);
            line.setUpdatedAt(now);
            lineRepository.save(line);
        }
    }

    @Override
    @Transactional
    public void markConfirmed(long purchaseOrderId, String actorUserId) {
        PurchaseOrderJpaEntity entity = requireActiveOrder(purchaseOrderId);
        Instant now = Instant.now();
        entity.setStatus(PurchaseOrderStatus.CONFIRMED);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    public List<PurchaseOrderView> findAllActive() {
        return orderRepository.findByRecordingStateOrderByOrderDateDescIdDesc(ACTIVE).stream()
                .map(order -> toView(order, loadLines(order.getId())))
                .toList();
    }

    @Override
    public List<PurchaseOrderView> findAllActive(PurchaseOrderListCriteria criteria) {
        if (criteria == null) {
            return findAllActive();
        }
        String partnerName = normalizeQuery(criteria.partnerName());
        String orderNo = normalizeQuery(criteria.orderNo());
        String itemPropertyScope = normalizeScope(criteria.itemPropertyScope());
        return orderRepository.searchActive(
                ACTIVE,
                criteria.orderDateFrom(),
                criteria.orderDateTo(),
                partnerName,
                orderNo,
                criteria.status(),
                criteria.excludeCancelled(),
                PurchaseOrderStatus.CANCELLED,
                itemPropertyScope
        ).stream()
                .map(order -> toView(order, loadLines(order.getId())))
                .filter(view -> matchesItemPropertyScope(view, itemPropertyScope))
                .toList();
    }

    private static String normalizeScope(String itemPropertyScope) {
        if (itemPropertyScope == null || itemPropertyScope.isBlank()) {
            return null;
        }
        return itemPropertyScope.trim();
    }

    private static boolean matchesItemPropertyScope(PurchaseOrderView view, String itemPropertyScope) {
        if (itemPropertyScope == null || itemPropertyScope.isBlank()) {
            return true;
        }
        boolean hasSubMaterial = view.lines().stream()
                .anyMatch(line -> "부자재".equals(line.propertyClassification()));
        boolean hasNonSubMaterial = view.lines().stream()
                .anyMatch(line -> line.propertyClassification() != null
                        && !line.propertyClassification().isBlank()
                        && !"부자재".equals(line.propertyClassification()));
        boolean hasGeneral = view.lines().stream()
                .anyMatch(line -> "원자재".equals(line.propertyClassification())
                        || "상품".equals(line.propertyClassification()));
        return switch (itemPropertyScope) {
            case "SUB_MATERIAL" -> hasSubMaterial && !hasNonSubMaterial;
            case "GENERAL" -> hasGeneral && !hasSubMaterial;
            default -> true;
        };
    }

    private String normalizeQuery(String value) {
        if (value == null) {
            return null;
        }
        String trimmed = value.trim();
        return trimmed.isEmpty() ? null : trimmed;
    }

    @Override
    public Optional<PurchaseOrderView> findActiveById(long id) {
        return orderRepository.findByIdAndRecordingState(id, ACTIVE)
                .map(order -> toView(order, loadLines(order.getId())));
    }

    @Override
    public PurchaseOrderStatus findStatus(long purchaseOrderId) {
        return requireActiveOrder(purchaseOrderId).getStatus();
    }

    @Override
    @Transactional(readOnly = true)
    public boolean hasReceiptProgress(long purchaseOrderId) {
        return loadLines(purchaseOrderId).stream().anyMatch(line ->
                greaterThanZero(line.getReceivedQty()) || greaterThanZero(line.getWaitingInspectionQty()));
    }

    private static boolean greaterThanZero(java.math.BigDecimal value) {
        return value != null && value.compareTo(java.math.BigDecimal.ZERO) > 0;
    }

    private PurchaseOrderJpaEntity requireActiveOrder(long purchaseOrderId) {
        return orderRepository.findByIdAndRecordingState(purchaseOrderId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("구매발주를 찾을 수 없습니다: " + purchaseOrderId));
    }

    private List<PurchaseOrderLineJpaEntity> loadLines(long purchaseOrderId) {
        return lineRepository.findByPurchaseOrderIdAndRecordingStateOrderByLineNoAsc(purchaseOrderId, ACTIVE);
    }

    private void insertLines(long purchaseOrderId, PurchaseOrderCommand command, String actorUserId, Instant now) {
        short lineNo = 1;
        for (PurchaseOrderLineCommand line : command.lines()) {
            itemLookup.findActiveById(line.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + line.itemId()));

            PurchaseOrderLineJpaEntity entity = new PurchaseOrderLineJpaEntity();
            entity.setPurchaseOrderId(purchaseOrderId);
            entity.setLineNo(lineNo++);
            entity.setItemId(line.itemId());
            entity.setOrderQty(line.orderQty());
            entity.setUnitPrice(line.unitPrice() != null ? line.unitPrice() : java.math.BigDecimal.ZERO);
            entity.setAmount(PurchaseOrderService.lineAmount(line.orderQty(), line.unitPrice()));
            entity.setRequirementLineId(line.requirementLineId());
            entity.setRequestedDeliveryDate(line.requestedDeliveryDate());
            entity.setRecordingState(ACTIVE);
            entity.setCreatedBy(actorUserId);
            entity.setCreatedById(actorUserId);
            entity.setCreatedAt(now);
            entity.setUpdatedBy(actorUserId);
            entity.setUpdatedById(actorUserId);
            entity.setUpdatedAt(now);
            lineRepository.save(entity);
        }
    }

    private PurchaseOrderView toView(PurchaseOrderJpaEntity order, List<PurchaseOrderLineJpaEntity> lines) {
        CompanyJpaEntity partner = companyRepository.findById(order.getPartnerId()).orElse(null);
        String partnerName = partner != null ? partner.getCompanyName() : "";
        String partnerBusinessRegNo = partner != null ? partner.getBusinessRegNo() : "";

        Map<Long, ItemJpaEntity> items = lines.stream()
                .map(PurchaseOrderLineJpaEntity::getItemId)
                .distinct()
                .map(itemRepository::findById)
                .flatMap(Optional::stream)
                .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));

        RequirementContext requirementContext = loadRequirementContext(lines);

        List<PurchaseOrderLineView> lineViews = new ArrayList<>();
        for (PurchaseOrderLineJpaEntity line : lines) {
            ItemJpaEntity item = items.get(line.getItemId());
            // Map.of()/emptyMap()는 null 키 get 시 NPE — 직접발주(requirementLineId=null)에서 발생한다.
            Long requirementLineId = line.getRequirementLineId();
            RequirementMeta requirementMeta = requirementLineId == null
                    ? null
                    : requirementContext.metaByLineId().get(requirementLineId);
            lineViews.add(new PurchaseOrderLineView(
                    line.getId(),
                    line.getLineNo(),
                    line.getItemId(),
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    item != null ? item.getPropertyClassification().name() : "",
                    line.getOrderQty(),
                    line.getReceivedQty() != null ? line.getReceivedQty() : java.math.BigDecimal.ZERO,
                    line.getWaitingInspectionQty() != null ? line.getWaitingInspectionQty() : java.math.BigDecimal.ZERO,
                    line.getUnitPrice(),
                    line.getAmount(),
                    line.getRequirementLineId(),
                    requirementMeta != null ? requirementMeta.planNo() : null,
                    requirementMeta != null ? requirementMeta.runNo() : null,
                    line.getRequestedDeliveryDate()
            ));
        }

        return new PurchaseOrderView(
                order.getId(),
                order.getOrderNo(),
                order.getPartnerId(),
                partnerName,
                partnerBusinessRegNo,
                order.getOrderDate(),
                order.getSourceType(),
                order.getStatus(),
                order.getCreatedAt(),
                order.getCreatedBy(),
                lineViews
        );
    }

    private RequirementContext loadRequirementContext(List<PurchaseOrderLineJpaEntity> lines) {
        Set<Long> requirementIds = lines.stream()
                .map(PurchaseOrderLineJpaEntity::getRequirementLineId)
                .filter(id -> id != null && id > 0)
                .collect(Collectors.toSet());
        if (requirementIds.isEmpty()) {
            return new RequirementContext(new HashMap<>());
        }

        Map<Long, MaterialRequirementLineJpaEntity> requirements = requirementLineRepository.findAllById(requirementIds).stream()
                .filter(line -> line.getRecordingState() == ACTIVE)
                .collect(Collectors.toMap(MaterialRequirementLineJpaEntity::getId, Function.identity()));

        Set<Long> planIds = new HashSet<>();
        Set<Long> runIds = new HashSet<>();
        for (MaterialRequirementLineJpaEntity requirement : requirements.values()) {
            planIds.add(requirement.getProductionPlanId());
            runIds.add(requirement.getMrpRunId());
        }

        Map<Long, ProductionPlanJpaEntity> plans = productionPlanRepository.findAllById(planIds).stream()
                .collect(Collectors.toMap(ProductionPlanJpaEntity::getId, Function.identity()));
        Map<Long, MrpRunJpaEntity> runs = mrpRunRepository.findAllById(runIds).stream()
                .collect(Collectors.toMap(MrpRunJpaEntity::getId, Function.identity()));

        Map<Long, RequirementMeta> metaByRequirementId = new HashMap<>();
        for (MaterialRequirementLineJpaEntity requirement : requirements.values()) {
            ProductionPlanJpaEntity plan = plans.get(requirement.getProductionPlanId());
            MrpRunJpaEntity run = runs.get(requirement.getMrpRunId());
            metaByRequirementId.put(
                    requirement.getId(),
                    new RequirementMeta(
                            plan != null ? plan.getPlanNo() : "",
                            run != null ? run.getRunNo() : ""
                    )
            );
        }
        return new RequirementContext(metaByRequirementId);
    }

    private record RequirementMeta(String planNo, String runNo) {
    }

    private record RequirementContext(Map<Long, RequirementMeta> metaByLineId) {
    }
}
