package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;



import com.shindong.smartmanager.application.production.WorkPlanListCriteria;

import com.shindong.smartmanager.application.production.WorkPlanRepository;

import com.shindong.smartmanager.application.production.WorkPlanSaveCommand;

import com.shindong.smartmanager.application.production.WorkPlanView;

import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.WorkPlanStatus;

import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;

import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;

import com.shindong.smartmanager.infrastructure.persistence.process.ProcessSequenceJpaEntity;

import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;

import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;

import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;

import com.shindong.smartmanager.infrastructure.persistence.workcenter.SpringDataWorkCenterRepository;

import com.shindong.smartmanager.infrastructure.persistence.workcenter.WorkCenterJpaEntity;

import jakarta.persistence.EntityManager;

import jakarta.persistence.PersistenceContext;

import jakarta.persistence.Query;

import java.time.Instant;

import java.time.LocalDate;

import java.util.ArrayList;

import java.util.HashMap;

import java.util.List;

import java.util.Map;

import java.util.Optional;

import org.springframework.stereotype.Repository;

import org.springframework.transaction.annotation.Transactional;



@Repository

public class JpaWorkPlanRepository implements WorkPlanRepository {



    private static final int ACTIVE = 1;



    @PersistenceContext

    private EntityManager entityManager;



    private final SpringDataWorkPlanRepository workPlanRepository;

    private final SpringDataProductionPlanRepository productionPlanRepository;

    private final SpringDataProcessSequenceRepository processRepository;

    private final SpringDataItemRepository itemRepository;

    private final SpringDataPublicCodeRepository publicCodeRepository;

    private final SpringDataWorkCenterRepository workCenterRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;



