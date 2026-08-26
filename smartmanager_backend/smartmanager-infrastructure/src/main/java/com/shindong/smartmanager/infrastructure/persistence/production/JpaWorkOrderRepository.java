package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.production.WorkOrderListCriteria;
import com.shindong.smartmanager.application.production.WorkOrderRepository;
import com.shindong.smartmanager.application.production.WorkOrderSaveCommand;
import com.shindong.smartmanager.application.production.WorkOrderView;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.WorkOrderStatus;
import com.shindong.smartmanager.domain.production.WorkPlanStatus;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.process.ProcessSequenceJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.SpringDataWorkCenterRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.WorkCenterJpaEntity;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaWorkOrderRepository implements WorkOrderRepository {

    private static final int ACTIVE = 1;

    @PersistenceContext
    private EntityManager entityManager;

    private final SpringDataWorkOrderRepository workOrderRepository;
    private final SpringDataWorkPlanRepository workPlanRepository;
    private final SpringDataProductionPlanRepository productionPlanRepository;
    private final SpringDataProcessSequenceRepository processRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final SpringDataWorkCenterRepository workCenterRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaWorkOrderRepository(
            SpringDataWorkOrderRepository workOrderRepository,
            SpringDataWorkPlanRepository workPlanRepository,
            SpringDataProductionPlanRepository productionPlanRepository,
            SpringDataProcessSequenceRepository processRepository,
            SpringDataItemRepository itemRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            SpringDataWorkCenterRepository workCenterRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.workOrderRepository = workOrderRepository;
        this.workPlanRepository = workPlanRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.workCenterRepository = workCenterRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional(readOnly = true)
    public List<WorkOrderView> findOrderTargets() {
        String sql = """
                SELECT wp.id
                FROM work_plan wp
                WHERE wp.recording_state = 1
                  AND wp.status = 'PLANNED'
                  AND wp.work_distinction = 'INHOUSE'
                  AND NOT EXISTS (
                    SELECT 1 FROM work_order wo
                    WHERE wo.work_plan_id = wp.id
                      AND wo.recording_state = 1
                      AND wo.status = 'ISSUED'
                  )
                ORDER BY wp.plan_start_date ASC, wp.id ASC
                """;
        @SuppressWarnings("unchecked")
        List<Number> rows = entityManager.createNativeQuery(sql).getResultList();
        List<WorkOrderView> result = new ArrayList<>();
        for (Number row : rows) {
            workPlanRepository.findById(row.longValue())
                    .map(this::toTargetView)
                    .ifPresent(result::add);
        }
        return result;
    }

    @Override
    @Transactional
    public List<WorkOrderView> saveAll(List<WorkOrderSaveCommand> commands, String actorUserId) {
        Instant now = Instant.now();
        List<WorkOrderView> result = new ArrayList<>();
        for (WorkOrderSaveCommand command : commands) {
            purgeInactiveByWorkPlanId(command.workPlanId());
            WorkOrderJpaEntity entity = new WorkOrderJpaEntity();
            entity.setWorkPlanId(command.workPlanId());
            entity.setOrderNum(command.orderNum());
            entity.setOrderedQty(command.orderedQty());
            entity.setReportedQty(BigDecimal.ZERO);
            entity.setStatus(WorkOrderStatus.ISSUED);
            entity.setRecordingState(ACTIVE);
            entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setCreatedAt(now);
            entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setUpdatedAt(now);
            result.add(toView(workOrderRepository.save(entity)));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<WorkOrderView> findAllActive(WorkOrderListCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT wo.id
                FROM work_order wo
                JOIN work_plan wp ON wp.id = wo.work_plan_id
                JOIN production_plan pp ON pp.id = wp.production_plan_id
                JOIN process_sequence ps ON ps.id = wp.process_sequence_id
                JOIN item i ON i.id = ps.item_id
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                LEFT JOIN work_center wc ON wc.id = wp.work_center_id
                WHERE wo.recording_state = 1
                  AND wo.status = 'ISSUED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendListFilters(sql, params, criteria);
        sql.append(" ORDER BY wo.created_at DESC, wo.id DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Number> rows = query.getResultList();
        List<WorkOrderView> result = new ArrayList<>();
        for (Number row : rows) {
            workOrderRepository.findByIdAndRecordingStateAndStatus(row.longValue(), ACTIVE, WorkOrderStatus.ISSUED)
                    .map(this::toView)
                    .ifPresent(result::add);
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<WorkOrderView> findActiveIssuedById(long id) {
        return workOrderRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, WorkOrderStatus.ISSUED)
                .map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<WorkOrderView> findActiveById(long id) {
        return workOrderRepository.findByIdAndRecordingState(id, ACTIVE)
                .map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public long countByOrderNumPrefix(String prefix) {
        return workOrderRepository.countByOrderNumStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional(readOnly = true)
    public boolean hasActiveDownstream(long workOrderId) {
        Number workReports = (Number) entityManager.createNativeQuery("""
                SELECT COUNT(*)
                FROM work_report
                WHERE work_order_id = :workOrderId
                  AND status = 'REGISTERED'
                  AND recording_state = 1
                """)
                .setParameter("workOrderId", workOrderId)
                .getSingleResult();
        if (workReports != null && workReports.longValue() > 0) {
            return true;
        }
        Number materialIssues = (Number) entityManager.createNativeQuery("""
                SELECT COUNT(*)
                FROM material_issue
                WHERE work_order_id = :workOrderId
                  AND status = 'ISSUED'
                  AND recording_state = 1
                """)
                .setParameter("workOrderId", workOrderId)
                .getSingleResult();
        return materialIssues != null && materialIssues.longValue() > 0;
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        WorkOrderJpaEntity entity = workOrderRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, WorkOrderStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + id));
        if (entity.getReportedQty().compareTo(BigDecimal.ZERO) > 0) {
            throw new IllegalStateException("실적이 등록된 작업지시는 취소할 수 없습니다.");
        }
        if (hasActiveDownstream(id)) {
            throw new IllegalStateException(
                    "등록된 작업일보 또는 자재투입이 있어 작업지시를 취소할 수 없습니다."
            );
        }
        purgeCancelledDownstream(id);
        Number remainingReports = (Number) entityManager.createNativeQuery("""
                SELECT COUNT(*) FROM work_report WHERE work_order_id = :workOrderId
                """)
                .setParameter("workOrderId", id)
                .getSingleResult();
        Number remainingIssues = (Number) entityManager.createNativeQuery("""
                SELECT COUNT(*) FROM material_issue WHERE work_order_id = :workOrderId
                """)
                .setParameter("workOrderId", id)
                .getSingleResult();
        if ((remainingReports != null && remainingReports.longValue() > 0)
                || (remainingIssues != null && remainingIssues.longValue() > 0)) {
            throw new IllegalStateException(
                    "작업일보 또는 자재투입 이력이 남아 작업지시를 삭제할 수 없습니다."
            );
        }
        workOrderRepository.delete(entity);
        workOrderRepository.flush();
    }

    /** 취소 상태 하위 전표만 물리 삭제해 work_order FK를 해제한다. */
    private void purgeCancelledDownstream(long workOrderId) {
        entityManager.createNativeQuery("""
                DELETE wcl FROM work_report_consumption_line wcl
                INNER JOIN work_report wr ON wr.id = wcl.work_report_id
                WHERE wr.work_order_id = :workOrderId
                  AND wr.status = 'CANCELLED'
                """)
                .setParameter("workOrderId", workOrderId)
                .executeUpdate();
        entityManager.createNativeQuery("""
                DELETE FROM work_report
                WHERE work_order_id = :workOrderId
                  AND status = 'CANCELLED'
                """)
                .setParameter("workOrderId", workOrderId)
                .executeUpdate();
        entityManager.createNativeQuery("""
                DELETE mil FROM material_issue_line mil
                INNER JOIN material_issue mi ON mi.id = mil.material_issue_id
                WHERE mi.work_order_id = :workOrderId
                  AND mi.status = 'CANCELLED'
                """)
                .setParameter("workOrderId", workOrderId)
                .executeUpdate();
        entityManager.createNativeQuery("""
                DELETE FROM material_issue
                WHERE work_order_id = :workOrderId
                  AND status = 'CANCELLED'
                """)
                .setParameter("workOrderId", workOrderId)
                .executeUpdate();
        entityManager.flush();
    }

    @Override
    @Transactional
    public void addReportedQty(long id, BigDecimal goodQty, String actorUserId) {
        updateReportedQty(id, goodQty, actorUserId);
    }

    @Override
    @Transactional
    public void subtractReportedQty(long id, BigDecimal goodQty, String actorUserId) {
        updateReportedQty(id, goodQty.negate(), actorUserId);
    }

    @Override
    @Transactional
    public void purgeInactiveByWorkPlanId(long workPlanId) {
        workOrderRepository.deleteInactiveByWorkPlanId(workPlanId, 0);
        workOrderRepository.flush();
    }

    private void updateReportedQty(long id, BigDecimal delta, String actorUserId) {
        WorkOrderJpaEntity entity = workOrderRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, WorkOrderStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + id));
        BigDecimal next = entity.getReportedQty().add(delta);
        if (next.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalStateException("작업지시 실적 수량이 음수가 됩니다.");
        }
        if (next.compareTo(entity.getOrderedQty()) > 0) {
            throw new IllegalStateException("작업지시 수량을 초과할 수 없습니다.");
        }
        entity.setReportedQty(next);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(Instant.now());
        workOrderRepository.save(entity);
    }

    private void appendListFilters(StringBuilder sql, Map<String, Object> params, WorkOrderListCriteria criteria) {
        if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND i.item_no LIKE :itemNo");
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            sql.append(" AND i.item_name LIKE :itemName");
            params.put("itemName", "%" + criteria.itemName().trim() + "%");
        }
        if (criteria.processName() != null && !criteria.processName().isBlank()) {
            sql.append(" AND pc.small_name LIKE :processName");
            params.put("processName", "%" + criteria.processName().trim() + "%");
        }
        if (criteria.workCenterName() != null && !criteria.workCenterName().isBlank()) {
            sql.append(" AND wc.wc_name LIKE :workCenterName");
            params.put("workCenterName", "%" + criteria.workCenterName().trim() + "%");
        }
        if (criteria.orderNum() != null && !criteria.orderNum().isBlank()) {
            sql.append(" AND wo.order_num LIKE :orderNum");
            params.put("orderNum", "%" + criteria.orderNum().trim() + "%");
        }
        if (criteria.planStartDateFrom() != null) {
            sql.append(" AND wp.plan_start_date >= :planStartDateFrom");
            params.put("planStartDateFrom", criteria.planStartDateFrom());
        }
        if (criteria.planStartDateTo() != null) {
            sql.append(" AND wp.plan_start_date <= :planStartDateTo");
            params.put("planStartDateTo", criteria.planStartDateTo());
        }
    }

    private WorkOrderView toTargetView(WorkPlanJpaEntity plan) {
        WorkOrderView base = toViewFromPlan(plan, null);
        return new WorkOrderView(
                0L,
                plan.getId(),
                "",
                base.productionPlanId(),
                base.planNo(),
                base.itemId(),
                base.itemNo(),
                base.itemName(),
                base.processSequenceId(),
                base.processSequenceNum(),
                base.processCode(),
                base.processName(),
                base.workCenterId(),
                base.workCenterName(),
                plan.getPlannedQty(),
                BigDecimal.ZERO,
                plan.getPlannedQty(),
                plan.getPlanStartDate(),
                WorkOrderStatus.ISSUED,
                true,
                plan.getCreatedAt(),
                masterAuditActorLookup.nameOf(plan.getCreatedById())
        );
    }

    private WorkOrderView toView(WorkOrderJpaEntity entity) {
        WorkPlanJpaEntity plan = workPlanRepository.findById(entity.getWorkPlanId())
                .orElseThrow(() -> new IllegalStateException("작업계획을 찾을 수 없습니다: " + entity.getWorkPlanId()));
        WorkOrderView base = toViewFromPlan(plan, entity);
        BigDecimal remaining = entity.getOrderedQty().subtract(entity.getReportedQty());
        return new WorkOrderView(
                entity.getId(),
                entity.getWorkPlanId(),
                entity.getOrderNum(),
                base.productionPlanId(),
                base.planNo(),
                base.itemId(),
                base.itemNo(),
                base.itemName(),
                base.processSequenceId(),
                base.processSequenceNum(),
                base.processCode(),
                base.processName(),
                base.workCenterId(),
                base.workCenterName(),
                entity.getOrderedQty(),
                entity.getReportedQty(),
                remaining,
                plan.getPlanStartDate(),
                entity.getStatus(),
                entity.getStatus() == WorkOrderStatus.ISSUED
                        && entity.getReportedQty().compareTo(BigDecimal.ZERO) == 0
                        && !hasActiveDownstream(entity.getId()),
                entity.getCreatedAt(),
                masterAuditActorLookup.nameOf(entity.getCreatedById())
        );
    }

    private WorkOrderView toViewFromPlan(WorkPlanJpaEntity plan, WorkOrderJpaEntity order) {
        ProductionPlanJpaEntity productionPlan = productionPlanRepository.findById(plan.getProductionPlanId()).orElse(null);
        ProcessSequenceJpaEntity process = processRepository.findById(plan.getProcessSequenceId()).orElse(null);
        ItemJpaEntity item = process != null ? itemRepository.findById(process.getItemId()).orElse(null) : null;
        PublicCodeJpaEntity processCode = process != null
                ? publicCodeRepository.findById(process.getPublicCodeId()).orElse(null)
                : null;
        String wcName = null;
        if (plan.getWorkCenterId() != null) {
            wcName = workCenterRepository.findById(plan.getWorkCenterId())
                    .map(WorkCenterJpaEntity::getWcName)
                    .orElse(null);
        }
        BigDecimal ordered = order != null ? order.getOrderedQty() : plan.getPlannedQty();
        BigDecimal reported = order != null ? order.getReportedQty() : BigDecimal.ZERO;
        return new WorkOrderView(
                order != null ? order.getId() : 0L,
                plan.getId(),
                order != null ? order.getOrderNum() : "",
                productionPlan != null ? productionPlan.getId() : 0L,
                productionPlan != null ? productionPlan.getPlanNo() : "",
                process != null ? process.getItemId() : 0L,
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                plan.getProcessSequenceId(),
                process != null ? process.getProcessSequenceNum() : 0,
                processCode != null ? processCode.getSmallCode() : "",
                processCode != null ? processCode.getSmallName() : "",
                plan.getWorkCenterId(),
                wcName,
                ordered,
                reported,
                ordered.subtract(reported),
                plan.getPlanStartDate(),
                order != null ? order.getStatus() : WorkOrderStatus.ISSUED,
                order == null || (order.getStatus() == WorkOrderStatus.ISSUED
                        && order.getReportedQty().compareTo(BigDecimal.ZERO) == 0),
                order != null ? order.getCreatedAt() : plan.getCreatedAt(),
                order != null ? masterAuditActorLookup.nameOf(order.getCreatedById()) : masterAuditActorLookup.nameOf(plan.getCreatedById())
        );
    }
}
