package com.shindong.smartmanager.application.bom;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemService;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.pricing.CostType;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.EnumSet;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class ItemCompositionService {

    private static final Set<PropertyClassification> PARENT_CLASSES =
            EnumSet.of(PropertyClassification.제품, PropertyClassification.상품, PropertyClassification.공정품);
    private static final Set<PropertyClassification> CHILD_CLASSES =
            EnumSet.of(PropertyClassification.원자재, PropertyClassification.공정품);

    private final ItemCompositionRepository itemCompositionRepository;
    private final ItemRepository itemRepository;
    private final ItemService itemService;
    private final ProcessRepository processRepository;
    private final UnitPriceRepository unitPriceRepository;
    private final BomHistoryProjector bomHistoryProjector;
    private final DomainEventStore domainEventStore;

    public ItemCompositionService(
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository,
            ItemService itemService,
            ProcessRepository processRepository,
            UnitPriceRepository unitPriceRepository,
            BomHistoryProjector bomHistoryProjector,
            DomainEventStore domainEventStore
    ) {
        this.itemCompositionRepository = itemCompositionRepository;
        this.itemRepository = itemRepository;
        this.itemService = itemService;
        this.processRepository = processRepository;
        this.unitPriceRepository = unitPriceRepository;
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

    public List<ItemCompositionView> listActive(
            Long parentItemId,
            Long childItemId,
            String parentItemNoQuery,
            String childItemNoQuery
    ) {
        return itemCompositionRepository.findAllActive(
                parentItemId,
                childItemId,
                parentItemNoQuery,
                childItemNoQuery
        );
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

    /**
     * 정전개 트리에서 Lot 미추적 품목을 미리보기한다. 트리 밖 모품목이 있으면 otherParentItemNos에 담는다.
     */
    public List<LotTrackedEnablePreviewItem> previewEnableLotTracked(String rootItemNum) {
        BomTreeNode root = explode(rootItemNum);
        Set<Long> treeItemIds = new HashSet<>();
        Map<Long, BomTreeNode> uniqueNodes = new LinkedHashMap<>();
        collectExplosionNodes(root, treeItemIds, uniqueNodes);

        List<LotTrackedEnablePreviewItem> result = new ArrayList<>();
        for (BomTreeNode node : uniqueNodes.values()) {
            List<String> otherParents = itemCompositionRepository.findActiveByChildItemId(node.itemId()).stream()
                    .filter(line -> !treeItemIds.contains(line.parentItemId()))
                    .map(ItemCompositionView::parentItemNo)
                    .distinct()
                    .sorted()
                    .toList();
            result.add(new LotTrackedEnablePreviewItem(
                    node.itemId(),
                    node.itemNum(),
                    node.itemName(),
                    node.lotTracked(),
                    otherParents
            ));
        }
        result.sort(Comparator.comparing(LotTrackedEnablePreviewItem::itemNo));
        return result;
    }

    /**
     * 정전개 트리에 속하고 현재 Lot 미추적인 품목만 Lot 추적을 켠다.
     */
    public int enableLotTracked(String rootItemNum, List<Long> itemIds, String actorUserId) {
        if (itemIds == null || itemIds.isEmpty()) {
            throw new IllegalArgumentException("Lot 추적 설정 대상 품목이 없습니다.");
        }
        BomTreeNode root = explode(rootItemNum);
        Set<Long> treeItemIds = new HashSet<>();
        Map<Long, BomTreeNode> uniqueNodes = new LinkedHashMap<>();
        collectExplosionNodes(root, treeItemIds, uniqueNodes);

        int updated = 0;
        Set<Long> requested = new HashSet<>(itemIds);
        for (Long itemId : requested) {
            BomTreeNode node = uniqueNodes.get(itemId);
            if (node == null) {
                throw new IllegalArgumentException(
                        "정전개 트리에 없는 품목은 일괄 설정할 수 없습니다: " + itemId);
            }
            if (node.lotTracked()) {
                continue;
            }
            itemService.updateLotTracked(itemId, true, actorUserId);
            updated++;
        }
        return updated;
    }

    private static void collectExplosionNodes(
            BomTreeNode node,
            Set<Long> treeItemIds,
            Map<Long, BomTreeNode> uniqueNodes
    ) {
        treeItemIds.add(node.itemId());
        uniqueNodes.putIfAbsent(node.itemId(), node);
        for (BomTreeNode child : node.children()) {
            collectExplosionNodes(child, treeItemIds, uniqueNodes);
        }
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
        LocalDate refDate = LocalDate.now();
        List<BomVendorPriceView> outsourcePrices = resolveOutsourcePrices(item, refDate);
        List<BomVendorPriceView> purchasePrices = resolvePurchasePrices(item, refDate);
        return new BomTreeNode(
                item.id(),
                item.itemNo(),
                item.itemName(),
                item.propertyClassification(),
                level,
                cumulativeQuantity.setScale(4, RoundingMode.HALF_UP),
                item.lotTracked(),
                outsourcePrices,
                purchasePrices,
                children
        );
    }

    private List<BomVendorPriceView> resolveOutsourcePrices(ItemView item, LocalDate refDate) {
        boolean hasOutsourceProcess = processRepository.findAllActiveByItemId(item.id(), ProcessVariant.plan).stream()
                .anyMatch(process -> process.workDistinction() == WorkDistinction.OUTSOURCE);
        if (!hasOutsourceProcess) {
            return List.of();
        }
        return unitPriceRepository.findAllActiveByCostTypeAndItemIds(CostType.OUTSOURCE, Set.of(item.id())).stream()
                .filter(price -> isEffectiveOn(price, refDate))
                .map(this::toOutsourceVendorPrice)
                .sorted(Comparator.comparing(BomVendorPriceView::partnerName).thenComparing(BomVendorPriceView::detail))
                .toList();
    }

    private List<BomVendorPriceView> resolvePurchasePrices(ItemView item, LocalDate refDate) {
        if (item.propertyClassification() != PropertyClassification.원자재) {
            return List.of();
        }
        return unitPriceRepository.findAllActiveByCostTypeAndItemIds(CostType.PURCHASE, Set.of(item.id())).stream()
                .filter(price -> isEffectiveOn(price, refDate))
                .map(this::toPurchaseVendorPrice)
                .sorted(Comparator.comparing(BomVendorPriceView::partnerName))
                .toList();
    }

    private BomVendorPriceView toOutsourceVendorPrice(UnitPriceView price) {
        String begin = price.beginProcessName() != null ? price.beginProcessName() : "";
        String end = price.endProcessName() != null ? price.endProcessName() : "";
        String detail = begin.isBlank() && end.isBlank() ? "" : begin + "~" + end;
        return new BomVendorPriceView(price.companyName(), resolveUnitPriceAmount(price), detail);
    }

    private BomVendorPriceView toPurchaseVendorPrice(UnitPriceView price) {
        String detail = price.orderRate() != null
                ? price.orderRate().stripTrailingZeros().toPlainString() + "%"
                : "";
        return new BomVendorPriceView(price.companyName(), resolveUnitPriceAmount(price), detail);
    }

    private boolean isEffectiveOn(UnitPriceView price, LocalDate refDate) {
        if (price.beginDate().isAfter(refDate)) {
            return false;
        }
        return price.endDate() == null || !price.endDate().isBefore(refDate);
    }

    private BigDecimal resolveUnitPriceAmount(UnitPriceView price) {
        if (price.discountUnitCost() != null) {
            return price.discountUnitCost();
        }
        return price.standardUnitCost() != null ? price.standardUnitCost() : BigDecimal.ZERO;
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
        // 자품수량 0: BOM에 등록만 하고 전개·소요 계산에서 하위를 떨어내지 않을 때 사용
        if (childQuantity == null || childQuantity.signum() < 0) {
            throw new IllegalArgumentException("자품수량은 0 이상이어야 합니다.");
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
