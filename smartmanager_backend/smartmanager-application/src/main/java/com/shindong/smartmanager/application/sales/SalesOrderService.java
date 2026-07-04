package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.EnumSet;
import java.util.List;
import java.util.Set;

public class SalesOrderService {

    private static final Set<PropertyClassification> ALLOWED_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.상품, PropertyClassification.제품, PropertyClassification.공정품);

    private final SalesOrderRepository salesOrderRepository;
    private final CompanyRepository companyRepository;
    private final ItemRepository itemRepository;
    private final DomainEventStore domainEventStore;

    public SalesOrderService(
            SalesOrderRepository salesOrderRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            DomainEventStore domainEventStore
    ) {
        this.salesOrderRepository = salesOrderRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.domainEventStore = domainEventStore;
    }

    public List<SalesOrderView> list() {
        return salesOrderRepository.findAllActive();
    }

    public SalesOrderView get(long id) {
        return salesOrderRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("수주를 찾을 수 없습니다: " + id));
    }

    public SalesOrderView register(SalesOrderCommand command, String actorUserId) {
        validateCommand(command);
        validatePartner(command.partnerId());
        validateLines(command.lines());

        String orderNo = resolveOrderNo(command.orderNo(), command.orderDate());
        if (salesOrderRepository.existsActiveByOrderNo(orderNo)) {
            throw new IllegalArgumentException("이미 사용 중인 수주번호입니다: " + orderNo);
        }
        long orderId = salesOrderRepository.save(command, orderNo, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.SALES_ORDER_REGISTERED,
                1,
                AggregateTypes.SALES_ORDER,
                String.valueOf(orderId),
                actorUserId,
                """
                {"salesOrderId":%d,"orderNo":"%s","partnerId":%d}
                """.formatted(orderId, orderNo, command.partnerId()).trim()
        ));

        return get(orderId);
    }

    public SalesOrderView update(long id, SalesOrderCommand command, String actorUserId) {
        ensureDraft(id);
        ensureOrderEditable(id);
        validateCommand(command);
        validatePartner(command.partnerId());
        validateLines(command.lines());

        salesOrderRepository.updateHeader(id, command, actorUserId);
        salesOrderRepository.replaceLines(id, command, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.SALES_ORDER_UPDATED,
                1,
                AggregateTypes.SALES_ORDER,
                String.valueOf(id),
                actorUserId,
                """
                {"salesOrderId":%d,"partnerId":%d}
                """.formatted(id, command.partnerId()).trim()
        ));

        return get(id);
    }

    public SalesOrderView confirm(long id, String actorUserId) {
        SalesOrderView existing = get(id);
        if (existing.status() != SalesOrderStatus.DRAFT) {
            throw new IllegalArgumentException("작성중 수주만 확정할 수 있습니다.");
        }
        if (existing.lines().isEmpty()) {
            throw new IllegalArgumentException("수주 라인이 없습니다.");
        }

        salesOrderRepository.markConfirmed(id, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.SALES_ORDER_CONFIRMED,
                1,
                AggregateTypes.SALES_ORDER,
                String.valueOf(id),
                actorUserId,
                """
                {"salesOrderId":%d,"orderNo":"%s"}
                """.formatted(id, existing.orderNo()).trim()
        ));

        return get(id);
    }

    public void cancel(long id, String actorUserId) {
        SalesOrderView existing = get(id);
        if (existing.status() != SalesOrderStatus.DRAFT) {
            throw new IllegalArgumentException("작성중 수주만 취소할 수 있습니다.");
        }

        salesOrderRepository.updateStatus(id, SalesOrderStatus.CANCELLED, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.SALES_ORDER_CANCELLED,
                1,
                AggregateTypes.SALES_ORDER,
                String.valueOf(id),
                actorUserId,
                """
                {"salesOrderId":%d,"orderNo":"%s"}
                """.formatted(id, existing.orderNo()).trim()
        ));
    }

    public SalesOrderBulkResult registerBulk(List<SalesOrderCommand> commands, String actorUserId) {
        if (commands == null || commands.isEmpty()) {
            throw new IllegalArgumentException("일괄 등록할 수주가 없습니다.");
        }
        List<SalesOrderView> created = new java.util.ArrayList<>();
        List<SalesOrderBulkFailure> failures = new java.util.ArrayList<>();
        for (int i = 0; i < commands.size(); i++) {
            SalesOrderCommand command = commands.get(i);
            try {
                created.add(register(command, actorUserId));
            } catch (RuntimeException ex) {
                failures.add(new SalesOrderBulkFailure(
                        i,
                        command.orderNo(),
                        ex.getMessage() != null ? ex.getMessage() : "등록 실패"
                ));
            }
        }
        return new SalesOrderBulkResult(created.size(), failures.size(), created, failures);
    }

    public String previewNextOrderNo(LocalDate orderDate) {
        if (orderDate == null) {
            throw new IllegalArgumentException("수주일은 필수입니다.");
        }
        return nextOrderNo(orderDate);
    }

    public List<SalesOrderLineListView> listLines(SalesOrderLineListCriteria criteria) {
        return salesOrderRepository.findLineList(criteria != null ? criteria : new SalesOrderLineListCriteria(
                null, null, null, null, null, null
        ));
    }

    public SalesOrderLineListView startLineProgress(long lineId, String actorUserId) {
        SalesOrderLineListView line = requireLine(lineId);
        ensureOrderConfirmed(line);
        if (line.fulfillmentStatus() != SalesLineFulfillmentStatus.WAITING) {
            throw new IllegalArgumentException("대기 상태에서만 진행 처리할 수 있습니다.");
        }
        salesOrderRepository.updateLineFulfillmentStatus(lineId, SalesLineFulfillmentStatus.IN_PROGRESS, actorUserId);
        return requireLine(lineId);
    }

    public SalesOrderLineListView completeLine(long lineId, String actorUserId) {
        SalesOrderLineListView line = requireLine(lineId);
        ensureOrderConfirmed(line);
        if (line.fulfillmentStatus() != SalesLineFulfillmentStatus.IN_PROGRESS) {
            throw new IllegalArgumentException("진행 상태에서만 완료 처리할 수 있습니다.");
        }
        salesOrderRepository.updateLineFulfillmentStatus(lineId, SalesLineFulfillmentStatus.COMPLETED, actorUserId);
        return requireLine(lineId);
    }

    public SalesOrderLineListView forceCompleteLine(long lineId, String actorUserId) {
        SalesOrderLineListView line = requireLine(lineId);
        if (line.deliveryStatus() == SalesLineDeliveryStatus.COMPLETED) {
            throw new IllegalArgumentException("납품 완료된 수주 라인은 강제완료할 수 없습니다.");
        }
        if (line.orderStatus() == SalesOrderStatus.DRAFT) {
            confirm(line.orderId(), actorUserId);
            line = requireLine(lineId);
        } else if (line.orderStatus() != SalesOrderStatus.CONFIRMED) {
            throw new IllegalArgumentException("취소된 수주는 강제완료할 수 없습니다.");
        }
        if (line.fulfillmentStatus() == SalesLineFulfillmentStatus.FORCE_COMPLETED) {
            return line;
        }
        salesOrderRepository.updateLineFulfillmentStatus(
                lineId,
                SalesLineFulfillmentStatus.FORCE_COMPLETED,
                actorUserId
        );
        return requireLine(lineId);
    }

    private SalesOrderLineListView requireLine(long lineId) {
        return salesOrderRepository.findLineListItem(lineId)
                .orElseThrow(() -> new IllegalArgumentException("수주 라인을 찾을 수 없습니다: " + lineId));
    }

    private void ensureOrderConfirmed(SalesOrderLineListView line) {
        if (line.orderStatus() != SalesOrderStatus.CONFIRMED) {
            throw new IllegalArgumentException("확정된 수주만 이행 상태를 변경할 수 있습니다.");
        }
    }

    public static SalesFulfillmentRoute resolveRoute(PropertyClassification classification) {
        if (classification == PropertyClassification.상품) {
            return SalesFulfillmentRoute.COMMODITY;
        }
        return SalesFulfillmentRoute.MANUFACTURING;
    }

    private void ensureDraft(long id) {
        SalesOrderStatus status = salesOrderRepository.findStatus(id);
        if (status != SalesOrderStatus.DRAFT) {
            throw new IllegalArgumentException("작성중 수주만 수정할 수 있습니다.");
        }
    }

    private void ensureOrderEditable(long id) {
        SalesOrderView order = get(id);
        boolean progressed = order.lines().stream()
                .anyMatch(line -> line.fulfillmentStatus() != SalesLineFulfillmentStatus.WAITING);
        if (progressed) {
            throw new IllegalArgumentException("생산계획 수립 또는 이행이 시작된 수주는 수정할 수 없습니다.");
        }
    }

    private void validateCommand(SalesOrderCommand command) {
        if (command.orderDate() == null) {
            throw new IllegalArgumentException("수주일은 필수입니다.");
        }
        if (command.requestedDeliveryDate() == null) {
            throw new IllegalArgumentException("납기요구일은 필수입니다.");
        }
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("수주 라인은 1건 이상 필요합니다.");
        }
    }

    private void validatePartner(long partnerId) {
        companyRepository.findActiveById(partnerId)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + partnerId));
        List<CompanyRoleType> roles = companyRepository.findRoles(partnerId);
        if (!roles.contains(CompanyRoleType.SALES)) {
            throw new IllegalArgumentException("수주거래처(SALES)로 매핑된 거래처만 선택할 수 있습니다.");
        }
    }

    private void validateLines(List<SalesOrderLineCommand> lines) {
        int lineNo = 1;
        for (SalesOrderLineCommand line : lines) {
            final int currentLine = lineNo;
            if (line.orderQty() == null || line.orderQty().compareTo(BigDecimal.ZERO) <= 0) {
                throw new IllegalArgumentException("라인 " + currentLine + ": 수량은 0보다 커야 합니다.");
            }
            ItemView item = itemRepository.findActiveById(line.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("라인 " + currentLine + ": 품목을 찾을 수 없습니다."));
            if (!ALLOWED_ITEM_CLASSES.contains(item.propertyClassification())) {
                throw new IllegalArgumentException(
                        "라인 " + currentLine + ": 수주 품목은 상품·제품·공정품만 가능합니다. (원자재 불가)"
                );
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
        String prefix = "SO-" + orderDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = salesOrderRepository.countByOrderNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    public static BigDecimal lineAmount(BigDecimal qty, BigDecimal unitPrice) {
        BigDecimal price = unitPrice != null ? unitPrice : BigDecimal.ZERO;
        return qty.multiply(price).setScale(2, RoundingMode.HALF_UP);
    }
}
