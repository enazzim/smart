package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.bom.ItemCompositionCommand;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.bom.ItemCompositionUpdateCommand;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class ItemCompositionApplicationService {

    private final ItemCompositionService itemCompositionService;
    private final ItemRepository itemRepository;

    public ItemCompositionApplicationService(
            ItemCompositionService itemCompositionService,
            ItemRepository itemRepository
    ) {
        this.itemCompositionService = itemCompositionService;
        this.itemRepository = itemRepository;
    }

    @Transactional
    public ItemCompositionView register(ItemCompositionCommand command, String actorUserId) {
        return itemCompositionService.register(command, actorUserId);
    }

    @Transactional
    public ItemCompositionView register(
            String parentItemNum,
            String childItemNum,
            java.math.BigDecimal parentQuantity,
            java.math.BigDecimal childQuantity,
            String actorUserId
    ) {
        ItemView parent = itemRepository.findActiveByItemNo(parentItemNum)
                .orElseThrow(() -> new IllegalArgumentException("모품목을 찾을 수 없습니다: " + parentItemNum));
        ItemView child = itemRepository.findActiveByItemNo(childItemNum)
                .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다: " + childItemNum));
        return itemCompositionService.register(
                new ItemCompositionCommand(parent.id(), child.id(), parentQuantity, childQuantity),
                actorUserId
        );
    }

    @Transactional
    public ItemCompositionView update(long id, ItemCompositionUpdateCommand command, String actorUserId) {
        return itemCompositionService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        itemCompositionService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<ItemCompositionView> listActive(
            Long parentItemId,
            Long childItemId,
            String parentItemNo,
            String childItemNo
    ) {
        return itemCompositionService.listActive(parentItemId, childItemId, parentItemNo, childItemNo);
    }

    @Transactional(readOnly = true)
    public List<ItemCompositionView> listByParentItemNo(String parentItemNum) {
        ItemView parent = itemRepository.findActiveByItemNo(parentItemNum)
                .orElseThrow(() -> new IllegalArgumentException("모품목을 찾을 수 없습니다: " + parentItemNum));
        return itemCompositionService.listByParentItemId(parent.id());
    }

    @Transactional(readOnly = true)
    public ItemCompositionView getActive(long id) {
        return itemCompositionService.getActive(id);
    }

    @Transactional(readOnly = true)
    public com.shindong.smartmanager.application.bom.BomTreeNode explode(String itemNum) {
        return itemCompositionService.explode(itemNum);
    }

    @Transactional(readOnly = true)
    public List<ItemCompositionView> reverse(String childItemNum) {
        return itemCompositionService.reverse(childItemNum);
    }

    @Transactional
    public int copyBom(String sourceItemNum, String targetItemNum, String actorUserId) {
        return itemCompositionService.copyBom(sourceItemNum, targetItemNum, actorUserId);
    }
}
