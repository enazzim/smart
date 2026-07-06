package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.application.production.MaterialIssueLineRecordView;
import com.shindong.smartmanager.application.production.MaterialIssueLineSaveCommand;
import com.shindong.smartmanager.application.production.MaterialIssueListCriteria;
import com.shindong.smartmanager.application.production.MaterialIssueRepository;
import com.shindong.smartmanager.application.production.MaterialIssueSaveCommand;
import com.shindong.smartmanager.application.production.MaterialIssueView;
import com.shindong.smartmanager.application.production.WorkOrderRepository;
import com.shindong.smartmanager.application.production.WorkOrderView;
import com.shindong.smartmanager.domain.production.MaterialIssueStatus;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.process.ProcessSequenceJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;
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
public class JpaMaterialIssueRepository implements MaterialIssueRepository {

    private static final int ACTIVE = 1;

    @PersistenceContext
    private EntityManager entityManager;

    private final SpringDataMaterialIssueRepository issueRepository;
    private final SpringDataMaterialIssueLineRepository lineRepository;
    private final WorkOrderRepository workOrderRepository;
    private final SpringDataWorkOrderRepository workOrderJpaRepository;
    private final SpringDataWorkPlanRepository workPlanRepository;
    private final SpringDataProductionPlanRepository productionPlanRepository;
    private final SpringDataProcessSequenceRepository processRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;

