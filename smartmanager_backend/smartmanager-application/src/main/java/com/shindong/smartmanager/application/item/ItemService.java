package com.shindong.smartmanager.application.item;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.util.List;

public class ItemService {

    private final ItemRepository itemRepository;
    private final DomainEventStore domainEventStore;

    public ItemService(ItemRepository itemRepository, DomainEventStore domainEventStore) {
        this.itemRepository = itemRepository;
        this.domainEventStore = domainEventStore;
    }

    public ItemView register(ItemCommand command, String actorUserId) {
        validateCommand(command.itemNo(), command.itemName(), command.propertyClassification(), command.unit());
        if (itemRepository.existsActiveByItemNo(command.itemNo())) {
            throw new IllegalArgumentException("이미 등록된 품목번호입니다: " + command.itemNo());
        }

        long itemId = itemRepository.save(command, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.ITEM_REGISTERED,
                1,
                AggregateTypes.ITEM,
                String.valueOf(itemId),
                actorUserId,
                buildPayload(itemId, command.itemNo(), command.itemName(), command.propertyClassification().name())
        ));

        return getActive(itemId);
    }

    public ItemView update(long id, ItemUpdateCommand command, String actorUserId) {
        ItemView existing = getActive(id);
        validateUpdate(command.itemName(), command.propertyClassification(), command.unit());

        itemRepository.update(id, command, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.ITEM_UPDATED,
                1,
                AggregateTypes.ITEM,
                String.valueOf(id),
                actorUserId,
                buildPayload(id, existing.itemNo(), command.itemName(), command.propertyClassification().name())
        ));

        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        ItemView existing = getActive(id);
        itemRepository.softDelete(id, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.ITEM_DELETED,
                1,
                AggregateTypes.ITEM,
                String.valueOf(id),
                actorUserId,
                """
                {"itemId":%d,"itemNo":"%s","itemName":"%s"}
                """.formatted(id, escape(existing.itemNo()), escape(existing.itemName())).trim()
        ));
    }

    public List<ItemView> listActive(String itemNoQuery, String itemNameQuery) {
        return itemRepository.findAllActive(itemNoQuery, itemNameQuery);
    }

    public ItemView getActive(long id) {
        return itemRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + id));
    }

    public ItemView getActiveByItemNo(String itemNo) {
        return itemRepository.findActiveByItemNo(itemNo)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemNo));
    }

    private void validateCommand(
            String itemNo,
            String itemName,
            com.shindong.smartmanager.domain.item.PropertyClassification propertyClassification,
            String unit
    ) {
        if (itemNo == null || itemNo.isBlank()) {
            throw new IllegalArgumentException("품목번호는 필수입니다.");
        }
        validateUpdate(itemName, propertyClassification, unit);
    }

    private void validateUpdate(
            String itemName,
            com.shindong.smartmanager.domain.item.PropertyClassification propertyClassification,
            String unit
    ) {
        if (itemName == null || itemName.isBlank()) {
            throw new IllegalArgumentException("품목명은 필수입니다.");
        }
        if (propertyClassification == null) {
            throw new IllegalArgumentException("자산분류는 필수입니다.");
        }
        if (unit == null || unit.isBlank()) {
            throw new IllegalArgumentException("단위는 필수입니다.");
        }
    }

    private String buildPayload(long itemId, String itemNo, String itemName, String propertyClassification) {
        return """
                {"itemId":%d,"itemNo":"%s","itemName":"%s","propertyClassification":"%s"}
                """.formatted(itemId, escape(itemNo), escape(itemName), propertyClassification).trim();
    }

    private String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