    public JpaWorkPlanRepository(

            SpringDataWorkPlanRepository workPlanRepository,

            SpringDataProductionPlanRepository productionPlanRepository,

            SpringDataProcessSequenceRepository processRepository,

            SpringDataItemRepository itemRepository,

            SpringDataPublicCodeRepository publicCodeRepository,

            SpringDataWorkCenterRepository workCenterRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {

        this.workPlanRepository = workPlanRepository;

        this.productionPlanRepository = productionPlanRepository;

        this.processRepository = processRepository;

        this.itemRepository = itemRepository;

        this.publicCodeRepository = publicCodeRepository;

        this.workCenterRepository = workCenterRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;

    }



    @Override

    @Transactional

    public List<WorkPlanView> saveAll(List<WorkPlanSaveCommand> commands, String actorUserId) {

        Instant now = Instant.now();

        List<WorkPlanJpaEntity> saved = new ArrayList<>();

        for (WorkPlanSaveCommand command : commands) {

            purgeInactiveHistory(
                    command.productionPlanId(),
                    command.processSequenceId(),
                    command.workDistinction()
            );

            WorkPlanJpaEntity entity = new WorkPlanJpaEntity();

            entity.setProductionPlanId(command.productionPlanId());

            entity.setProcessSequenceId(command.processSequenceId());

            entity.setWorkCenterId(command.workCenterId());

            entity.setWorkDistinction(command.workDistinction());

            entity.setPlannedQty(command.plannedQty());

            entity.setPlanStartDate(command.planStartDate());

            entity.setPlanEndDate(command.planEndDate());

            entity.setSetupTime(command.setupTime());

            entity.setStandardTime(command.standardTime());

            entity.setStatus(WorkPlanStatus.PLANNED);

            entity.setRecordingState(ACTIVE);

            entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));

            entity.setCreatedAt(now);

            entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));

            entity.setUpdatedAt(now);

            saved.add(workPlanRepository.save(entity));

        }

        return saved.stream().map(this::toView).toList();

    }



    @Override

    @Transactional(readOnly = true)

    public List<WorkPlanView> findAllActive(WorkPlanListCriteria criteria) {

        if (criteria == null || isListCriteriaEmpty(criteria)) {

            return workPlanRepository.findByRecordingStateOrderByPlanStartDateDescIdDesc(ACTIVE).stream()

                    .filter(entity -> entity.getStatus() == WorkPlanStatus.PLANNED)

                    .map(this::toView)

                    .toList();

        }



        StringBuilder sql = new StringBuilder("""

                SELECT wp.id

                FROM work_plan wp

                JOIN production_plan pp ON pp.id = wp.production_plan_id

                JOIN process_sequence ps ON ps.id = wp.process_sequence_id

                JOIN item i ON i.id = ps.item_id

                LEFT JOIN public_code pc ON pc.id = ps.public_code_id

                LEFT JOIN work_center wc ON wc.id = wp.work_center_id

                WHERE wp.recording_state = 1

                  AND wp.status = 'PLANNED'

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

        if (criteria.workCenterName() != null && !criteria.workCenterName().isBlank()) {

            sql.append(" AND wc.wc_name LIKE :workCenterName");

            params.put("workCenterName", "%" + criteria.workCenterName().trim() + "%");

        }

        if (criteria.planStartDateFrom() != null) {

            sql.append(" AND wp.plan_start_date >= :planStartDateFrom");

            params.put("planStartDateFrom", criteria.planStartDateFrom());

        }

        if (criteria.planStartDateTo() != null) {

            sql.append(" AND wp.plan_start_date <= :planStartDateTo");

            params.put("planStartDateTo", criteria.planStartDateTo());

        }

        sql.append(" ORDER BY wp.plan_start_date DESC, wp.id DESC");



        Query query = entityManager.createNativeQuery(sql.toString());

        params.forEach(query::setParameter);



        @SuppressWarnings("unchecked")

        List<Number> rows = query.getResultList();

        List<WorkPlanView> result = new ArrayList<>();

        for (Number row : rows) {

            workPlanRepository.findById(row.longValue())

                    .filter(entity -> entity.getRecordingState() == ACTIVE

                            && entity.getStatus() == WorkPlanStatus.PLANNED)

                    .map(this::toView)

                    .ifPresent(result::add);

        }

        return result;

    }



    @Override

    @Transactional(readOnly = true)

    public Optional<WorkPlanView> findActivePlannedById(long id) {

        return workPlanRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, WorkPlanStatus.PLANNED)

                .map(this::toView);

    }



    @Override

    @Transactional(readOnly = true)

    public Optional<WorkPlanView> findActiveById(long id) {

        return workPlanRepository.findById(id)

                .filter(entity -> entity.getRecordingState() == ACTIVE)

                .map(this::toView);

    }



    @Override

    @Transactional(readOnly = true)

    public List<WorkPlanView> findActivePlannedByProductionPlanId(long productionPlanId) {

        return workPlanRepository.findByProductionPlanIdAndRecordingStateAndStatusOrderByProcessSequenceIdAscIdAsc(

                productionPlanId,

                ACTIVE,

                WorkPlanStatus.PLANNED

        ).stream().map(this::toView).toList();

    }



    @Override

    @Transactional(readOnly = true)

    public long countActivePlannedByProductionPlanId(long productionPlanId) {

        return workPlanRepository.countByProductionPlanIdAndRecordingStateAndStatus(

                productionPlanId,

                ACTIVE,

                WorkPlanStatus.PLANNED

        );

    }



    @Override

    @Transactional(readOnly = true)

    public List<WorkPlanView> findActivePlannedForLoad(Long workCenterId, LocalDate from, LocalDate to) {

        StringBuilder sql = new StringBuilder("""

                SELECT wp.id

                FROM work_plan wp

                WHERE wp.recording_state = 1

                  AND wp.status = 'PLANNED'

                  AND wp.work_center_id IS NOT NULL

                  AND wp.plan_start_date >= :fromDate

                  AND wp.plan_start_date <= :toDate

                """);

        Map<String, Object> params = new HashMap<>();

        params.put("fromDate", from);

        params.put("toDate", to);

        if (workCenterId != null) {

            sql.append(" AND wp.work_center_id = :workCenterId");

            params.put("workCenterId", workCenterId);

        }

        sql.append(" ORDER BY wp.work_center_id ASC, wp.plan_start_date ASC, wp.id ASC");



        Query query = entityManager.createNativeQuery(sql.toString());

        params.forEach(query::setParameter);



        @SuppressWarnings("unchecked")

        List<Number> rows = query.getResultList();

        List<WorkPlanView> result = new ArrayList<>();

        for (Number row : rows) {

            workPlanRepository.findById(row.longValue())

                    .filter(entity -> entity.getRecordingState() == ACTIVE

                            && entity.getStatus() == WorkPlanStatus.PLANNED

                            && entity.getWorkCenterId() != null)

                    .map(this::toView)

                    .ifPresent(result::add);

        }

        return result;

    }



    @Override

    @Transactional

    public void cancelById(long id, String actorUserId) {

        WorkPlanJpaEntity entity = workPlanRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, WorkPlanStatus.PLANNED)

                .orElseThrow(() -> new IllegalArgumentException("작업계획을 찾을 수 없습니다: " + id));

        Instant now = Instant.now();

        purgeInactiveHistory(
                entity.getProductionPlanId(),
                entity.getProcessSequenceId(),
                entity.getWorkDistinction()
        );

        entity.setStatus(WorkPlanStatus.CANCELLED);

        entity.setRecordingState(0);

        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));

        entity.setUpdatedAt(now);

        workPlanRepository.save(entity);

    }



    @Override

    @Transactional

    public void cancelByProductionPlanId(long productionPlanId, String actorUserId) {

        Instant now = Instant.now();

        for (WorkPlanJpaEntity entity : workPlanRepository.findByProductionPlanIdAndRecordingStateAndStatusOrderByProcessSequenceIdAscIdAsc(

                productionPlanId,

                ACTIVE,

                WorkPlanStatus.PLANNED

        )) {

            purgeInactiveHistory(
                    entity.getProductionPlanId(),
                    entity.getProcessSequenceId(),
                    entity.getWorkDistinction()
            );

            entity.setStatus(WorkPlanStatus.CANCELLED);

            entity.setRecordingState(0);

            entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));

            entity.setUpdatedAt(now);

            workPlanRepository.save(entity);

        }

    }



    private void purgeInactiveHistory(
            long productionPlanId,
            long processSequenceId,
            WorkDistinction workDistinction
    ) {
        workPlanRepository.deleteInactiveByKey(productionPlanId, processSequenceId, workDistinction, 0);
        workPlanRepository.flush();
    }



    private static boolean isListCriteriaEmpty(WorkPlanListCriteria criteria) {

        return (criteria.itemNo() == null || criteria.itemNo().isBlank())

                && (criteria.itemName() == null || criteria.itemName().isBlank())

                && (criteria.processName() == null || criteria.processName().isBlank())

                && (criteria.workCenterName() == null || criteria.workCenterName().isBlank())

                && criteria.planStartDateFrom() == null

                && criteria.planStartDateTo() == null;

    }



    private WorkPlanView toView(WorkPlanJpaEntity entity) {

        ProductionPlanJpaEntity plan = productionPlanRepository.findById(entity.getProductionPlanId()).orElse(null);

        ProcessSequenceJpaEntity process = processRepository.findById(entity.getProcessSequenceId()).orElse(null);

        ItemJpaEntity item = process != null ? itemRepository.findById(process.getItemId()).orElse(null) : null;

        PublicCodeJpaEntity processCode = process != null

                ? publicCodeRepository.findById(process.getPublicCodeId()).orElse(null)

                : null;

        String wcName = null;

        if (entity.getWorkCenterId() != null) {

            wcName = workCenterRepository.findById(entity.getWorkCenterId())

                    .map(WorkCenterJpaEntity::getWcName)

                    .orElse(null);

        }

        return new WorkPlanView(

                entity.getId(),

                entity.getProductionPlanId(),

                plan != null ? plan.getPlanNo() : "",

                process != null ? process.getItemId() : 0L,

                item != null ? item.getItemNo() : "",

                item != null ? item.getItemName() : "",

                entity.getProcessSequenceId(),

                process != null ? process.getProcessSequenceNum() : 0,

                processCode != null ? processCode.getSmallCode() : "",

                processCode != null ? processCode.getSmallName() : "",

                entity.getWorkDistinction(),

                entity.getWorkCenterId(),

                wcName,

                entity.getPlannedQty(),

                entity.getPlanStartDate(),

                entity.getPlanEndDate(),

                entity.getSetupTime(),

                entity.getStandardTime(),

                entity.getStatus(),

                entity.getStatus() == WorkPlanStatus.PLANNED,

                entity.getCreatedAt(),

                masterAuditActorLookup.nameOf(entity.getCreatedById())

        );

    }

}


