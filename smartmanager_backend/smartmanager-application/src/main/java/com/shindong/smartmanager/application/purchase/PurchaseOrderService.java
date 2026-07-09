package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.production.MaterialRequirementLineView;
import com.shindong.smartmanager.application.production.MrpRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.pricing.CostType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderSourceType;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;

public class PurchaseOrderService {

    private static final Set<PropertyClassification> ALLOWED_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.원자재, PropertyClassification.상품);

    private static final BigDecimal HUNDRED = new BigDecimal("100");

    /** 기업정보(tenant-profile) API 연동 전 발주서 발신 기본값 */
    private static final String DEFAULT_ISSUER_COMPANY_NAME = "유한책임회사 신동공업";
    private static final String DEFAULT_ISSUER_ADDRESS = "경상남도 사천시 곤양면 곤북로 82";
    private static final String DEFAULT_ISSUER_PHONE = "055) 855-0145";
    private static final String DEFAULT_ISSUER_FAX = "855-0143";

    private final PurchaseOrderRepository purchaseOrderRepository;
    private final CompanyRepository companyRepository;
    private final ItemRepository itemRepository;
    private final MrpRepository mrpRepository;
    private final UnitPriceRepository unitPriceRepository;
    private final DomainEventStore domainEventStore;

    public PurchaseOrderService(
            PurchaseOrderRepository purchaseOrderRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            MrpRepository mrpRepository,
            UnitPriceRepository unitPriceRepository,
            DomainEventStore domainEventStore
    ) {
        this.purchaseOrderRepository = purchaseOrderRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.mrpRepository = mrpRepository;
        this.unitPriceRepository = unitPriceRepository;
        this.domainEventStore = domainEventStore;
    }

    public List<PurchaseOrderView> list() {
        return purchaseOrderRepository.findAllActive();
    }

    public List<PurchaseOrderView> list(PurchaseOrderListCriteria criteria) {
        return purchaseOrderRepository.findAllActive(criteria);
    }

    public PurchaseOrderView get(long id) {
        return purchaseOrderRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("구매발주를 찾을 수 없습니다: " + id));
    }

    public PurchaseOrderPrintView getPrintView(long id) {
        PurchaseOrderView order = get(id);
        if (order.status() == PurchaseOrderStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 발주는 출력할 수 없습니다.");
        }
        return buildPrintView(List.of(order));
    }

    public List<PurchaseOrderPrintView> getBatchPrintViews(List<Long> orderIds) {
        if (orderIds == null || orderIds.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주를 1건 이상 선택해 주세요.");
        }
        List<PurchaseOrderView> orders = new ArrayList<>();
        for (Long orderId : orderIds) {
            if (orderId == null) {
                continue;
            }
            PurchaseOrderView order = get(orderId);
            if (order.status() == PurchaseOrderStatus.CANCELLED) {
                throw new IllegalArgumentException("취소된 발주는 출력할 수 없습니다: " + order.orderNo());
            }
            orders.add(order);
        }
        if (orders.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주를 1건 이상 선택해 주세요.");
        }
        Map<Long, List<PurchaseOrderView>> byPartner = orders.stream()
                .collect(Collectors.groupingBy(PurchaseOrderView::partnerId));
        List<PurchaseOrderPrintView> views = new ArrayList<>();
        for (List<PurchaseOrderView> partnerOrders : byPartner.values()) {
            views.add(buildPrintView(partnerOrders));
        }
        views.sort((left, right) -> left.partnerName().compareToIgnoreCase(right.partnerName()));
        return views;
    }

    private PurchaseOrderPrintView buildPrintView(List<PurchaseOrderView> orders) {
        if (orders.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주가 없습니다.");
        }
        PurchaseOrderView first = orders.get(0);
        String orderNos = orders.stream()
                .map(PurchaseOrderView::orderNo)
                .distinct()
                .collect(Collectors.joining(", "));
        LocalDate orderDate = orders.stream()
                .map(PurchaseOrderView::orderDate)
                .min(LocalDate::compareTo)
                .orElse(first.orderDate());
        String orderManagerName = orders.stream()
                .map(PurchaseOrderView::createdBy)
                .filter(name -> name != null && !name.isBlank())
                .findFirst()
                .orElse("—");

        List<PurchaseOrderPrintLineView> lines = new ArrayList<>();
        BigDecimal totalAmount = BigDecimal.ZERO;
        int lineNo = 1;
        for (PurchaseOrderView order : orders) {
            for (PurchaseOrderLineView line : order.lines()) {
                ItemView item = itemRepository.findActiveById(line.itemId()).orElse(null);
                BigDecimal amount = line.amount() != null ? line.amount() : BigDecimal.ZERO;
                lines.add(new PurchaseOrderPrintLineView(
                        lineNo++,
                        line.itemNo(),
                        line.itemName(),
                        item != null ? item.standard() : null,
                        item != null ? item.unit() : "",
                        line.orderQty(),
                        line.unitPrice(),
                        amount,
                        line.requestedDeliveryDate()
                ));
                totalAmount = totalAmount.add(amount);
            }
        }
        return new PurchaseOrderPrintView(
                orderNos,
                orderDate,
                first.partnerName(),
                first.partnerBusinessRegNo(),
                DEFAULT_ISSUER_COMPANY_NAME,
                DEFAULT_ISSUER_ADDRESS,
                DEFAULT_ISSUER_PHONE,
                DEFAULT_ISSUER_FAX,
                orderManagerName,
                lines,
                totalAmount
        );
    }

    public List<MrpPurchaseCandidateView> listMrpCandidates() {
        return listMrpCandidates(LocalDate.now());
    }

    public List<MrpPurchaseCandidateView> listMrpCandidates(LocalDate refDate) {
        LocalDate effectiveDate = refDate != null ? refDate : LocalDate.now();
        List<MaterialRequirementLineView> lines = mrpRepository.findAllActiveLines();
        if (lines.isEmpty()) {
            return List.of();
        }
        Set<Long> lineIds = lines.stream().map(MaterialRequirementLineView::id).collect(Collectors.toSet());
        Map<Long, BigDecimal> orderedQtyByLineId = purchaseOrderRepository.sumOrderedQtyByRequirementLineIds(lineIds);
        Set<Long> itemIds = lines.stream()
                .filter(line -> hasRemainingQty(line, orderedQtyByLineId))
                .map(MaterialRequirementLineView::componentItemId)
                .collect(Collectors.toSet());
        Map<Long, List<UnitPriceView>> pricesByItemId = resolveEffectivePurchasePricesByItem(itemIds, effectiveDate);
        Map<Long, Integer> leadTimeByItemId = loadLeadTimeByItemId(itemIds);

        List<MrpPurchaseCandidateView> candidates = new ArrayList<>();
        for (MaterialRequirementLineView line : lines) {
            BigDecimal orderedQty = orderedQtyByLineId.getOrDefault(line.id(), BigDecimal.ZERO);
            BigDecimal remainingQty = line.grossQty().subtract(orderedQty);
            if (remainingQty.compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            Integer leadTimeDays = leadTimeByItemId.get(line.componentItemId());
            candidates.add(toMrpPurchaseCandidateView(
                    line,
                    remainingQty,
                    orderedQty,
                    pricesByItemId.getOrDefault(line.componentItemId(), List.of()),
                    leadTimeDays,
                    effectiveDate
            ));
        }
        return candidates;
    }

    private boolean hasRemainingQty(MaterialRequirementLineView line, Map<Long, BigDecimal> orderedQtyByLineId) {
        BigDecimal orderedQty = orderedQtyByLineId.getOrDefault(line.id(), BigDecimal.ZERO);
        return line.grossQty().subtract(orderedQty).compareTo(BigDecimal.ZERO) > 0;
    }

    private Map<Long, Integer> loadLeadTimeByItemId(Set<Long> itemIds) {
        Map<Long, Integer> leadTimes = new HashMap<>();
        for (Long itemId : itemIds) {
            itemRepository.findActiveById(itemId).ifPresent(item -> leadTimes.put(itemId, item.leadTime()));
        }
        return leadTimes;
    }

    private MrpPurchaseCandidateView toMrpPurchaseCandidateView(
            MaterialRequirementLineView line,
            BigDecimal remainingQty,
            BigDecimal orderedQty,
            List<UnitPriceView> purchasePrices,
            Integer leadTimeDays,
            LocalDate orderDate
    ) {
        List<UnitPriceView> allocatable = purchasePrices.stream()
                .filter(price -> price.orderRate() != null && price.orderRate().compareTo(BigDecimal.ZERO) > 0)
                .sorted((left, right) -> left.companyName().compareToIgnoreCase(right.companyName()))
                .toList();
        BigDecimal orderRateTotal = allocatable.stream()
                .map(UnitPriceView::orderRate)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
        List<MrpPurchaseCandidateVendorView> vendors = buildVendorSplits(
                remainingQty,
                allocatable,
                leadTimeDays,
                orderDate
        );
        boolean orderable = remainingQty.compareTo(BigDecimal.ZERO) > 0
                && !vendors.isEmpty()
                && orderRateTotal.compareTo(HUNDRED) == 0;
        String orderableMessage = resolveOrderableMessage(vendors, orderRateTotal, remainingQty);
        return new MrpPurchaseCandidateView(
                line.id(),
                line.mrpRunId(),
                line.runNo(),
                line.productionPlanId(),
                line.planNo(),
                line.componentItemId(),
                line.componentItemNo(),
                line.componentItemName(),
                line.componentPropertyClassification().name(),
                line.unit(),
                line.grossQty(),
                orderedQty,
                remainingQty,
                line.createdAt(),
                orderRateTotal,
                orderable,
                orderableMessage,
                vendors
        );
    }

    private String resolveOrderableMessage(
            List<MrpPurchaseCandidateVendorView> vendors,
            BigDecimal orderRateTotal,
            BigDecimal remainingQty
    ) {
        if (remainingQty.compareTo(BigDecimal.ZERO) <= 0) {
            return "발주가 완료된 자재소요입니다.";
        }
        if (vendors.isEmpty()) {
            return "유효한 구매단가·거래처가 없습니다.";
        }
        if (orderRateTotal.compareTo(HUNDRED) != 0) {
            return "발주비율 합계가 100%가 아닙니다. (현재 "
                    + orderRateTotal.stripTrailingZeros().toPlainString() + "%)";
        }
        return null;
    }

    private List<MrpPurchaseCandidateVendorView> buildVendorSplits(
            BigDecimal remainingQty,
            List<UnitPriceView> allocatable,
            Integer leadTimeDays,
            LocalDate orderDate
    ) {
        if (allocatable.isEmpty()) {
            return List.of();
        }
        LocalDate requestedDeliveryDate = resolveRequestedDeliveryDate(orderDate, leadTimeDays);
        BigDecimal allocatedQty = BigDecimal.ZERO;
        List<MrpPurchaseCandidateVendorView> vendors = new ArrayList<>();
        for (int index = 0; index < allocatable.size(); index++) {
            UnitPriceView price = allocatable.get(index);
            BigDecimal orderQty;
            if (index == allocatable.size() - 1) {
                orderQty = remainingQty.subtract(allocatedQty);
            } else {
                orderQty = remainingQty.multiply(price.orderRate())
                        .divide(HUNDRED, 4, RoundingMode.HALF_UP);
                allocatedQty = allocatedQty.add(orderQty);
            }
            BigDecimal unitPrice = resolveUnitPriceAmount(price);
            vendors.add(new MrpPurchaseCandidateVendorView(
                    price.companyId(),
                    price.companyName(),
                    price.businessRegNo(),
                    price.orderRate(),
                    orderQty,
                    unitPrice,
                    lineAmount(orderQty, unitPrice),
                    leadTimeDays,
                    requestedDeliveryDate
            ));
        }
        return vendors;
    }

    private LocalDate resolveRequestedDeliveryDate(LocalDate orderDate, Integer leadTimeDays) {
        if (orderDate == null || leadTimeDays == null || leadTimeDays <= 0) {
            return null;
        }
        return orderDate.plusDays(leadTimeDays);
    }

    private Map<Long, List<UnitPriceView>> resolveEffectivePurchasePricesByItem(
            Set<Long> itemIds,
            LocalDate refDate
    ) {
        if (itemIds.isEmpty()) {
            return Map.of();
        }
        List<UnitPriceView> rows = unitPriceRepository.findAllActiveByCostTypeAndItemIds(CostType.PURCHASE, itemIds);
        Map<String, UnitPriceView> latestByItemAndCompany = rows.stream()
                .filter(price -> isEffectiveOn(price, refDate))
                .collect(Collectors.toMap(
                        price -> price.itemId() + ":" + price.companyId(),
                        Function.identity(),
                        (left, right) -> left.beginDate().isAfter(right.beginDate()) ? left : right
                ));
        return latestByItemAndCompany.values().stream()
                .collect(Collectors.groupingBy(UnitPriceView::itemId));
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

    public PurchaseOrderView register(PurchaseOrderCommand command, String actorUserId) {
        validateCommand(command);
        validatePartner(command.partnerId());
        validateLines(command.lines(), command.sourceType());

        String orderNo = resolveOrderNo(command.orderNo(), command.orderDate());
        if (purchaseOrderRepository.existsActiveByOrderNo(orderNo)) {
            throw new IllegalArgumentException("이미 사용 중인 발주번호입니다: " + orderNo);
        }

        long orderId = purchaseOrderRepository.save(command, orderNo, actorUserId);
        appendEvent(EventTypes.PURCHASE_ORDER_REGISTERED, orderId, actorUserId, orderNo, command.partnerId());
        return get(orderId);
    }

    public PurchaseOrderView registerFromMrp(CreatePurchaseOrderFromMrpCommand command, String actorUserId) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("발주할 자재소요를 1건 이상 선택하세요.");
        }
        validatePartner(command.partnerId());

        Map<Long, MaterialRequirementLineView> requirementById = mrpRepository.findAllActiveLines().stream()
                .collect(Collectors.toMap(MaterialRequirementLineView::id, Function.identity()));

        List<PurchaseOrderLineCommand> lineCommands = new ArrayList<>();
        Set<Long> seenRequirementIds = new HashSet<>();
        for (CreatePurchaseOrderFromMrpLineCommand line : command.lines()) {
            if (!seenRequirementIds.add(line.requirementLineId())) {
                throw new IllegalArgumentException("중복된 자재소요 라인입니다: " + line.requirementLineId());
            }
            MaterialRequirementLineView requirement = requirementById.get(line.requirementLineId());
            if (requirement == null) {
                throw new IllegalArgumentException("자재소요 라인을 찾을 수 없습니다: " + line.requirementLineId());
            }
            if (line.orderQty() == null || line.orderQty().compareTo(BigDecimal.ZERO) <= 0) {
                throw new IllegalArgumentException("발주수량은 0보다 커야 합니다.");
            }
            validateRequirementOrderQty(line.requirementLineId(), line.orderQty(), requirement.grossQty());
            lineCommands.add(new PurchaseOrderLineCommand(
                    requirement.componentItemId(),
                    line.orderQty(),
                    line.unitPrice() != null ? line.unitPrice() : BigDecimal.ZERO,
                    requirement.id(),
                    line.requestedDeliveryDate()
            ));
        }

        PurchaseOrderCommand orderCommand = new PurchaseOrderCommand(
                null,
                command.partnerId(),
                command.orderDate() != null ? command.orderDate() : LocalDate.now(),
                PurchaseOrderSourceType.MRP,
                lineCommands
        );
        validateLines(lineCommands, PurchaseOrderSourceType.MRP);
        return register(orderCommand, actorUserId);
    }

    public PurchaseOrderView update(long id, PurchaseOrderCommand command, String actorUserId) {
        ensureDraft(id);
        validateCommand(command);
        validatePartner(command.partnerId());
        validateLines(command.lines(), command.sourceType());

        purchaseOrderRepository.updateHeader(id, command, actorUserId);
        purchaseOrderRepository.replaceLines(id, command, actorUserId);
        appendEvent(EventTypes.PURCHASE_ORDER_UPDATED, id, actorUserId, null, command.partnerId());
        return get(id);
    }

    public PurchaseOrderView confirm(long id, String actorUserId) {
        PurchaseOrderView existing = get(id);
        if (existing.status() != PurchaseOrderStatus.DRAFT) {
            throw new IllegalArgumentException("작성중 발주만 확정할 수 있습니다.");
        }
        if (existing.lines().isEmpty()) {
            throw new IllegalArgumentException("발주 라인이 없습니다.");
        }

        purchaseOrderRepository.markConfirmed(id, actorUserId);
        appendEvent(EventTypes.PURCHASE_ORDER_CONFIRMED, id, actorUserId, existing.orderNo(), existing.partnerId());
        return get(id);
    }

    public void cancel(long id, String actorUserId) {
        PurchaseOrderView existing = get(id);
        if (existing.status() == PurchaseOrderStatus.CANCELLED) {
            return;
        }
        if (!existing.cancelable()) {
            if (existing.status() == PurchaseOrderStatus.IN_PROGRESS || existing.status() == PurchaseOrderStatus.RECEIVED) {
                throw new IllegalArgumentException("입고 진행·완료 상태의 발주는 취소할 수 없습니다. 입고를 먼저 취소해 주세요.");
            }
            throw new IllegalArgumentException("입고·검사대기 수량이 남아 있어 발주를 취소할 수 없습니다.");
        }

        purchaseOrderRepository.updateStatus(id, PurchaseOrderStatus.CANCELLED, actorUserId);
        purchaseOrderRepository.clearRequirementLineReferences(id, actorUserId);
        appendEvent(EventTypes.PURCHASE_ORDER_CANCELLED, id, actorUserId, existing.orderNo(), existing.partnerId());
    }

    public String previewNextOrderNo(LocalDate orderDate) {
        if (orderDate == null) {
            throw new IllegalArgumentException("발주일은 필수입니다.");
        }
        return nextOrderNo(orderDate);
    }

    public static BigDecimal lineAmount(BigDecimal qty, BigDecimal unitPrice) {
        BigDecimal price = unitPrice != null ? unitPrice : BigDecimal.ZERO;
        return qty.multiply(price).setScale(2, RoundingMode.HALF_UP);
    }

    private void appendEvent(String eventType, long orderId, String actorUserId, String orderNo, Long partnerId) {
        String payload = orderNo != null
                ? """
                {"purchaseOrderId":%d,"orderNo":"%s","partnerId":%d}
                """.formatted(orderId, orderNo, partnerId).trim()
                : """
                {"purchaseOrderId":%d,"partnerId":%d}
                """.formatted(orderId, partnerId).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.PURCHASE_ORDER,
                String.valueOf(orderId),
                actorUserId,
                payload
        ));
    }

    private void ensureDraft(long id) {
        PurchaseOrderStatus status = purchaseOrderRepository.findStatus(id);
        if (status != PurchaseOrderStatus.DRAFT) {
            throw new IllegalArgumentException("작성중 발주만 수정할 수 있습니다.");
        }
    }

    private void validateCommand(PurchaseOrderCommand command) {
        if (command.orderDate() == null) {
            throw new IllegalArgumentException("발주일은 필수입니다.");
        }
        if (command.sourceType() == null) {
            throw new IllegalArgumentException("발주 출처(sourceType)는 필수입니다.");
        }
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("발주 라인은 1건 이상 필요합니다.");
        }
    }

    private void validatePartner(long partnerId) {
        companyRepository.findActiveById(partnerId)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + partnerId));
        List<CompanyRoleType> roles = companyRepository.findRoles(partnerId);
        if (!roles.contains(CompanyRoleType.PURCHASE)) {
            throw new IllegalArgumentException("구매거래처(PURCHASE)로 매핑된 거래처만 선택할 수 있습니다.");
        }
    }

    private void validateLines(List<PurchaseOrderLineCommand> lines, PurchaseOrderSourceType sourceType) {
        int lineNo = 1;
        Set<Long> requirementIds = new HashSet<>();
        for (PurchaseOrderLineCommand line : lines) {
            final int currentLine = lineNo;
            if (line.orderQty() == null || line.orderQty().compareTo(BigDecimal.ZERO) <= 0) {
                throw new IllegalArgumentException("라인 " + currentLine + ": 수량은 0보다 커야 합니다.");
            }
            ItemView item = itemRepository.findActiveById(line.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("라인 " + currentLine + ": 품목을 찾을 수 없습니다."));
            if (!ALLOWED_ITEM_CLASSES.contains(item.propertyClassification())) {
                throw new IllegalArgumentException(
                        "라인 " + currentLine + ": 구매 품목은 원자재·상품만 가능합니다."
                );
            }
            if (line.requirementLineId() != null) {
                if (!requirementIds.add(line.requirementLineId())) {
                    throw new IllegalArgumentException("라인 " + currentLine + ": 자재소요 라인이 중복되었습니다.");
                }
                MaterialRequirementLineView requirement = mrpRepository.findActiveLineById(line.requirementLineId())
                        .orElseThrow(() -> new IllegalArgumentException("라인 " + currentLine + ": 자재소요를 찾을 수 없습니다."));
                if (requirement.componentItemId() != line.itemId()) {
                    throw new IllegalArgumentException("라인 " + currentLine + ": 자재소요 품목과 발주 품목이 일치하지 않습니다.");
                }
                validateRequirementOrderQty(line.requirementLineId(), line.orderQty(), requirement.grossQty());
            } else if (sourceType == PurchaseOrderSourceType.MRP) {
                throw new IllegalArgumentException("라인 " + currentLine + ": MRP 발주는 자재소요 라인 연결이 필요합니다.");
            }
            lineNo++;
        }
    }

    private String resolveOrderNo(String requestedOrderNo, LocalDate orderDate) {
        if (requestedOrderNo != null && !requestedOrderNo.isBlank()) {
            return requestedOrderNo.trim();
        }
        return nextOrderNo(orderDate);
    }

    private String nextOrderNo(LocalDate orderDate) {
        String prefix = "PO-" + orderDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = purchaseOrderRepository.countByOrderNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private void validateRequirementOrderQty(long requirementLineId, BigDecimal orderQty, BigDecimal grossQty) {
        BigDecimal orderedQty = purchaseOrderRepository.sumOrderedQtyByRequirementLineId(requirementLineId);
        BigDecimal remainingQty = grossQty.subtract(orderedQty);
        if (orderQty.compareTo(remainingQty) > 0) {
            throw new IllegalArgumentException(
                    "발주수량(" + orderQty.stripTrailingZeros().toPlainString()
                            + ")이 잔여 소요량(" + remainingQty.stripTrailingZeros().toPlainString() + ")을 초과합니다."
            );
        }
    }
}
