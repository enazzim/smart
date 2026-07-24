package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.production.WorkPlanRepository;
import com.shindong.smartmanager.application.production.WorkPlanView;
import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderSourceType;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import com.shindong.smartmanager.domain.pricing.CostType;
import com.shindong.smartmanager.domain.process.WorkDistinction;
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

public class OutsourcingOrderService {

    private static final Set<PropertyClassification> ALLOWED_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.제품, PropertyClassification.공정품);

    private static final BigDecimal HUNDRED = new BigDecimal("100");

    private static final String DEFAULT_ISSUER_COMPANY_NAME = "유한책임회사 신동공업";
    private static final String DEFAULT_ISSUER_ADDRESS = "경상남도 사천시 곤양면 곤북로 82";
    private static final String DEFAULT_ISSUER_PHONE = "055) 855-0145";
    private static final String DEFAULT_ISSUER_FAX = "055-762-9251";

    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final WorkPlanRepository workPlanRepository;
    private final CompanyRepository companyRepository;
    private final ItemRepository itemRepository;
    private final UnitPriceRepository unitPriceRepository;
    private final DomainEventStore domainEventStore;

    public OutsourcingOrderService(
            OutsourcingOrderRepository outsourcingOrderRepository,
            WorkPlanRepository workPlanRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            UnitPriceRepository unitPriceRepository,
            DomainEventStore domainEventStore
    ) {
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.workPlanRepository = workPlanRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.unitPriceRepository = unitPriceRepository;
        this.domainEventStore = domainEventStore;
    }

    public List<OutsourcingOrderView> list() {
        return outsourcingOrderRepository.findAllActive();
    }

    public List<OutsourcingOrderView> list(OutsourcingOrderListCriteria criteria) {
        return outsourcingOrderRepository.findAllActive(criteria);
    }

    public OutsourcingOrderView get(long id) {
        return outsourcingOrderRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("외주발주를 찾을 수 없습니다: " + id));
    }

    public OutsourcingOrderPrintView getPrintView(long id) {
        OutsourcingOrderView order = get(id);
        if (order.status() == OutsourcingOrderStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 발주는 출력할 수 없습니다.");
        }
        return buildPrintView(List.of(order));
    }

    public List<OutsourcingOrderPrintView> getBatchPrintViews(List<Long> orderIds) {
        if (orderIds == null || orderIds.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주를 1건 이상 선택해 주세요.");
        }
        List<OutsourcingOrderView> orders = new ArrayList<>();
        for (Long orderId : orderIds) {
            if (orderId == null) {
                continue;
            }
            OutsourcingOrderView order = get(orderId);
            if (order.status() == OutsourcingOrderStatus.CANCELLED) {
                throw new IllegalArgumentException("취소된 발주는 출력할 수 없습니다: " + order.orderNo());
            }
            orders.add(order);
        }
        if (orders.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주를 1건 이상 선택해 주세요.");
        }
        Map<Long, List<OutsourcingOrderView>> byPartner = orders.stream()
                .collect(Collectors.groupingBy(OutsourcingOrderView::partnerId));
        List<OutsourcingOrderPrintView> views = new ArrayList<>();
        for (List<OutsourcingOrderView> partnerOrders : byPartner.values()) {
            views.add(buildPrintView(partnerOrders));
        }
        views.sort((left, right) -> left.partnerName().compareToIgnoreCase(right.partnerName()));
        return views;
    }

    private OutsourcingOrderPrintView buildPrintView(List<OutsourcingOrderView> orders) {
        if (orders.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주가 없습니다.");
        }
        OutsourcingOrderView first = orders.get(0);
        var partner = companyRepository.findActiveById(first.partnerId())
                .orElseThrow(() -> new IllegalStateException("거래처를 찾을 수 없습니다: " + first.partnerId()));

        String orderNos = orders.stream()
                .map(OutsourcingOrderView::orderNo)
                .distinct()
                .collect(Collectors.joining(", "));
        LocalDate orderDate = orders.stream()
                .map(OutsourcingOrderView::orderDate)
                .min(LocalDate::compareTo)
                .orElse(first.orderDate());
        String orderManagerName = orders.stream()
                .map(OutsourcingOrderView::createdBy)
                .filter(name -> name != null && !name.isBlank())
                .findFirst()
                .orElse("—");

        List<OutsourcingOrderPrintLineView> lines = new ArrayList<>();
        BigDecimal totalAmount = BigDecimal.ZERO;
        int lineNo = 1;
        for (OutsourcingOrderView order : orders) {
            for (OutsourcingOrderLineView line : order.lines()) {
                ItemView item = itemRepository.findActiveById(line.itemId()).orElse(null);
                BigDecimal amount = line.amount() != null ? line.amount() : BigDecimal.ZERO;
                lines.add(new OutsourcingOrderPrintLineView(
                        lineNo++,
                        line.itemName(),
                        line.itemNo(),
                        item != null ? nullToEmpty(item.modelType()) : "",
                        item != null ? nullToEmpty(item.standard()) : "",
                        line.beginProcessName(),
                        line.endProcessName(),
                        item != null ? nullToEmpty(item.unit()) : "EA",
                        line.orderQty(),
                        line.unitPrice(),
                        amount,
                        line.requestedDeliveryDate(),
                        line.planNo() != null ? line.planNo() : order.orderNo()
                ));
                totalAmount = totalAmount.add(amount);
            }
        }

        return new OutsourcingOrderPrintView(
                orderNos,
                orderDate,
                first.partnerName(),
                first.partnerBusinessRegNo(),
                partner.telephone(),
                partner.fax(),
                DEFAULT_ISSUER_COMPANY_NAME,
                DEFAULT_ISSUER_ADDRESS,
                DEFAULT_ISSUER_PHONE,
                DEFAULT_ISSUER_FAX,
                orderManagerName,
                lines,
                totalAmount
        );
    }

    private static String nullToEmpty(String value) {
        return value != null ? value : "";
    }

    public List<WorkPlanOutsourceCandidateView> listWorkPlanCandidates() {
        return listWorkPlanCandidates(LocalDate.now());
    }

    public List<WorkPlanOutsourceCandidateView> listWorkPlanCandidates(LocalDate orderDate) {
        LocalDate effectiveDate = orderDate != null ? orderDate : LocalDate.now();
        List<WorkPlanView> outsourcePlans = workPlanRepository.findAllActive(null).stream()
                .filter(plan -> plan.workDistinction() == WorkDistinction.OUTSOURCE)
                .toList();
        if (outsourcePlans.isEmpty()) {
            return List.of();
        }

        Set<Long> workPlanIds = outsourcePlans.stream().map(WorkPlanView::id).collect(Collectors.toSet());
        Map<Long, BigDecimal> orderedQtyByWorkPlanId =
                outsourcingOrderRepository.sumOrderedQtyByWorkPlanIds(workPlanIds);

        Set<Long> itemIds = outsourcePlans.stream().map(WorkPlanView::itemId).collect(Collectors.toSet());
        Map<Long, List<UnitPriceView>> pricesByItemId =
                resolveEffectiveOutsourcePricesByItem(itemIds, effectiveDate);

        List<WorkPlanOutsourceCandidateView> candidates = new ArrayList<>();
        for (WorkPlanView plan : outsourcePlans) {
            BigDecimal orderedQty = orderedQtyByWorkPlanId.getOrDefault(plan.id(), BigDecimal.ZERO);
            BigDecimal remainingQty = plan.plannedQty().subtract(orderedQty);
            if (remainingQty.compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            List<UnitPriceView> prices = pricesByItemId.getOrDefault(plan.itemId(), List.of());
            candidates.add(toWorkPlanCandidateView(plan, orderedQty, remainingQty, prices, effectiveDate));
        }
        return candidates;
    }

    public OutsourcingOrderView register(OutsourcingOrderCommand command, String actorUserId) {
        validateCommand(command);
        validatePartner(command.partnerId());
        validateLines(command.lines());

        String orderNo = resolveOrderNo(command.orderNo(), command.orderDate());
        if (outsourcingOrderRepository.existsActiveByOrderNo(orderNo)) {
            throw new IllegalArgumentException("이미 사용 중인 발주번호입니다: " + orderNo);
        }

        long orderId = outsourcingOrderRepository.save(command, orderNo, actorUserId);
        appendEvent(EventTypes.OUTSOURCING_ORDER_REGISTERED, orderId, actorUserId, orderNo, command.partnerId());
        return get(orderId);
    }

    public OutsourcingOrderView registerFromWorkPlan(
            CreateOutsourcingOrderFromWorkPlanCommand command,
            String actorUserId
    ) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("발주할 작업계획을 1건 이상 선택하세요.");
        }
        validatePartner(command.partnerId());

        Map<Long, WorkPlanView> planById = workPlanRepository.findAllActive(null).stream()
                .filter(plan -> plan.workDistinction() == WorkDistinction.OUTSOURCE)
                .collect(Collectors.toMap(WorkPlanView::id, Function.identity()));

        Set<Long> workPlanIds = planById.keySet();
        Map<Long, BigDecimal> orderedQtyByWorkPlanId =
                outsourcingOrderRepository.sumOrderedQtyByWorkPlanIds(workPlanIds);

        List<OutsourcingOrderLineCommand> lineCommands = new ArrayList<>();
        Set<Long> seenWorkPlanIds = new HashSet<>();
        for (CreateOutsourcingOrderFromWorkPlanLineCommand line : command.lines()) {
            if (!seenWorkPlanIds.add(line.workPlanId())) {
                throw new IllegalArgumentException("중복된 작업계획입니다: " + line.workPlanId());
            }
            WorkPlanView plan = planById.get(line.workPlanId());
            if (plan == null) {
                throw new IllegalArgumentException("외주 작업계획을 찾을 수 없습니다: " + line.workPlanId());
            }
            if (line.orderQty() == null || line.orderQty().compareTo(BigDecimal.ZERO) <= 0) {
                throw new IllegalArgumentException("발주수량은 0보다 커야 합니다.");
            }
            validateWorkPlanOrderQty(
                    line.workPlanId(),
                    line.orderQty(),
                    plan.plannedQty(),
                    orderedQtyByWorkPlanId
            );
            lineCommands.add(new OutsourcingOrderLineCommand(
                    plan.itemId(),
                    plan.processSequenceId(),
                    line.beginProcessCodeId(),
                    line.endProcessCodeId(),
                    plan.id(),
                    line.orderQty(),
                    line.unitPrice() != null ? line.unitPrice() : BigDecimal.ZERO,
                    line.requestedDeliveryDate() != null ? line.requestedDeliveryDate() : plan.planEndDate()
            ));
        }

        OutsourcingOrderCommand orderCommand = new OutsourcingOrderCommand(
                null,
                command.partnerId(),
                command.orderDate() != null ? command.orderDate() : LocalDate.now(),
                OutsourcingOrderSourceType.WORK_PLAN,
                lineCommands
        );
        return register(orderCommand, actorUserId);
    }

    public void cancel(long id, String actorUserId) {
        OutsourcingOrderView existing = get(id);
        if (existing.status() == OutsourcingOrderStatus.CANCELLED) {
            return;
        }
        if (!existing.cancelable()) {
            throw new IllegalArgumentException("출고·입고 실적이 있어 외주발주를 취소할 수 없습니다.");
        }
        outsourcingOrderRepository.updateStatus(id, OutsourcingOrderStatus.CANCELLED, actorUserId);
        appendEvent(EventTypes.OUTSOURCING_ORDER_CANCELLED, id, actorUserId, existing.orderNo(), existing.partnerId());
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

    private WorkPlanOutsourceCandidateView toWorkPlanCandidateView(
            WorkPlanView plan,
            BigDecimal orderedQty,
            BigDecimal remainingQty,
            List<UnitPriceView> outsourcePrices,
            LocalDate orderDate
    ) {
        List<UnitPriceView> allocatable = outsourcePrices.stream()
                .filter(price -> price.orderRate() != null && price.orderRate().compareTo(BigDecimal.ZERO) > 0)
                .sorted((left, right) -> left.companyName().compareToIgnoreCase(right.companyName()))
                .toList();
        BigDecimal orderRateTotal = allocatable.stream()
                .map(UnitPriceView::orderRate)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
        List<WorkPlanOutsourceCandidateVendorView> vendors =
                buildVendorSplits(remainingQty, allocatable, plan.planEndDate());
        boolean orderable = remainingQty.compareTo(BigDecimal.ZERO) > 0
                && !vendors.isEmpty()
                && orderRateTotal.compareTo(HUNDRED) == 0;
        String orderableMessage = resolveOrderableMessage(vendors, orderRateTotal, remainingQty);
        return new WorkPlanOutsourceCandidateView(
                plan.id(),
                plan.productionPlanId(),
                plan.planNo(),
                plan.itemId(),
                plan.itemNo(),
                plan.itemName(),
                plan.processSequenceId(),
                plan.processSequenceNum(),
                plan.processCode(),
                plan.processName(),
                plan.plannedQty(),
                orderedQty,
                remainingQty,
                plan.planEndDate(),
                orderRateTotal,
                orderable,
                orderableMessage,
                vendors
        );
    }

    private String resolveOrderableMessage(
            List<WorkPlanOutsourceCandidateVendorView> vendors,
            BigDecimal orderRateTotal,
            BigDecimal remainingQty
    ) {
        if (remainingQty.compareTo(BigDecimal.ZERO) <= 0) {
            return "발주가 완료된 작업계획입니다.";
        }
        if (vendors.isEmpty()) {
            return "유효한 외주단가·거래처가 없습니다.";
        }
        if (orderRateTotal.compareTo(HUNDRED) != 0) {
            return "발주비율 합계가 100%가 아닙니다. (현재 "
                    + orderRateTotal.stripTrailingZeros().toPlainString() + "%)";
        }
        return null;
    }

    private List<WorkPlanOutsourceCandidateVendorView> buildVendorSplits(
            BigDecimal remainingQty,
            List<UnitPriceView> allocatable,
            LocalDate requestedDeliveryDate
    ) {
        if (allocatable.isEmpty()) {
            return List.of();
        }
        BigDecimal allocatedQty = BigDecimal.ZERO;
        List<WorkPlanOutsourceCandidateVendorView> vendors = new ArrayList<>();
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
            vendors.add(new WorkPlanOutsourceCandidateVendorView(
                    price.companyId(),
                    price.companyName(),
                    price.businessRegNo(),
                    price.beginProcessCodeId(),
                    price.beginProcessCode(),
                    price.beginProcessName(),
                    price.endProcessCodeId(),
                    price.endProcessCode(),
                    price.endProcessName(),
                    price.orderRate(),
                    orderQty,
                    unitPrice,
                    lineAmount(orderQty, unitPrice),
                    requestedDeliveryDate
            ));
        }
        return vendors;
    }

    private Map<Long, List<UnitPriceView>> resolveEffectiveOutsourcePricesByItem(
            Set<Long> itemIds,
            LocalDate refDate
    ) {
        if (itemIds.isEmpty()) {
            return Map.of();
        }
        List<UnitPriceView> rows = unitPriceRepository.findAllActiveByCostTypeAndItemIds(CostType.OUTSOURCE, itemIds);
        Map<String, UnitPriceView> latestByItemCompanySegment = new HashMap<>();
        for (UnitPriceView price : rows) {
            if (!isEffectiveOn(price, refDate)) {
                continue;
            }
            String key = price.itemId() + ":" + price.companyId() + ":"
                    + price.beginProcessCodeId() + ":" + price.endProcessCodeId();
            UnitPriceView existing = latestByItemCompanySegment.get(key);
            if (existing == null || price.beginDate().isAfter(existing.beginDate())) {
                latestByItemCompanySegment.put(key, price);
            }
        }
        return latestByItemCompanySegment.values().stream()
                .collect(Collectors.groupingBy(UnitPriceView::itemId));
    }

    private boolean isEffectiveOn(UnitPriceView price, LocalDate refDate) {
        if (price.beginDate().isAfter(refDate)) {
            return false;
        }
        return price.endDate() == null || !price.endDate().isBefore(refDate);
    }

    private BigDecimal resolveUnitPriceAmount(UnitPriceView price) {
        if (price.discountUnitCost() != null && price.discountUnitCost().signum() > 0) {
            return price.discountUnitCost();
        }
        return price.standardUnitCost() != null ? price.standardUnitCost() : BigDecimal.ZERO;
    }

    private void validateWorkPlanOrderQty(
            long workPlanId,
            BigDecimal orderQty,
            BigDecimal plannedQty,
            Map<Long, BigDecimal> orderedQtyByWorkPlanId
    ) {
        BigDecimal orderedQty = orderedQtyByWorkPlanId.getOrDefault(workPlanId, BigDecimal.ZERO);
        BigDecimal remaining = plannedQty.subtract(orderedQty);
        if (orderQty.compareTo(remaining) > 0) {
            throw new IllegalArgumentException(
                    "작업계획 잔량(" + remaining.stripTrailingZeros().toPlainString()
                            + ")을 초과하는 발주수량입니다.");
        }
    }

    private void appendEvent(String eventType, long orderId, String actorUserId, String orderNo, Long partnerId) {
        String payload = orderNo != null
                ? """
                {"outsourcingOrderId":%d,"orderNo":"%s","partnerId":%d}
                """.formatted(orderId, orderNo, partnerId).trim()
                : """
                {"outsourcingOrderId":%d,"partnerId":%d}
                """.formatted(orderId, partnerId).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.OUTSOURCING_ORDER,
                String.valueOf(orderId),
                actorUserId,
                payload
        ));
    }

    private void validateCommand(OutsourcingOrderCommand command) {
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
        if (!roles.contains(CompanyRoleType.OUTSOURCE)) {
            throw new IllegalArgumentException("외주거래처(OUTSOURCE)로 매핑된 거래처만 선택할 수 있습니다.");
        }
    }

    private void validateLines(List<OutsourcingOrderLineCommand> lines) {
        int lineNo = 1;
        for (OutsourcingOrderLineCommand line : lines) {
            final int currentLine = lineNo++;
            if (line.orderQty() == null || line.orderQty().compareTo(BigDecimal.ZERO) <= 0) {
                throw new IllegalArgumentException("라인 " + currentLine + ": 수량은 0보다 커야 합니다.");
            }
            ItemView item = itemRepository.findActiveById(line.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + line.itemId()));
            if (!ALLOWED_ITEM_CLASSES.contains(item.propertyClassification())) {
                throw new IllegalArgumentException(
                        "라인 " + currentLine + ": 외주발주는 제품·공정품만 가능합니다.");
            }
            if (line.beginProcessCodeId() <= 0 || line.endProcessCodeId() <= 0) {
                throw new IllegalArgumentException("라인 " + currentLine + ": 외주 공정구간은 필수입니다.");
            }
        }
    }

    private String resolveOrderNo(String requestedOrderNo, LocalDate orderDate) {
        if (requestedOrderNo != null && !requestedOrderNo.isBlank()) {
            return requestedOrderNo.trim();
        }
        return nextOrderNo(orderDate);
    }

    private String nextOrderNo(LocalDate orderDate) {
        String prefix = "OO-" + orderDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = outsourcingOrderRepository.countByOrderNoPrefix(prefix) + 1;
        return prefix + seq;
    }
}
