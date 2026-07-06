package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.application.production.WorkOrderRepository;
import com.shindong.smartmanager.application.production.WorkOrderView;
import com.shindong.smartmanager.application.production.WorkReportConsumptionRecordView;
import com.shindong.smartmanager.application.production.WorkReportConsumptionSaveCommand;
import com.shindong.smartmanager.application.production.WorkReportHistorySaveCommand;
import com.shindong.smartmanager.application.production.WorkReportListCriteria;
import com.shindong.smartmanager.application.production.WorkReportRepository;
import com.shindong.smartmanager.application.production.WorkReportSaveCommand;
import com.shindong.smartmanager.application.production.WorkReportView;
import com.shindong.smartmanager.domain.production.WorkReportHistorySourceType;
import com.shindong.smartmanager.domain.production.WorkReportStatus;
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
public class JpaWorkReportRepository implements WorkReportRepository {

    private static final int ACTIVE = 1;

    @PersistenceContext
    private EntityManager entityManager;

    private final SpringDataWorkReportRepository workReportRepository;
    private final SpringDataWorkReportHistoryRepository historyRepository;
    private final SpringDataWorkOrderRepository workOrderJpaRepository;
    private final SpringDataWorkPlanRepository workPlanRepository;
    private final SpringDataProductionPlanRepository productionPlanRepository;
    private final SpringDataProcessSequenceRepository processRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final SpringDataWorkCenterRepository workCenterRepository;
    private final WorkOrderRepository workOrderRepository;
    private final SpringDataWorkReportConsumptionLineRepository consumptionLineRepository;

