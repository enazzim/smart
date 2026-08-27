package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.production.MaterialRequirementLineSaveCommand;
import com.shindong.smartmanager.application.production.MaterialRequirementLineView;
import com.shindong.smartmanager.application.production.MrpRepository;
import com.shindong.smartmanager.application.production.MrpRunView;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import java.time.Instant;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaMrpRepository implements MrpRepository {

    private static final int ACTIVE = 1;

    private final SpringDataMrpRunRepository runRepository;
    private final SpringDataMaterialRequirementLineRepository lineRepository;
    private final SpringDataProductionPlanRepository planRepository;
    private final SpringDataItemRepository itemRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaMrpRepository(
            SpringDataMrpRunRepository runRepository,
            SpringDataMaterialRequirementLineRepository lineRepository,
            SpringDataProductionPlanRepository planRepository,
            SpringDataItemRepository itemRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.runRepository = runRepository;
        this.lineRepository = lineRepository;
        this.planRepository = planRepository;
        this.itemRepository = itemRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    public long nextSequenceByRunNoPrefix(String prefix) {
        return runRepository.findByRunNoStartingWithAndRecordingState(prefix, ACTIVE).stream()
                .mapToLong(run -> parseRunNoSequence(run.getRunNo(), prefix))
                .max()
                .orElse(0L) + 1L;
    }

    private long parseRunNoSequence(String runNo, String prefix) {
        if (runNo == null || !runNo.startsWith(prefix)) {
            return 0L;
        }
        try {
            return Long.parseLong(runNo.substring(prefix.length()));
        } catch (NumberFormatException ex) {
            return 0L;
        }
    }

    @Override
    @Transactional
    public long createRun(String runNo, String actorUserId) {
        Instant now = Instant.now();
        MrpRunJpaEntity entity = new MrpRunJpaEntity();
        entity.setRunNo(runNo);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setCreatedAt(now);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        return runRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void saveLines(List<MaterialRequirementLineSaveCommand> lines, String actorUserId) {
        Instant now = Instant.now();
        for (MaterialRequirementLineSaveCommand command : lines) {
            MaterialRequirementLineJpaEntity entity = new MaterialRequirementLineJpaEntity();
            entity.setMrpRunId(command.mrpRunId());
            entity.setProductionPlanId(command.productionPlanId());
            entity.setParentItemId(command.parentItemId());
            entity.setComponentItemId(command.componentItemId());
            entity.setBomUnitQty(command.bomUnitQty());
            entity.setPlannedQty(command.plannedQty());
            entity.setGrossQty(command.grossQty());
            entity.setRecordingState(ACTIVE);
            entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setCreatedAt(now);
            entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setUpdatedAt(now);
            lineRepository.save(entity);
        }
    }

    @Override
    public List<MrpRunView> findAllActiveRuns() {
        Map<Long, Integer> lineCounts = lineRepository.findByRecordingStateOrderByIdDesc(ACTIVE).stream()
                .collect(Collectors.groupingBy(
                        MaterialRequirementLineJpaEntity::getMrpRunId,
                        Collectors.collectingAndThen(Collectors.counting(), Long::intValue)
                ));
        Map<Long, Integer> planCounts = lineRepository.findByRecordingStateOrderByIdDesc(ACTIVE).stream()
                .collect(Collectors.groupingBy(
                        MaterialRequirementLineJpaEntity::getMrpRunId,
                        Collectors.mapping(
                                MaterialRequirementLineJpaEntity::getProductionPlanId,
                                Collectors.collectingAndThen(Collectors.toSet(), Set::size)
                        )
                ));

        return runRepository.findByRecordingStateOrderByIdDesc(ACTIVE).stream()
                .map(run -> new MrpRunView(
                        run.getId(),
                        run.getRunNo(),
                        planCounts.getOrDefault(run.getId(), 0),
                        lineCounts.getOrDefault(run.getId(), 0),
                        run.getCreatedAt(),
                        masterAuditActorLookup.nameOf(run.getCreatedById()),
                        false
                ))
                .toList();
    }

    @Override
    public Optional<MrpRunView> findActiveRunById(long id) {
        return runRepository.findByIdAndRecordingState(id, ACTIVE)
                .map(run -> {
                    List<MaterialRequirementLineJpaEntity> lines =
                            lineRepository.findByMrpRunIdAndRecordingStateOrderByIdAsc(id, ACTIVE);
                    long planCount = lines.stream()
                            .map(MaterialRequirementLineJpaEntity::getProductionPlanId)
                            .distinct()
                            .count();
                    return new MrpRunView(
                            run.getId(),
                            run.getRunNo(),
                            (int) planCount,
                            lines.size(),
                            run.getCreatedAt(),
                            masterAuditActorLookup.nameOf(run.getCreatedById()),
                            false
                    );
                });
    }

    @Override
    public List<MaterialRequirementLineView> findActiveLinesByRunId(long mrpRunId) {
        return mapLines(lineRepository.findByMrpRunIdAndRecordingStateOrderByIdAsc(mrpRunId, ACTIVE));
    }

    @Override
    public List<MaterialRequirementLineView> findAllActiveLines() {
        return mapLines(lineRepository.findByRecordingStateOrderByIdDesc(ACTIVE));
    }

    private List<MaterialRequirementLineView> mapLines(List<MaterialRequirementLineJpaEntity> lines) {
        if (lines.isEmpty()) {
            return List.of();
        }
        Map<Long, MrpRunJpaEntity> runs = loadRunsMap();
        Map<Long, ProductionPlanJpaEntity> plans = loadPlansMap();
        Map<Long, ItemJpaEntity> items = loadItemsMap();

        return lines.stream()
                .map(line -> toLineView(line, runs, plans, items))
                .flatMap(Optional::stream)
                .toList();
    }

    private Optional<MaterialRequirementLineView> toLineView(
            MaterialRequirementLineJpaEntity line,
            Map<Long, MrpRunJpaEntity> runs,
            Map<Long, ProductionPlanJpaEntity> plans,
            Map<Long, ItemJpaEntity> items
    ) {
        MrpRunJpaEntity run = runs.get(line.getMrpRunId());
        ProductionPlanJpaEntity plan = plans.get(line.getProductionPlanId());
        ItemJpaEntity parentItem = items.get(line.getParentItemId());
        ItemJpaEntity componentItem = items.get(line.getComponentItemId());
        if (run == null || plan == null || parentItem == null || componentItem == null) {
            return Optional.empty();
        }
        return Optional.of(new MaterialRequirementLineView(
                line.getId(),
                run.getId(),
                run.getRunNo(),
                plan.getId(),
                plan.getPlanNo(),
                parentItem.getId(),
                parentItem.getItemNo(),
                parentItem.getItemName(),
                componentItem.getId(),
                componentItem.getItemNo(),
                componentItem.getItemName(),
                componentItem.getPropertyClassification(),
                componentItem.getUnit(),
                line.getBomUnitQty(),
                line.getPlannedQty(),
                line.getGrossQty(),
                line.getCreatedAt(),
                false
        ));
    }

    @Override
    public Optional<MaterialRequirementLineView> findActiveLineById(long id) {
        return lineRepository.findByIdAndRecordingState(id, ACTIVE)
                .flatMap(line -> toLineView(
                        line,
                        loadRunsMap(),
                        loadPlansMap(),
                        loadItemsMap()
                ));
    }

    @Override
    public List<MaterialRequirementLineView> findActiveLinesByProductionPlanId(long productionPlanId) {
        return mapLines(lineRepository.findByProductionPlanIdAndRecordingStateOrderByIdAsc(productionPlanId, ACTIVE));
    }

    @Override
    public List<Long> findDistinctPlanIdsByRunId(long mrpRunId) {
        return lineRepository.findByMrpRunIdAndRecordingStateOrderByIdAsc(mrpRunId, ACTIVE).stream()
                .map(MaterialRequirementLineJpaEntity::getProductionPlanId)
                .distinct()
                .toList();
    }

    @Override
    public long countActiveLinesByProductionPlanId(long productionPlanId) {
        return lineRepository.countByProductionPlanIdAndRecordingState(productionPlanId, ACTIVE);
    }

    @Override
    public long countActiveLinesByRunId(long mrpRunId) {
        return lineRepository.countByMrpRunIdAndRecordingState(mrpRunId, ACTIVE);
    }

    @Override
    @Transactional
    public void deleteLinesByRunId(long mrpRunId) {
        lineRepository.findByMrpRunIdAndRecordingStateOrderByIdAsc(mrpRunId, ACTIVE)
                .forEach(lineRepository::delete);
    }

    @Override
    @Transactional
    public void deleteLinesByProductionPlanId(long productionPlanId) {
        lineRepository.findByProductionPlanIdAndRecordingStateOrderByIdAsc(productionPlanId, ACTIVE)
                .forEach(lineRepository::delete);
    }

    @Override
    @Transactional
    public void deleteLineById(long lineId) {
        MaterialRequirementLineJpaEntity entity = lineRepository.findByIdAndRecordingState(lineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("자재소요 라인을 찾을 수 없습니다: " + lineId));
        lineRepository.delete(entity);
    }

    @Override
    @Transactional
    public void deleteRunById(long mrpRunId) {
        MrpRunJpaEntity entity = runRepository.findByIdAndRecordingState(mrpRunId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("자재소요 산출 이력을 찾을 수 없습니다: " + mrpRunId));
        runRepository.delete(entity);
    }

    @Override
    @Transactional
    public boolean deleteRunIfEmpty(long mrpRunId) {
        if (countActiveLinesByRunId(mrpRunId) > 0) {
            return false;
        }
        return runRepository.findByIdAndRecordingState(mrpRunId, ACTIVE)
                .map(run -> {
                    runRepository.delete(run);
                    return true;
                })
                .orElse(false);
    }

    private Map<Long, MrpRunJpaEntity> loadRunsMap() {
        return runRepository.findByRecordingStateOrderByIdDesc(ACTIVE).stream()
                .collect(Collectors.toMap(MrpRunJpaEntity::getId, Function.identity()));
    }

    private Map<Long, ProductionPlanJpaEntity> loadPlansMap() {
        return planRepository.findByRecordingStateOrderByIdDesc(ACTIVE).stream()
                .collect(Collectors.toMap(ProductionPlanJpaEntity::getId, Function.identity()));
    }

    private Map<Long, ItemJpaEntity> loadItemsMap() {
        return itemRepository.findAll().stream()
                .filter(item -> item.getRecordingState() == ACTIVE)
                .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));
    }
}
