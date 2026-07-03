package com.shindong.smartmanager.application.bom;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class ItemCompositionService {

    private static final Set<PropertyClassification> PARENT_CLASSES =
            EnumSet.of(PropertyClassification.제품, PropertyClassification.상품, PropertyClassification.공정품);
    private static final Set<PropertyClassification> CHILD_CLASSES =
            EnumSet.of(PropertyClassification.원자재, PropertyClassification.공정품);

    private final ItemCompositionRepository itemCompositionRepository;
    private final ItemRepository itemRepository;
    private final BomHistoryProjector bomHistoryProjector;
    private final DomainEventStore domainEventStore;

    public ItemCompositionService(
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository,
            BomHistoryProjector bomHistoryProjector,
            DomainEventStore domainEventStore
    ) {
        this.itemCompositionRepository = itemCompositionRepository;
        this.itemRepository = itemRepository;
        this.bomHistoryProjector = bomHistoryProjector;
        this.domainEventStore = domainEventStore;
    }

    public ItemCompositionView register(ItemCompositionCommand command, String actorUserId) {
        validateQuantities(command.parentQuantity(), command.childQuantity());
        if (command.parentItemId() == command.childItemId()) {
            throw new IllegalArgumentException("모품목과 자품목은 같을 수 없습니다.");
        }

        ItemView parent = resolveParentItem(command.parentItemId());
        ItemView child = resolveChildItem(command.childItemId());

        if (itemCompositionRepository.existsActiveUk(command.parentItemId(), command.childItemId(), null)) {
            throw new IllegalArgumentException("동일 모·자품목 조합의 BOM이 이미 존재합니다.");
        }

        assertNoCycle(command.parentItemId(), command.childItemId());

        long id = itemCompositionRepository.save(command, actorUserId);
        bomHistoryProjector.snapshotOnRegister(id, actorUserId);
        appendEvent(EventTypes.BOM_LINE_REGISTERED, id, parent, child, actorUserId);

        return getActive(id);
    }

    public ItemCompositionView update(long id, ItemCompositionUpdateCommand command, String actorUserId) {
        validateQuantities(command.parentQuantity(), command.childQuantity());
        ItemCompositionView existing = getActive(id);

        itemCompositionRepository.updateQuantities(id, command, actorUserId);
        bomHistoryProjector.snapshotOnUpdate(id, actorUserId);

        ItemView parent = itemRepository.findActiveById(existing.parentItemId())
                .orElseThrow(() -> new IllegalArgumentException("모품목을 찾을 수 없습니다."));
        ItemView child = itemRepository.findActiveById(existing.childItemId())
                .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다."));
        appendEvent(EventTypes.BOM_LINE_UPDATED, id, parent, child, actorUserId);

        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        ItemCompositionView existing = getActive(id);
        bomHistoryProjector.snapshotOnDelete(id, actorUserId);
        itemCompositionRepository.softDelete(id, actorUserId);

        ItemView parent = itemRepository.findActiveById(existing.parentItemId())
                .orElseThrow(() -> new IllegalArgumentException("모품목을 찾을 수 없습니다."));
        ItemView child = itemRepository.findActiveById(existing.childItemId())
                .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다."));
        appendEvent(EventTypes.BOM_LINE_DELETED, id, parent, child, actorUserId);
    }

    public List<ItemCompositionView> listActive(String parentItemNoQuery, String childItemNoQuery) {
        return itemCompositionRepository.findAllActive(parentItemNoQuery, childItemNoQuery);
    }

    public List<ItemCompositionView> listByParentItemId(long parentItemId) {
        return itemCompositionRepository.findActiveByParentItemId(parentItemId);
    }

    public ItemCompositionView getActive(long id) {
        return itemCompositionRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("BOM을 찾을 수 없습니다: " + id));
    }

    public BomTreeNode explode(String rootItemNum) {
        ItemView root = itemRepository.findActiveByItemNo(rootItemNum)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + rootItemNum));
        return buildExplosionTree(root, BigDecimal.ONE, 0, new ArrayDeque<>());
    }

    public List<ItemCompositionView> reverse(String childItemNum) {
        ItemView child = itemRepository.findActiveByItemNo(childItemNum)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + childItemNum));
        return itemCompositionRepository.findActiveByChildItemId(child.id());
    }

    public int copyBom(String sourceItemNum, String targetItemNum, String actorUserId) {
        if (sourceItemNum.equals(targetItemNum)) {
            throw new IllegalArgumentException("원본과 대상 모품목이 같을 수 없습니다.");
        }

        ItemView source = resolveParentItem(
                itemRepository.findActiveByItemNo(sourceItemNum)
                        .orElseThrow(() -> new IllegalArgumentException("원본 모품목을 찾을 수 없습니다: " + sourceItemNum))
                        .id()
        );
        ItemView target = resolveParentItem(
                itemRepository.findActiveByItemNo(targetItemNum)
                        .orElseThrow(() -> new IllegalArgumentException("대상 모품목을 찾을 수 없습니다: " + targetItemNum))
                        .id()
        );

        int copied = 0;
        for (ItemCompositionView line : itemCompositionRepository.findActiveByParentItemId(source.id())) {
            if (itemCompositionRepository.existsActiveUk(target.id(), line.childItemId(), null)) {
                continue;
            }
            register(
                    new ItemCompositionCommand(
                            target.id(),
                            line.childItemId(),
                            line.parentQuantity(),
                            line.childQuantity()
                    ),
                    actorUserId
            );
            copied++;
        }
        return copied;
    }

    private BomTreeNode buildExplosionTree(
            ItemView item,
            BigDecimal cumulativeQuantity,
            int level,
            ArrayDeque<Long> path
    ) {
        if (path.contains(item.id())) {
            throw new IllegalArgumentException("BOM 순환 참조가 감지되었습니다: " + item.itemNo());
        }
        path.addLast(item.id());

        List<BomTreeNode> children = new ArrayList<>();
        for (ItemCompositionView line : itemCompositionRepository.findActiveByParentItemId(item.id())) {
            ItemView child = itemRepository.findActiveById(line.childItemId())
                    .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다: " + line.childItemId()));
            BigDecimal ratio = line.childQuantity().divide(
                    line.parentQuantity(),
                    MathContext.DECIMAL64
            );
            BigDecimal childQuantity = cumulativeQuantity
                    .multiply(ratio)
                    .setScale(4, RoundingMode.HALF_UP);
            children.add(buildExplosionTree(child, childQuantity, level + 1, path));
        }

        path.removeLast();
        return new BomTreeNode(
                item.itemNo(),
                item.itemName(),
                item.propertyClassification(),
                level,
                cumulativeQuantity.setScale(4, RoundingMode.HALF_UP),
                children
        );
    }

    private ItemView resolveParentItem(long parentItemId) {
        ItemView parent = itemRepository.findActiveById(parentItemId)
                .orElseThrow(() -> new IllegalArgumentException("모품목을 찾을 수 없습니다: " + parentItemId));
        if (!PARENT_CLASSES.contains(parent.propertyClassification())) {
            throw new IllegalArgumentException("모품목 자산분류가 허용되지 않습니다.");
        }
        return parent;
    }

    private ItemView resolveChildItem(long childItemId) {
        ItemView child = itemRepository.findActiveById(childItemId)
                .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다: " + childItemId));
        if (!CHILD_CLASSES.contains(child.propertyClassification())) {
            throw new IllegalArgumentException("자품목 자산분류가 허용되지 않습니다.");
        }
        return child;
    }

    private void validateQuantities(BigDecimal parentQuantity, BigDecimal childQuantity) {
        if (parentQuantity == null || parentQuantity.signum() <= 0) {
            throw new IllegalArgumentException("모품수량은 0보다 커야 합니다.");
        }
        if (childQuantity == null || childQuantity.signum() <= 0) {
            throw new IllegalArgumentException("자품수량은 0보다 커야 합니다.");
        }
    }

    private void assertNoCycle(long parentItemId, long childItemId) {
        Set<Long> visited = new HashSet<>();
        if (hasPathTo(childItemId, parentItemId, visited)) {
            throw new IllegalArgumentException("순환 참조가 발생합니다.");
        }
    }

    private boolean hasPathTo(long currentItemId, long targetItemId, Set<Long> visited) {
        if (currentItemId == targetItemId) {
            return true;
        }
        if (!visited.add(currentItemId)) {
            return false;
        }
        for (ItemCompositionView line : itemCompositionRepository.findActiveByParentItemId(currentItemId)) {
            if (hasPathTo(line.childItemId(), targetItemId, visited)) {
                return true;
            }
        }
        return false;
    }

    private void appendEvent(String eventType, long id, ItemView parent, ItemView child, String actorUserId) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.BOM_LINE,
                String.valueOf(id),
                actorUserId,
                """
                {"itemCompositionId":%d,"parentItemId":%d,"parentItemNo":"%s","childItemId":%d,"childItemNo":"%s"}
                """.formatted(
                        id,
                        parent.id(),
                        escape(parent.itemNo()),
                        child.id(),
                        escape(child.itemNo())
                ).trim()
        ));
    }

    private String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