    public JpaWorkReportRepository(
            SpringDataWorkReportRepository workReportRepository,
            SpringDataWorkReportHistoryRepository historyRepository,
            SpringDataWorkOrderRepository workOrderJpaRepository,
            SpringDataWorkPlanRepository workPlanRepository,
            SpringDataProductionPlanRepository productionPlanRepository,
            SpringDataProcessSequenceRepository processRepository,
            SpringDataItemRepository itemRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            SpringDataWorkCenterRepository workCenterRepository,
            WorkOrderRepository workOrderRepository,
            SpringDataWorkReportConsumptionLineRepository consumptionLineRepository
    ) {
        this.workReportRepository = workReportRepository;
        this.historyRepository = historyRepository;
        this.workOrderJpaRepository = workOrderJpaRepository;
        this.workPlanRepository = workPlanRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.workCenterRepository = workCenterRepository;
        this.workOrderRepository = workOrderRepository;
        this.consumptionLineRepository = consumptionLineRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<WorkOrderView> findReportTargets() {
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
    @Transactional
    public WorkReportView save(WorkReportSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        WorkReportJpaEntity entity = new WorkReportJpaEntity();
        entity.setReportNum(command.reportNum());
        entity.setWorkOrderId(command.workOrderId());
        entity.setReportDate(command.reportDate());
        entity.setGoodQty(command.goodQty());
        entity.setScrapQty(command.scrapQty());
        entity.setSetupTime(command.setupTime());
        entity.setRunTime(command.runTime());
        entity.setWorkerName(command.workerName());
        entity.setStatus(command.status());
        entity.setStockApplied(command.stockApplied() ? 1 : 0);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return toView(workReportRepository.save(entity));
    }

    @Override
    @Transactional(readOnly = true)
    public List<WorkReportView> findAllActive(WorkReportListCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT wr.id
                FROM work_report wr
                JOIN work_order wo ON wo.id = wr.work_order_id
                JOIN work_plan wp ON wp.id = wo.work_plan_id
                JOIN production_plan pp ON pp.id = wp.production_plan_id
                JOIN item i ON i.id = pp.item_id
                JOIN process_sequence ps ON ps.id = wp.process_sequence_id
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                WHERE wr.recording_state = 1
                  AND wr.status = 'REGISTERED'
                """);
        Map<String, Object> params = new HashMap<>();
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
        if (criteria.orderNum() != null && !criteria.orderNum().isBlank()) {
            sql.append(" AND wo.order_num LIKE :orderNum");
            params.put("orderNum", "%" + criteria.orderNum().trim() + "%");
        }
        if (criteria.reportNum() != null && !criteria.reportNum().isBlank()) {
            sql.append(" AND wr.report_num LIKE :reportNum");
            params.put("reportNum", "%" + criteria.reportNum().trim() + "%");
        }
        if (criteria.reportDateFrom() != null) {
            sql.append(" AND wr.report_date >= :reportDateFrom");
            params.put("reportDateFrom", criteria.reportDateFrom());
        }
        if (criteria.reportDateTo() != null) {
            sql.append(" AND wr.report_date <= :reportDateTo");
            params.put("reportDateTo", criteria.reportDateTo());
        }
        sql.append(" ORDER BY wr.report_date DESC, wr.id DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Number> rows = query.getResultList();
        List<WorkReportView> result = new ArrayList<>();
        for (Number row : rows) {
            workReportRepository.findByIdAndRecordingStateAndStatus(row.longValue(), ACTIVE, WorkReportStatus.REGISTERED)
                    .map(this::toView)
                    .ifPresent(result::add);
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<WorkReportView> findActiveRegisteredById(long id) {
        return workReportRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, WorkReportStatus.REGISTERED)
                .map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public long countByReportNumPrefix(String prefix) {
        return workReportRepository.countByReportNumStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        WorkReportJpaEntity entity = workReportRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, WorkReportStatus.REGISTERED)
                .orElseThrow(() -> new IllegalArgumentException("작업일보를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setStatus(WorkReportStatus.CANCELLED);
        entity.setStockApplied(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        workReportRepository.save(entity);
    }

    @Override
    @Transactional
    public void saveHistory(WorkReportHistorySaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        WorkReportHistoryJpaEntity entity = new WorkReportHistoryJpaEntity();
        entity.setItemId(command.itemId());
        entity.setProductionPlanId(command.productionPlanId());
        entity.setProcessSequenceId(command.processSequenceId());
        entity.setGoodQty(command.goodQty());
        entity.setScrapQty(command.scrapQty());
        entity.setHistoryDate(command.historyDate());
        entity.setSourceType(command.sourceType());
        entity.setSourceId(command.sourceId());
        entity.setFiscalYear((short) command.fiscalYear());
        entity.setFiscalMonth((byte) command.fiscalMonth());
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        historyRepository.save(entity);
    }

    @Override
    @Transactional
    public void deactivateHistory(WorkReportHistorySourceType sourceType, long sourceId, String actorUserId) {
        for (WorkReportHistoryJpaEntity entity : historyRepository.findBySourceTypeAndSourceIdAndRecordingState(
                sourceType, sourceId, ACTIVE)) {
            entity.setRecordingState(0);
            historyRepository.save(entity);
        }
    }

    @Override
    @Transactional
    public List<WorkReportConsumptionRecordView> saveConsumptionLines(
            long workReportId,
            List<WorkReportConsumptionSaveCommand> commands,
            String actorUserId
    ) {
        Instant now = Instant.now();
        List<WorkReportConsumptionRecordView> result = new ArrayList<>();
        short lineNo = 0;
        for (WorkReportConsumptionSaveCommand command : commands) {
            lineNo++;
            WorkReportConsumptionLineJpaEntity entity = new WorkReportConsumptionLineJpaEntity();
            entity.setWorkReportId(workReportId);
            entity.setLineNo(lineNo);
            entity.setItemId(command.itemId());
            entity.setItemCompositionId(command.itemCompositionId());
            entity.setIssueQty(command.issueQty());
            entity.setLocationCode(command.locationCode());
            entity.setSourceProcessId(command.sourceProcessId());
            entity.setRecordingState(ACTIVE);
            entity.setCreatedBy(actorUserId);
            entity.setCreatedById(actorUserId);
            entity.setCreatedAt(now);
            entity.setUpdatedBy(actorUserId);
            entity.setUpdatedById(actorUserId);
            entity.setUpdatedAt(now);
            WorkReportConsumptionLineJpaEntity saved = consumptionLineRepository.save(entity);
            result.add(toConsumptionRecord(saved));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<WorkReportConsumptionRecordView> findActiveConsumptionLinesByReportId(long workReportId) {
        return consumptionLineRepository.findByWorkReportIdAndRecordingStateOrderByLineNoAsc(workReportId, ACTIVE)
                .stream()
                .map(this::toConsumptionRecord)
                .toList();
    }

    @Override
    @Transactional
    public void deactivateConsumptionLinesByReportId(long workReportId, String actorUserId) {
        Instant now = Instant.now();
        for (WorkReportConsumptionLineJpaEntity entity
                : consumptionLineRepository.findByWorkReportIdAndRecordingStateOrderByLineNoAsc(workReportId, ACTIVE)) {
            entity.setRecordingState(0);
            entity.setUpdatedBy(actorUserId);
            entity.setUpdatedById(actorUserId);
            entity.setUpdatedAt(now);
            consumptionLineRepository.save(entity);
        }
    }

    private WorkReportConsumptionRecordView toConsumptionRecord(WorkReportConsumptionLineJpaEntity entity) {
        return new WorkReportConsumptionRecordView(
                entity.getId(),
                entity.getWorkReportId(),
                entity.getLineNo(),
                entity.getItemId(),
                entity.getItemCompositionId(),
                entity.getIssueQty(),
                entity.getLocationCode(),
                entity.getSourceProcessId()
        );
    }

    private WorkReportView toView(WorkReportJpaEntity entity) {
        WorkOrderJpaEntity order = workOrderJpaRepository.findById(entity.getWorkOrderId()).orElse(null);
        WorkPlanJpaEntity plan = order != null ? workPlanRepository.findById(order.getWorkPlanId()).orElse(null) : null;
        ProductionPlanJpaEntity productionPlan = plan != null
                ? productionPlanRepository.findById(plan.getProductionPlanId()).orElse(null)
                : null;
        ProcessSequenceJpaEntity process = plan != null
                ? processRepository.findById(plan.getProcessSequenceId()).orElse(null)
                : null;
        ItemJpaEntity item = productionPlan != null
                ? itemRepository.findById(productionPlan.getItemId()).orElse(null)
                : null;
        PublicCodeJpaEntity processCode = process != null
                ? publicCodeRepository.findById(process.getPublicCodeId()).orElse(null)
                : null;
        String wcName = null;
        if (plan != null && plan.getWorkCenterId() != null) {
            wcName = workCenterRepository.findById(plan.getWorkCenterId())
                    .map(WorkCenterJpaEntity::getWcName)
                    .orElse(null);
        }
        return new WorkReportView(
                entity.getId(),
                entity.getReportNum(),
                entity.getWorkOrderId(),
                order != null ? order.getOrderNum() : "",
                productionPlan != null ? productionPlan.getId() : 0L,
                productionPlan != null ? productionPlan.getPlanNo() : "",
                productionPlan != null ? productionPlan.getItemId() : 0L,
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                plan != null ? plan.getProcessSequenceId() : 0L,
                process != null ? process.getProcessSequenceNum() : 0,
                processCode != null ? processCode.getSmallCode() : "",
                processCode != null ? processCode.getSmallName() : "",
                plan != null ? plan.getWorkCenterId() : null,
                wcName,
                entity.getReportDate(),
                entity.getGoodQty(),
                entity.getScrapQty(),
                entity.getSetupTime(),
                entity.getRunTime(),
                entity.getWorkerName(),
                entity.getStatus(),
                entity.getStatus() == WorkReportStatus.REGISTERED,
                entity.getCreatedAt(),
                entity.getCreatedBy()
        );
    }
}
