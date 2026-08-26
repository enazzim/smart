package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.production.ProductionPlanListCriteria;
import com.shindong.smartmanager.application.production.ProductionPlanRepository;
import com.shindong.smartmanager.application.production.ProductionPlanSaveCommand;
import com.shindong.smartmanager.application.production.ProductionPlanView;
import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanSourceType;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.sales.SalesOrderJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.sales.SpringDataSalesOrderRepository;
import java.time.Instant;
import java.util.List;
import java.util.Map;
import java.util.Objects;
import java.util.Optional;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaProductionPlanRepository implements ProductionPlanRepository {

    private static final int ACTIVE = 1;

    private final SpringDataProductionPlanRepository planRepository;
    private final SpringDataSalesOrderRepository orderRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaProductionPlanRepository(
            SpringDataProductionPlanRepository planRepository,
            SpringDataSalesOrderRepository orderRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.planRepository = planRepository;
        this.orderRepository = orderRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    public long nextSequenceByPlanNoPrefix(String prefix) {
        return planRepository.findByPlanNoStartingWithAndRecordingState(prefix, ACTIVE).stream()
                .mapToLong(plan -> parsePlanNoSequence(plan.getPlanNo(), prefix))
                .max()
                .orElse(0L) + 1L;
    }

    private long parsePlanNoSequence(String planNo, String prefix) {
        if (planNo == null || !planNo.startsWith(prefix)) {
            return 0L;
        }
        try {
            return Long.parseLong(planNo.substring(prefix.length()));
        } catch (NumberFormatException ex) {
            return 0L;
        }
    }

    @Override
    public boolean existsActiveBySalesOrderLineId(long salesOrderLineId) {
        return planRepository.existsBySalesOrderLineIdAndRecordingState(salesOrderLineId, ACTIVE);
    }

    @Override
    public boolean existsActiveBySalesOrderId(long salesOrderId) {
        return planRepository.existsBySalesOrderIdAndRecordingState(salesOrderId, ACTIVE);
    }

    @Override
    @Transactional
    public long save(ProductionPlanSaveCommand command, String planNo, String actorUserId) {
        Instant now = Instant.now();
        ProductionPlanJpaEntity entity = new ProductionPlanJpaEntity();
        entity.setPlanNo(planNo);
        entity.setSourceType(command.sourceType());
        entity.setSalesOrderId(command.salesOrderId());
        entity.setSalesOrderLineId(command.salesOrderLineId());
        entity.setItemId(command.itemId());
        entity.setPlannedQty(command.plannedQty());
        entity.setProducedQty(java.math.BigDecimal.ZERO);
        entity.setRequestedDeliveryDate(command.requestedDeliveryDate());
        entity.setStatus(ProductionPlanStatus.PLANNED);
        entity.setMrpStatus(ProductionPlanMrpStatus.NOT_CALCULATED);
        entity.setWorkPlanStatus(ProductionPlanWorkPlanStatus.NOT_PLANNED);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setCreatedAt(now);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        return planRepository.save(entity).getId();
    }

    @Override
    public List<ProductionPlanView> findAllActive(ProductionPlanListCriteria criteria) {
        Map<Long, SalesOrderJpaEntity> orders = orderRepository.findByRecordingStateOrderByOrderDateDescIdDesc(ACTIVE)
                .stream()
                .collect(Collectors.toMap(SalesOrderJpaEntity::getId, Function.identity()));
        Map<Long, CompanyJpaEntity> companies = companyRepository.findAll().stream()
                .filter(company -> company.getRecordingState() == ACTIVE)
                .collect(Collectors.toMap(CompanyJpaEntity::getId, Function.identity()));
        Map<Long, ItemJpaEntity> items = itemRepository.findAll().stream()
                .filter(item -> item.getRecordingState() == ACTIVE)
                .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));

        return planRepository.findByRecordingStateOrderByIdDesc(ACTIVE).stream()
                .map(plan -> toView(plan, orders, companies, items))
                .flatMap(Optional::stream)
                .filter(view -> matchesCriteria(view, criteria))
                .toList();
    }

    @Override
    public Optional<ProductionPlanView> findActiveById(long id) {
        return planRepository.findByIdAndRecordingState(id, ACTIVE)
                .flatMap(plan -> {
                    Map<Long, SalesOrderJpaEntity> orders = Map.of();
                    if (plan.getSalesOrderId() != null) {
                        Optional<SalesOrderJpaEntity> orderOpt = orderRepository.findByIdAndRecordingState(
                                plan.getSalesOrderId(),
                                ACTIVE
                        );
                        if (orderOpt.isEmpty()) {
                            return Optional.empty();
                        }
                        orders = Map.of(orderOpt.get().getId(), orderOpt.get());
                    }
                    Map<Long, CompanyJpaEntity> companies = companyRepository.findAll().stream()
                            .filter(company -> company.getRecordingState() == ACTIVE)
                            .collect(Collectors.toMap(CompanyJpaEntity::getId, Function.identity()));
                    Map<Long, ItemJpaEntity> items = itemRepository.findAll().stream()
                            .filter(item -> item.getRecordingState() == ACTIVE)
                            .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));
                    return toView(plan, orders, companies, items);
                });
    }

    @Override
    public Set<Long> findActiveSalesOrderLineIds() {
        return planRepository.findByRecordingStateOrderByIdDesc(ACTIVE).stream()
                .map(ProductionPlanJpaEntity::getSalesOrderLineId)
                .filter(Objects::nonNull)
                .collect(Collectors.toSet());
    }

    @Override
    @Transactional
    public void deleteById(long id) {
        ProductionPlanJpaEntity entity = planRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + id));
        planRepository.delete(entity);
    }

    private Optional<ProductionPlanView> toView(
            ProductionPlanJpaEntity plan,
            Map<Long, SalesOrderJpaEntity> orders,
            Map<Long, CompanyJpaEntity> companies,
            Map<Long, ItemJpaEntity> items
    ) {
        ItemJpaEntity item = items.get(plan.getItemId());
        if (item == null) {
            return Optional.empty();
        }

        ProductionPlanSourceType sourceType = plan.getSourceType();
        String orderNo = null;
        Long partnerId = null;
        String partnerName = null;
        java.time.LocalDate orderDate = null;

        if (sourceType == ProductionPlanSourceType.SALES_ORDER) {
            SalesOrderJpaEntity order = orders.get(plan.getSalesOrderId());
            if (order == null) {
                return Optional.empty();
            }
            CompanyJpaEntity company = companies.get(order.getPartnerId());
            if (company == null) {
                return Optional.empty();
            }
            orderNo = order.getOrderNo();
            partnerId = order.getPartnerId();
            partnerName = company.getCompanyName();
            orderDate = order.getOrderDate();
        }

        return Optional.of(new ProductionPlanView(
                plan.getId(),
                plan.getPlanNo(),
                sourceType,
                plan.getSalesOrderId(),
                plan.getSalesOrderLineId(),
                orderNo,
                partnerId,
                partnerName,
                orderDate,
                plan.getItemId(),
                item.getItemNo(),
                item.getItemName(),
                plan.getPlannedQty(),
                plan.getProducedQty(),
                plan.getRequestedDeliveryDate(),
                plan.getStatus(),
                plan.getMrpStatus(),
                plan.getWorkPlanStatus()
        ));
    }

    private boolean matchesCriteria(ProductionPlanView view, ProductionPlanListCriteria criteria) {
        if (criteria == null) {
            return true;
        }
        if (criteria.partnerId() != null) {
            if (view.partnerId() == null || view.partnerId() != criteria.partnerId()) {
                return false;
            }
        }
        if (criteria.itemId() != null && view.itemId() != criteria.itemId()) {
            return false;
        }
        if (criteria.requestedDeliveryDateFrom() != null) {
            if (view.requestedDeliveryDate() == null
                    || view.requestedDeliveryDate().isBefore(criteria.requestedDeliveryDateFrom())) {
                return false;
            }
        }
        if (criteria.requestedDeliveryDateTo() != null) {
            if (view.requestedDeliveryDate() == null
                    || view.requestedDeliveryDate().isAfter(criteria.requestedDeliveryDateTo())) {
                return false;
            }
        }
        if (criteria.status() != null && view.status() != criteria.status()) {
            return false;
        }
        return criteria.mrpStatus() == null || view.mrpStatus() == criteria.mrpStatus();
    }

    @Override
    @Transactional
    public void updateMrpStatus(long id, ProductionPlanMrpStatus mrpStatus, String actorUserId) {
        ProductionPlanJpaEntity entity = planRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + id));
        entity.setMrpStatus(mrpStatus);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(Instant.now());
        planRepository.save(entity);
    }

    @Override
    @Transactional
    public void updateWorkPlanStatus(long id, ProductionPlanWorkPlanStatus workPlanStatus, String actorUserId) {
        ProductionPlanJpaEntity entity = planRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + id));
        entity.setWorkPlanStatus(workPlanStatus);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(Instant.now());
        planRepository.save(entity);
    }

    @Override
    @Transactional
    public void addProducedQty(long id, java.math.BigDecimal qty, String actorUserId) {
        updateProducedQty(id, qty, actorUserId);
    }

    @Override
    @Transactional
    public void subtractProducedQty(long id, java.math.BigDecimal qty, String actorUserId) {
        updateProducedQty(id, qty.negate(), actorUserId);
    }

    private void updateProducedQty(long id, java.math.BigDecimal delta, String actorUserId) {
        ProductionPlanJpaEntity entity = planRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("생산계획을 찾을 수 없습니다: " + id));
        java.math.BigDecimal next = entity.getProducedQty().add(delta);
        if (next.compareTo(java.math.BigDecimal.ZERO) < 0) {
            throw new IllegalStateException("생산 실적 수량이 음수가 됩니다.");
        }
        entity.setProducedQty(next);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(Instant.now());
        planRepository.save(entity);
    }
}
