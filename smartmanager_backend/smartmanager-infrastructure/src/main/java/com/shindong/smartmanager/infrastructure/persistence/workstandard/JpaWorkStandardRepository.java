package com.shindong.smartmanager.infrastructure.persistence.workstandard;

import com.shindong.smartmanager.application.workstandard.WorkStandardCommand;
import com.shindong.smartmanager.application.workstandard.WorkStandardRepository;
import com.shindong.smartmanager.application.workstandard.WorkStandardUpdateCommand;
import com.shindong.smartmanager.application.workstandard.WorkStandardView;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.process.ProcessSequenceJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.SpringDataWorkCenterRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.WorkCenterJpaEntity;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaWorkStandardRepository implements WorkStandardRepository {

    private final SpringDataWorkStandardRepository workStandardRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataProcessSequenceRepository processSequenceRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final SpringDataWorkCenterRepository workCenterRepository;

    public JpaWorkStandardRepository(
            SpringDataWorkStandardRepository workStandardRepository,
            SpringDataItemRepository itemRepository,
            SpringDataProcessSequenceRepository processSequenceRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            SpringDataWorkCenterRepository workCenterRepository
    ) {
        this.workStandardRepository = workStandardRepository;
        this.itemRepository = itemRepository;
        this.processSequenceRepository = processSequenceRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.workCenterRepository = workCenterRepository;
    }

    @Override
    @Transactional
    public long save(WorkStandardCommand command, String actorUserId) {
        Instant now = Instant.now();
        WorkStandardJpaEntity entity = new WorkStandardJpaEntity();
        applyCommand(entity, command);
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return workStandardRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, WorkStandardUpdateCommand command, String actorUserId) {
        WorkStandardJpaEntity entity = workStandardRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("작업표준을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setWorkCenterId(command.workCenterId());
        entity.setEquipmentId(command.equipmentId());
        entity.setPriorityOrder(command.priorityOrder());
        entity.setMainWorkerUserId(command.mainWorkerId());
        entity.setToolName(normalizeToolName(command.toolName()));
        entity.setSetupTime(command.setupTime());
        entity.setStandardTime(command.standardTime());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        workStandardRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        WorkStandardJpaEntity entity = workStandardRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("작업표준을 찾을 수 없습니다: " + id));
        markDeleted(entity, actorUserId);
        workStandardRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDeleteByProcessSequenceId(long processSequenceId, String actorUserId) {
        Instant now = Instant.now();
        for (WorkStandardJpaEntity entity : workStandardRepository.findByProcessSequenceIdAndRecordingState(
                processSequenceId, 1)) {
            entity.setRecordingState(0);
            entity.setUpdatedBy(actorUserId);
            entity.setUpdatedById(actorUserId);
            entity.setUpdatedAt(now);
            workStandardRepository.save(entity);
        }
    }

    @Override
    public List<WorkStandardView> findAllActive(String itemNumQuery) {
        String normalized = itemNumQuery == null || itemNumQuery.isBlank() ? null : itemNumQuery.trim();
        return workStandardRepository.searchActive(normalized).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public List<WorkStandardView> findActiveByItemId(long itemId) {
        return workStandardRepository
                .findByItemIdAndRecordingStateOrderByProcessSequenceIdAscPriorityOrderAsc(itemId, 1)
                .stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<WorkStandardView> findActiveById(long id) {
        return workStandardRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public boolean existsActiveUk(long itemId, long processSequenceId, int priorityOrder, Long excludeId) {
        return workStandardRepository.existsActiveUk(itemId, processSequenceId, priorityOrder, excludeId);
    }

    private void applyCommand(WorkStandardJpaEntity entity, WorkStandardCommand command) {
        entity.setItemId(command.itemId());
        entity.setProcessSequenceId(command.processSequenceId());
        entity.setWorkCenterId(command.workCenterId());
        entity.setEquipmentId(command.equipmentId());
        entity.setPriorityOrder(command.priorityOrder());
        entity.setMainWorkerUserId(command.mainWorkerId());
        entity.setToolName(normalizeToolName(command.toolName()));
        entity.setSetupTime(command.setupTime());
        entity.setStandardTime(command.standardTime());
    }

    private void markDeleted(WorkStandardJpaEntity entity, String actorUserId) {
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
    }

    private static String normalizeToolName(String toolName) {
        if (toolName == null || toolName.isBlank()) {
            return null;
        }
        return toolName.trim();
    }

    private WorkStandardView toView(WorkStandardJpaEntity entity) {
        ItemJpaEntity item = itemRepository.findById(entity.getItemId())
                .orElseThrow(() -> new IllegalStateException("품목을 찾을 수 없습니다: " + entity.getItemId()));
        ProcessSequenceJpaEntity process = processSequenceRepository.findById(entity.getProcessSequenceId())
                .orElseThrow(() -> new IllegalStateException("공정을 찾을 수 없습니다: " + entity.getProcessSequenceId()));
        PublicCodeJpaEntity processCode = publicCodeRepository.findById(process.getPublicCodeId())
                .orElseThrow(() -> new IllegalStateException("공정코드를 찾을 수 없습니다: " + process.getPublicCodeId()));
        WorkCenterJpaEntity workCenter = workCenterRepository.findById(entity.getWorkCenterId())
                .orElseThrow(() -> new IllegalStateException("작업장을 찾을 수 없습니다: " + entity.getWorkCenterId()));

        return new WorkStandardView(
                entity.getId(),
                entity.getItemId(),
                item.getItemNo(),
                item.getItemName(),
                entity.getProcessSequenceId(),
                process.getProcessSequenceNum(),
                process.getPublicCodeId(),
                processCode.getSmallCode(),
                processCode.getSmallName(),
                process.getWorkDistinction(),
                entity.getWorkCenterId(),
                workCenter.getWcName(),
                entity.getEquipmentId(),
                entity.getPriorityOrder(),
                entity.getMainWorkerUserId(),
                entity.getToolName(),
                entity.getSetupTime(),
                entity.getStandardTime(),
                entity.getCreatedAt()
        );
    }
}
