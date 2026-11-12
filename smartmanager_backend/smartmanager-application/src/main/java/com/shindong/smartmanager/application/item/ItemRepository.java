package com.shindong.smartmanager.application.item;

import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.util.List;
import java.util.Optional;

public interface ItemRepository {

    boolean existsActiveByItemNo(String itemNo);

    long save(ItemCommand command, String actorUserId);

    void update(long id, ItemUpdateCommand command, String actorUserId);

    void updateLotTracked(long id, boolean lotTracked, String actorUserId);

    void softDelete(long id, String actorUserId);

    List<ItemView> findAllActive(String itemNoQuery, String itemNameQuery);

    List<ItemLookupView> lookupActive(String query, List<PropertyClassification> classifications, int limit);

    Optional<ItemView> findActiveById(long id);

    Optional<ItemView> findActiveByItemNo(String itemNo);
}
