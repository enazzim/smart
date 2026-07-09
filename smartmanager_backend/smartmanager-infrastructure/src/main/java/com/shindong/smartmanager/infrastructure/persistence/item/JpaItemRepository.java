package com.shindong.smartmanager.infrastructure.persistence.item;

import com.shindong.smartmanager.application.item.ItemCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemUpdateCommand;
import com.shindong.smartmanager.application.item.ItemView;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaItemRepository implements ItemRepository {

    private final SpringDataItemRepository itemRepository;

    public JpaItemRepository(SpringDataItemRepository itemRepository) {
        this.itemRepository = itemRepository;
    }

    @Override
    public boolean existsActiveByItemNo(String itemNo) {
        return itemRepository.existsByItemNoAndRecordingState(itemNo, 1);
    }

    @Override
    @Transactional
    public long save(ItemCommand command, String actorUserId) {
        Instant now = Instant.now();
        ItemJpaEntity entity = new ItemJpaEntity();
        applyCommand(entity, command);
        entity.setItemNo(command.itemNo());
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return itemRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, ItemUpdateCommand command, String actorUserId) {
        ItemJpaEntity entity = itemRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + id));
        applyUpdate(entity, command);
        Instant now = Instant.now();
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        itemRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        ItemJpaEntity entity = itemRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        itemRepository.save(entity);
    }

    @Override
    public List<ItemView> findAllActive(String itemNoQuery, String itemNameQuery) {
        String itemNo = itemNoQuery == null ? "" : itemNoQuery.trim();
        String itemName = itemNameQuery == null ? "" : itemNameQuery.trim();
        return itemRepository.searchActive(itemNo, itemName).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<ItemView> findActiveById(long id) {
        return itemRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public Optional<ItemView> findActiveByItemNo(String itemNo) {
        return itemRepository.findByItemNoAndRecordingState(itemNo, 1).map(this::toView);
    }

    private void applyCommand(ItemJpaEntity entity, ItemCommand command) {
        entity.setItemName(command.itemName());
        entity.setPropertyClassification(command.propertyClassification());
        entity.setModelType(command.modelType());
        entity.setUnit(command.unit());
        entity.setStandard(command.standard());
        entity.setStandardUnitCost(command.standardUnitCost());
        entity.setCheckDistinction(command.checkDistinction());
        entity.setLeadTime(command.leadTime());
        entity.setSafetyStockQuantity(command.safetyStockQuantity());
        entity.setOrderIntervalQuantity(command.orderIntervalQuantity());
        entity.setMinOrderQuantity(command.minOrderQuantity());
    }

    private void applyUpdate(ItemJpaEntity entity, ItemUpdateCommand command) {
        entity.setItemName(command.itemName());
        entity.setPropertyClassification(command.propertyClassification());
        entity.setModelType(command.modelType());
        entity.setUnit(command.unit());
        entity.setStandard(command.standard());
        entity.setStandardUnitCost(command.standardUnitCost());
        entity.setCheckDistinction(command.checkDistinction());
        entity.setLeadTime(command.leadTime());
        entity.setSafetyStockQuantity(command.safetyStockQuantity());
        entity.setOrderIntervalQuantity(command.orderIntervalQuantity());
        entity.setMinOrderQuantity(command.minOrderQuantity());
    }

    private ItemView toView(ItemJpaEntity entity) {
        return new ItemView(
                entity.getId(),
                entity.getItemNo(),
                entity.getItemName(),
                entity.getPropertyClassification(),
                entity.getModelType(),
                entity.getUnit(),
                entity.getStandard(),
                entity.getStandardUnitCost(),
                entity.getCheckDistinction(),
                entity.getLeadTime(),
                entity.getSafetyStockQuantity(),
                entity.getOrderIntervalQuantity(),
                entity.getMinOrderQuantity(),
                entity.getCreatedAt()
        );
    }
}
