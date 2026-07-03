package com.shindong.smartmanager.infrastructure.persistence.bom;

import com.shindong.smartmanager.application.bom.ItemCompositionCommand;
import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionUpdateCommand;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaItemCompositionRepository implements ItemCompositionRepository {

    private final SpringDataItemCompositionRepository compositionRepository;
    private final SpringDataBomChangeLogRepository changeLogRepository;
    private final SpringDataItemRepository itemRepository;

    public JpaItemCompositionRepository(
            SpringDataItemCompositionRepository compositionRepository,
            SpringDataBomChangeLogRepository changeLogRepository,
            SpringDataItemRepository itemRepository
    ) {
        this.compositionRepository = compositionRepository;
        this.changeLogRepository = changeLogRepository;
        this.itemRepository = itemRepository;
    }

    @Override
    @Transactional
    public long save(ItemCompositionCommand command, String actorUserId) {
        Instant now = Instant.now();
        ItemJpaEntity child = itemRepository.findById(command.childItemId())
                .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다."));

        ItemCompositionJpaEntity entity = new ItemCompositionJpaEntity();
        entity.setParentItemId(command.parentItemId());
        entity.setChildItemId(command.childItemId());
        entity.setNeedQuantityDenominator(command.parentQuantity());
        entity.setNeedQuantityNumerator(command.childQuantity());
        entity.setProcessManagement(0);
        entity.setBomUnit(child.getUnit());
        entity.setBeginDate(LocalDate.now());
        entity.setEndDate(null);
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return compositionRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void updateQuantities(long id, ItemCompositionUpdateCommand command, String actorUserId) {
        ItemCompositionJpaEntity entity = compositionRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("BOM을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setNeedQuantityDenominator(command.parentQuantity());
        entity.setNeedQuantityNumerator(command.childQuantity());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        compositionRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        ItemCompositionJpaEntity entity = compositionRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("BOM을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        compositionRepository.save(entity);
    }

    @Override
    public Optional<ItemCompositionView> findActiveById(long id) {
        return compositionRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public List<ItemCompositionView> findAllActive(String parentItemNoQuery, String childItemNoQuery) {
        return compositionRepository.findAllActiveFiltered(parentItemNoQuery, childItemNoQuery)
                .stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public List<ItemCompositionView> findActiveByParentItemId(long parentItemId) {
        return compositionRepository
                .findByParentItemIdAndRecordingStateOrderByChildItemIdAsc(parentItemId, 1)
                .stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public List<ItemCompositionView> findActiveByChildItemId(long childItemId) {
        return compositionRepository
                .findByChildItemIdAndRecordingStateOrderByParentItemIdAsc(childItemId, 1)
                .stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public boolean existsActiveUk(long parentItemId, long childItemId, Long excludeId) {
        return compositionRepository.existsActiveUk(parentItemId, childItemId, excludeId);
    }

    @Override
    @Transactional
    public void appendChangeLog(long itemCompositionId, String changeReason, String actorUserId) {
        ItemCompositionJpaEntity entity = compositionRepository.findById(itemCompositionId)
                .orElseThrow(() -> new IllegalArgumentException("BOM을 찾을 수 없습니다: " + itemCompositionId));

        BomChangeLogJpaEntity log = new BomChangeLogJpaEntity();
        log.setItemCompositionId(entity.getId());
        log.setParentItemId(entity.getParentItemId());
        log.setChildItemId(entity.getChildItemId());
        log.setNeedQuantityDenominator(entity.getNeedQuantityDenominator());
        log.setNeedQuantityNumerator(entity.getNeedQuantityNumerator());
        log.setChangeReason(changeReason);
        log.setChangedBy(actorUserId);
        log.setChangedById(actorUserId);
        log.setChangedAt(Instant.now());
        changeLogRepository.save(log);
    }

    private ItemCompositionView toView(ItemCompositionJpaEntity entity) {
        ItemJpaEntity parent = itemRepository.findById(entity.getParentItemId())
                .orElseThrow(() -> new IllegalStateException("모품목을 찾을 수 없습니다: " + entity.getParentItemId()));
        ItemJpaEntity child = itemRepository.findById(entity.getChildItemId())
                .orElseThrow(() -> new IllegalStateException("자품목을 찾을 수 없습니다: " + entity.getChildItemId()));

        return new ItemCompositionView(
                entity.getId(),
                entity.getParentItemId(),
                parent.getItemNo(),
                parent.getItemName(),
                entity.getChildItemId(),
                child.getItemNo(),
                child.getItemName(),
                entity.getNeedQuantityDenominator(),
                entity.getNeedQuantityNumerator(),
                entity.getBeginDate(),
                entity.getEndDate(),
                entity.getCreatedAt()
        );
    }
}
