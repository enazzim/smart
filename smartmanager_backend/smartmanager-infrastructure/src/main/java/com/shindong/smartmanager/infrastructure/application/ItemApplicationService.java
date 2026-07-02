package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.item.ItemCommand;
import com.shindong.smartmanager.application.item.ItemService;
import com.shindong.smartmanager.application.item.ItemUpdateCommand;
import com.shindong.smartmanager.application.item.ItemView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class ItemApplicationService {

    private final ItemService itemService;

    public ItemApplicationService(ItemService itemService) {
        this.itemService = itemService;
    }

    @Transactional
    public ItemView register(ItemCommand command, String actorUserId) {
        return itemService.register(command, actorUserId);
    }

    @Transactional
    public ItemView update(long id, ItemUpdateCommand command, String actorUserId) {
        return itemService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        itemService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<ItemView> listActive(String itemNoQuery, String itemNameQuery) {
        return itemService.listActive(itemNoQuery, itemNameQuery);
    }

    @Transactional(readOnly = true)
    public ItemView getActive(long id) {
        return itemService.getActive(id);
    }

    @Transactional(readOnly = true)
    public ItemView getActiveByItemNo(String itemNo) {
        return itemService.getActiveByItemNo(itemNo);
    }
}