    public JpaMaterialIssueRepository(
            SpringDataMaterialIssueRepository issueRepository,
            SpringDataMaterialIssueLineRepository lineRepository,
            WorkOrderRepository workOrderRepository,
            SpringDataWorkOrderRepository workOrderJpaRepository,
            SpringDataWorkPlanRepository workPlanRepository,
            SpringDataProductionPlanRepository productionPlanRepository,
            SpringDataProcessSequenceRepository processRepository,
            SpringDataItemRepository itemRepository,
            SpringDataPublicCodeRepository publicCodeRepository
    ) {
        this.issueRepository = issueRepository;
        this.lineRepository = lineRepository;
        this.workOrderRepository = workOrderRepository;
        this.workOrderJpaRepository = workOrderJpaRepository;
        this.workPlanRepository = workPlanRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.publicCodeRepository = publicCodeRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<WorkOrderView> findIssueTargets() {
        String sql = """
                SELECT wo.id
                FROM work_order wo
                WHERE wo.recording_state = 1
                  AND wo.status = 'ISSUED'
                  AND wo.reported_qty < wo.ordered_qty
                ORDER BY wo.created_at ASC, wo.id ASC
                """;
        @SuppressWarnings("unchecked")
        List<Number> rows = entityManager.createNativeQuery(sql).getResultList();
        List<WorkOrderView> result = new ArrayList<>();
        for (Number row : rows) {
            workOrderRepository.findActiveIssuedById(row.longValue()).ifPresent(result::add);
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public long countByIssueNumPrefix(String prefix) {
        return issueRepository.countByIssueNumStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional
    public MaterialIssueView save(MaterialIssueSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        MaterialIssueJpaEntity entity = new MaterialIssueJpaEntity();
        entity.setIssueNum(command.issueNum());
        entity.setWorkOrderId(command.workOrderId());
        entity.setIssueDate(command.issueDate());
        entity.setStatus(command.status());
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return toView(issueRepository.save(entity));
    }

    @Override
    @Transactional
    public List<MaterialIssueLineRecordView> saveLines(
            long materialIssueId,
            List<MaterialIssueLineSaveCommand> lines,
            String actorUserId
    ) {
        short lineNo = 1;
        List<MaterialIssueLineRecordView> result = new ArrayList<>();
        for (MaterialIssueLineSaveCommand line : lines) {
            MaterialIssueLineJpaEntity entity = new MaterialIssueLineJpaEntity();
            entity.setMaterialIssueId(materialIssueId);
            entity.setLineNo(lineNo++);
            entity.setItemId(line.itemId());
            entity.setItemCompositionId(line.itemCompositionId());
            entity.setIssueQty(line.issueQty());
            entity.setSourceLocationCode(line.locationCode());
            entity.setSourceProcessId(line.sourceProcessId());
            entity.setRecordingState(ACTIVE);
            MaterialIssueLineJpaEntity saved = lineRepository.save(entity);
            result.add(new MaterialIssueLineRecordView(
                    saved.getId(),
                    saved.getItemId(),
                    saved.getItemCompositionId(),
                    saved.getIssueQty(),
                    saved.getSourceLocationCode(),
                    saved.getSourceProcessId()
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<MaterialIssueView> findActiveIssuedById(long id) {
        return issueRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, MaterialIssueStatus.ISSUED)
                .map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public List<MaterialIssueView> findAllActive(MaterialIssueListCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT mi.id
                FROM material_issue mi
                JOIN work_order wo ON wo.id = mi.work_order_id
                JOIN work_plan wp ON wp.id = wo.work_plan_id
                JOIN production_plan pp ON pp.id = wp.production_plan_id
                JOIN item i ON i.id = pp.item_id
                WHERE mi.recording_state = 1
                  AND mi.status = 'ISSUED'
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
            if (criteria.orderNum() != null && !criteria.orderNum().isBlank()) {
                sql.append(" AND wo.order_num LIKE :orderNum");
                params.put("orderNum", "%" + criteria.orderNum().trim() + "%");
            }
            if (criteria.issueNum() != null && !criteria.issueNum().isBlank()) {
                sql.append(" AND mi.issue_num LIKE :issueNum");
                params.put("issueNum", "%" + criteria.issueNum().trim() + "%");
            }
            if (criteria.issueDateFrom() != null) {
                sql.append(" AND mi.issue_date >= :issueDateFrom");
                params.put("issueDateFrom", criteria.issueDateFrom());
            }
            if (criteria.issueDateTo() != null) {
                sql.append(" AND mi.issue_date <= :issueDateTo");
                params.put("issueDateTo", criteria.issueDateTo());
            }
        }
        sql.append(" ORDER BY mi.issue_date DESC, mi.id DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Number> rows = query.getResultList();
        List<MaterialIssueView> result = new ArrayList<>();
        for (Number row : rows) {
            issueRepository.findByIdAndRecordingStateAndStatus(row.longValue(), ACTIVE, MaterialIssueStatus.ISSUED)
                    .map(this::toView)
                    .ifPresent(result::add);
        }
        return result;
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        MaterialIssueJpaEntity entity = issueRepository.findByIdAndRecordingStateAndStatus(
                        id, ACTIVE, MaterialIssueStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("자재투입을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setStatus(MaterialIssueStatus.CANCELLED);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        issueRepository.save(entity);

        List<MaterialIssueLineJpaEntity> lines = lineRepository
                .findByMaterialIssueIdAndRecordingStateOrderByLineNoAsc(id, ACTIVE);
        for (MaterialIssueLineJpaEntity line : lines) {
            line.setRecordingState(0);
            lineRepository.save(line);
        }
    }

    @Override
    @Transactional(readOnly = true)
    public Map<Long, BigDecimal> sumIssuedQtyByWorkOrderId(long workOrderId) {
        Map<Long, BigDecimal> result = new HashMap<>();
        for (Object[] row : lineRepository.sumIssuedQtyByWorkOrderId(workOrderId)) {
            if (row[0] == null) {
                continue;
            }
            long compositionId = ((Number) row[0]).longValue();
            BigDecimal qty = row[1] instanceof BigDecimal bigDecimal
                    ? bigDecimal
                    : BigDecimal.valueOf(((Number) row[1]).doubleValue());
            result.put(compositionId, qty);
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<MaterialIssueLineRecordView> findActiveLinesByIssueId(long issueId) {
        return lineRepository.findByMaterialIssueIdAndRecordingStateOrderByLineNoAsc(issueId, ACTIVE).stream()
                .map(line -> new MaterialIssueLineRecordView(
                        line.getId(),
                        line.getItemId(),
                        line.getItemCompositionId(),
                        line.getIssueQty(),
                        line.getSourceLocationCode(),
                        line.getSourceProcessId()
                ))
                .toList();
    }

    private MaterialIssueView toView(MaterialIssueJpaEntity entity) {
        WorkOrderJpaEntity workOrder = workOrderJpaRepository.findById(entity.getWorkOrderId())
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + entity.getWorkOrderId()));
        WorkPlanJpaEntity workPlan = workPlanRepository.findById(workOrder.getWorkPlanId())
                .orElseThrow(() -> new IllegalArgumentException("작업계획을 찾을 수 없습니다: " + workOrder.getWorkPlanId()));
        ProductionPlanJpaEntity productionPlan = productionPlanRepository.findById(workPlan.getProductionPlanId())
                .orElseThrow(() -> new IllegalArgumentException(
                        "생산계획을 찾을 수 없습니다: " + workPlan.getProductionPlanId()));
        ItemJpaEntity item = itemRepository.findById(productionPlan.getItemId())
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + productionPlan.getItemId()));
        ProcessSequenceJpaEntity process = processRepository.findById(workPlan.getProcessSequenceId())
                .orElseThrow(() -> new IllegalArgumentException(
                        "공정을 찾을 수 없습니다: " + workPlan.getProcessSequenceId()));
        PublicCodeJpaEntity processCode = publicCodeRepository.findById(process.getPublicCodeId()).orElse(null);
        String processName = processCode != null ? processCode.getSmallName() : "—";

        return new MaterialIssueView(
                entity.getId(),
                entity.getIssueNum(),
                entity.getWorkOrderId(),
                workOrder.getOrderNum(),
                item.getId(),
                item.getItemNo(),
                item.getItemName(),
                processName,
                entity.getIssueDate(),
                entity.getStatus(),
                entity.getStatus() == MaterialIssueStatus.ISSUED ? "등록" : "취소",
                entity.getStatus() == MaterialIssueStatus.ISSUED,
                entity.getCreatedAt(),
                entity.getCreatedBy()
        );
    }
}
