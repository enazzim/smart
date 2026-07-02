package com.shindong.smartmanager.infrastructure.persistence.process;

import com.shindong.smartmanager.application.process.ProcessCommand;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessUpdateCommand;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.SpringDataWorkCenterRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.WorkCenterJpaEntity;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaProcessRepository implements ProcessRepository {

    private final SpringDataProcessSequenceRepository processRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final SpringDataWorkCenterRepository workCenterRepository;

    public JpaProcessRepository(
            SpringDataProcessSequenceRepository processRepository,
            SpringDataItemRepository itemRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            SpringDataWorkCenterRepository workCenterRepository
    ) {
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.workCenterRepository = workCenterRepository;
    }

    @Override
    @Transactional
    public long save(ProcessCommand command, ProcessVariant variant, String actorUserId) {
        Instant now = Instant.now();
        ProcessSequenceJpaEntity entity = new ProcessSequenceJpaEntity();
        applyCommand(entity, command);
        entity.setVariant(variant);
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return processRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, ProcessUpdateCommand command, String actorUserId) {
        ProcessSequenceJpaEntity entity = processRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + id));
        applyUpdate(entity, command);
        Instant now = Instant.now();
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        processRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        ProcessSequenceJpaEntity entity = processRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        processRepository.save(entity);
    }

    @Override
    public Optional<ProcessView> findActiveById(long id) {
        return processRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public List<ProcessView> findAllActiveByItemId(long itemId, ProcessVariant variant) {
        return processRepository
                .findByItemIdAndVariantAndRecordingStateOrderByProcessSequenceNumAsc(itemId, variant, 1)
                .stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public List<ProcessView> findAllActive(ProcessVariant variant) {
        return processRepository
                .findByVariantAndRecordingStateOrderByItemIdAscProcessSequenceNumAsc(variant, 1)
                .stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public boolean existsActiveDuplicate(
            long itemId,
            long processCodeId,
            short processSequenceNum,
            Long excludeId
    ) {
        return processRepository.existsActiveDuplicate(itemId, processCodeId, processSequenceNum, excludeId);
    }

    private void applyCommand(ProcessSequenceJpaEntity entity, ProcessCommand command) {
        entity.setItemId(command.itemId());
        entity.setProcessSequenceNum(command.processSequenceNum());
        entity.setPublicCodeId(command.processCodeId());
        entity.setWorkDistinction(command.workDistinction());
        entity.setWorkCenterId(command.workCenterId());
        entity.setOutsideOrderRate(command.outsideOrderRate());
        entity.setProgressRate(command.progressRate());
    }

    private void applyUpdate(ProcessSequenceJpaEntity entity, ProcessUpdateCommand command) {
        entity.setItemId(command.itemId());
        entity.setProcessSequenceNum(command.processSequenceNum());
        entity.setPublicCodeId(command.processCodeId());
        entity.setWorkDistinction(command.workDistinction());
        entity.setWorkCenterId(command.workCenterId());
        entity.setOutsideOrderRate(command.outsideOrderRate());
        entity.setProgressRate(command.progressRate());
    }

    private ProcessView toView(ProcessSequenceJpaEntity entity) {
        ItemJpaEntity item = itemRepository.findById(entity.getItemId())
                .orElseThrow(() -> new IllegalStateException("품목을 찾을 수 없습니다: " + entity.getItemId()));
        PublicCodeJpaEntity processCode = publicCodeRepository.findById(entity.getPublicCodeId())
                .orElseThrow(() -> new IllegalStateException("공정코드를 찾을 수 없습니다: " + entity.getPublicCodeId()));

        String wcName = null;
        if (entity.getWorkCenterId() != null) {
            wcName = workCenterRepository.findById(entity.getWorkCenterId())
                    .map(WorkCenterJpaEntity::getWcName)
                    .orElse(null);
        }

        return new ProcessView(
                entity.getId(),
                entity.getItemId(),
                item.getItemNo(),
                item.getItemName(),
                entity.getProcessSequenceNum(),
                entity.getPublicCodeId(),
                processCode.getSmallCode(),
                processCode.getSmallName(),
                entity.getWorkDistinction(),
                entity.getWorkCenterId(),
                wcName,
                entity.getOutsideOrderRate(),
                entity.getProgressRate(),
                entity.getCreatedAt()
        );
    }
}
