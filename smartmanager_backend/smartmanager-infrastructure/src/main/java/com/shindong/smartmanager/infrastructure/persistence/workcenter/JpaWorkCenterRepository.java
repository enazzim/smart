package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import com.shindong.smartmanager.application.process.ProcessCodeLookup;
import com.shindong.smartmanager.application.workcenter.WorkCenterCommand;
import com.shindong.smartmanager.application.workcenter.WorkCenterRepository;
import com.shindong.smartmanager.application.workcenter.WorkCenterView;
import com.shindong.smartmanager.infrastructure.persistence.equipment.SpringDataEquipmentRepository;
import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;
import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaWorkCenterRepository implements WorkCenterRepository {

    private final SpringDataWorkCenterRepository workCenterRepository;
    private final SpringDataProcessSequenceRepository processSequenceRepository;
    private final SpringDataEquipmentRepository equipmentRepository;
    private final ProcessCodeLookup processCodeLookup;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaWorkCenterRepository(
            SpringDataWorkCenterRepository workCenterRepository,
            SpringDataProcessSequenceRepository processSequenceRepository,
            SpringDataEquipmentRepository equipmentRepository,
            ProcessCodeLookup processCodeLookup,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.workCenterRepository = workCenterRepository;
        this.processSequenceRepository = processSequenceRepository;
        this.equipmentRepository = equipmentRepository;
        this.processCodeLookup = processCodeLookup;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional
    public long save(WorkCenterCommand command, String actorUserId) {
        Instant now = Instant.now();
        WorkCenterJpaEntity entity = new WorkCenterJpaEntity();
        entity.setWcName(command.wcName().trim());
        entity.setMainProcessCodeId(command.mainProcessCodeId());
        entity.setOperationTime(command.operationTime());
        entity.setRecordingState(1);
        entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setCreatedAt(now);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        return workCenterRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, WorkCenterCommand command, String actorUserId) {
        WorkCenterJpaEntity entity = workCenterRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("작업장을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setWcName(command.wcName().trim());
        entity.setMainProcessCodeId(command.mainProcessCodeId());
        entity.setOperationTime(command.operationTime());
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        workCenterRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        WorkCenterJpaEntity entity = workCenterRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("작업장을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        workCenterRepository.save(entity);
    }

    @Override
    public List<WorkCenterView> findAllActive(String query) {
        String normalized = query == null || query.isBlank() ? null : query.trim();
        return workCenterRepository.searchActive(normalized).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<WorkCenterView> findActiveById(long id) {
        return workCenterRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public boolean existsActiveByWcName(String wcName, Long excludeId) {
        return workCenterRepository.existsActiveByWcName(wcName.trim(), excludeId);
    }

    @Override
    public boolean isReferencedByActiveProcess(long workCenterId) {
        return processSequenceRepository.existsByWorkCenterIdAndRecordingState(workCenterId, 1);
    }

    @Override
    public boolean isReferencedByActiveEquipment(long workCenterId) {
        return equipmentRepository.existsByWorkCenterIdAndRecordingState(workCenterId, 1);
    }

    private WorkCenterView toView(WorkCenterJpaEntity entity) {
        ProcessCodeLookup.ProcessCodeInfo processCode = processCodeLookup
                .findActiveProcessCode(entity.getMainProcessCodeId())
                .orElse(new ProcessCodeLookup.ProcessCodeInfo(
                        entity.getMainProcessCodeId(), "", ""));
        return new WorkCenterView(
                entity.getId(),
                entity.getWcName(),
                entity.getMainProcessCodeId(),
                processCode.smallCode(),
                processCode.smallName(),
                entity.getOperationTime(),
                entity.getCreatedAt()
        );
    }
}
