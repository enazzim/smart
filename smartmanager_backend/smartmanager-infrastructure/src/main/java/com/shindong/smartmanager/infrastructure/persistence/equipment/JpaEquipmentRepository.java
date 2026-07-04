package com.shindong.smartmanager.infrastructure.persistence.equipment;

import com.shindong.smartmanager.application.equipment.EquipmentCategoryLookup;
import com.shindong.smartmanager.application.equipment.EquipmentCommand;
import com.shindong.smartmanager.application.equipment.EquipmentRepository;
import com.shindong.smartmanager.application.equipment.EquipmentService;
import com.shindong.smartmanager.application.equipment.EquipmentUpdateCommand;
import com.shindong.smartmanager.application.equipment.EquipmentView;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.SpringDataWorkCenterRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.WorkCenterJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.workstandard.SpringDataWorkStandardRepository;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaEquipmentRepository implements EquipmentRepository {

    private final SpringDataEquipmentRepository equipmentRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final SpringDataWorkCenterRepository workCenterRepository;
    private final SpringDataWorkStandardRepository workStandardRepository;
    private final EquipmentCategoryLookup equipmentCategoryLookup;

    public JpaEquipmentRepository(
            SpringDataEquipmentRepository equipmentRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            SpringDataWorkCenterRepository workCenterRepository,
            SpringDataWorkStandardRepository workStandardRepository,
            EquipmentCategoryLookup equipmentCategoryLookup
    ) {
        this.equipmentRepository = equipmentRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.workCenterRepository = workCenterRepository;
        this.workStandardRepository = workStandardRepository;
        this.equipmentCategoryLookup = equipmentCategoryLookup;
    }

    @Override
    @Transactional
    public long save(EquipmentCommand command, String actorUserId) {
        Instant now = Instant.now();
        EquipmentJpaEntity entity = new EquipmentJpaEntity();
        entity.setEquipmentNum(command.equipmentNum().trim());
        entity.setEquipmentName(command.equipmentName().trim());
        entity.setEquipmentCategoryId(command.equipmentCategoryId());
        entity.setWorkCenterId(command.workCenterId());
        entity.setDesignShot(command.designShot());
        entity.setInitialShot(command.initialShot());
        entity.setWorkShot(0);
        entity.setAccumulatedShot(command.initialShot());
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return equipmentRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, EquipmentUpdateCommand command, String actorUserId) {
        EquipmentJpaEntity entity = equipmentRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("설비를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setEquipmentName(command.equipmentName().trim());
        entity.setEquipmentCategoryId(command.equipmentCategoryId());
        entity.setWorkCenterId(command.workCenterId());
        entity.setDesignShot(command.designShot());
        entity.setInitialShot(command.initialShot());
        entity.setAccumulatedShot(entity.getWorkShot() + command.initialShot());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        equipmentRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        EquipmentJpaEntity entity = equipmentRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("설비를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        equipmentRepository.save(entity);
    }

    @Override
    public List<EquipmentView> findAllActive(String query) {
        String normalized = query == null || query.isBlank() ? null : query.trim();
        return equipmentRepository.searchActive(normalized).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<EquipmentView> findActiveById(long id) {
        return equipmentRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public boolean existsActiveByEquipmentNum(String equipmentNum, Long excludeId) {
        return equipmentRepository.existsActiveByEquipmentNum(equipmentNum.trim(), excludeId);
    }

    @Override
    public boolean isReferencedByActiveWorkStandard(long equipmentId) {
        return workStandardRepository.existsByEquipmentIdAndRecordingState(equipmentId, 1);
    }

    @Override
    public boolean existsByWorkCenterIdAndRecordingState(long workCenterId) {
        return equipmentRepository.existsByWorkCenterIdAndRecordingState(workCenterId, 1);
    }

    private EquipmentView toView(EquipmentJpaEntity entity) {
        EquipmentCategoryLookup.CategoryInfo category = equipmentCategoryLookup
                .findActiveCategory(entity.getEquipmentCategoryId())
                .orElseGet(() -> publicCodeRepository.findById(entity.getEquipmentCategoryId())
                        .map(code -> new EquipmentCategoryLookup.CategoryInfo(
                                code.getId(), nullToEmpty(code.getSmallCode()), nullToEmpty(code.getSmallName())))
                        .orElse(new EquipmentCategoryLookup.CategoryInfo(entity.getEquipmentCategoryId(), "", "")));

        String wcName = null;
        if (entity.getWorkCenterId() != null) {
            wcName = workCenterRepository.findById(entity.getWorkCenterId())
                    .map(WorkCenterJpaEntity::getWcName)
                    .orElse(null);
        }

        return new EquipmentView(
                entity.getId(),
                entity.getEquipmentNum(),
                entity.getEquipmentName(),
                entity.getEquipmentCategoryId(),
                category.smallName(),
                category.smallCode(),
                entity.getWorkCenterId(),
                wcName,
                entity.getDesignShot(),
                entity.getInitialShot(),
                entity.getWorkShot(),
                entity.getAccumulatedShot(),
                EquipmentService.isReplacementDue(entity.getDesignShot(), entity.getAccumulatedShot()),
                entity.getCreatedAt()
        );
    }

    private static String nullToEmpty(String value) {
        return value == null ? "" : value;
    }
}
