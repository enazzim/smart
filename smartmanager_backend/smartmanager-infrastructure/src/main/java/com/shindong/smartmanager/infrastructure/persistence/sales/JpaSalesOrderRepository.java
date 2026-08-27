package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.sales.SalesOrderCommand;
import com.shindong.smartmanager.application.sales.SalesOrderLineCommand;
import com.shindong.smartmanager.application.sales.SalesOrderLineView;
import com.shindong.smartmanager.application.sales.SalesOrderRepository;
import com.shindong.smartmanager.application.sales.SalesOrderService;
import com.shindong.smartmanager.application.sales.SalesOrderView;
import com.shindong.smartmanager.application.sales.SalesOrderLineListCriteria;
import com.shindong.smartmanager.application.sales.SalesOrderLineListView;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import java.time.Instant;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.function.Function;
import java.util.stream.Collectors;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaSalesOrderRepository implements SalesOrderRepository {

    private static final int ACTIVE = 1;

    private final SpringDataSalesOrderRepository orderRepository;
    private final SpringDataSalesOrderLineRepository lineRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final ItemRepository itemLookup;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaSalesOrderRepository(
            SpringDataSalesOrderRepository orderRepository,
            SpringDataSalesOrderLineRepository lineRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            ItemRepository itemLookup,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.orderRepository = orderRepository;
        this.lineRepository = lineRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.itemLookup = itemLookup;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    public long countByOrderNoPrefix(String prefix) {
        return orderRepository.countByOrderNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    public boolean existsActiveByOrderNo(String orderNo) {
        return orderRepository.existsByOrderNoAndRecordingState(orderNo, ACTIVE);
    }

    @Override
    @Transactional
    public long save(SalesOrderCommand command, String orderNo, String actorUserId) {
        Instant now = Instant.now();
        SalesOrderJpaEntity entity = new SalesOrderJpaEntity();
        entity.setOrderNo(orderNo);
        entity.setPartnerId(command.partnerId());
        entity.setOrderDate(command.orderDate());
        entity.setRequestedDeliveryDate(command.requestedDeliveryDate());
        entity.setStatus(SalesOrderStatus.DRAFT);
        entity.setRemark(command.remark());
        entity.setRecordingState(ACTIVE);
        entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setCreatedAt(now);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        SalesOrderJpaEntity saved = orderRepository.save(entity);
        insertLines(saved.getId(), command, actorUserId);
        return saved.getId();
    }

    @Override
    @Transactional
    public void replaceLines(long salesOrderId, SalesOrderCommand command, String actorUserId) {
        lineRepository.deleteBySalesOrderId(salesOrderId);
        lineRepository.flush();
        insertLines(salesOrderId, command, actorUserId);
    }

    @Override
    @Transactional
    public void updateHeader(long salesOrderId, SalesOrderCommand command, String actorUserId) {
        SalesOrderJpaEntity entity = requireActiveOrder(salesOrderId);
        Instant now = Instant.now();
        entity.setPartnerId(command.partnerId());
        entity.setOrderDate(command.orderDate());
        entity.setRequestedDeliveryDate(command.requestedDeliveryDate());
        entity.setRemark(command.remark());
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    @Transactional
    public void updateStatus(long salesOrderId, SalesOrderStatus status, String actorUserId) {
        SalesOrderJpaEntity entity = requireActiveOrder(salesOrderId);
        Instant now = Instant.now();
        entity.setStatus(status);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    @Transactional
    public void markConfirmed(long salesOrderId, String actorUserId) {
        SalesOrderJpaEntity entity = requireActiveOrder(salesOrderId);
        if (entity.getStatus() != SalesOrderStatus.DRAFT) {
            return;
        }
        Instant now = Instant.now();
        entity.setStatus(SalesOrderStatus.CONFIRMED);
        entity.setConfirmedAt(now);
        entity.setConfirmedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    @Transactional
    public void revertToDraft(long salesOrderId, String actorUserId) {
        SalesOrderJpaEntity entity = requireActiveOrder(salesOrderId);
        if (entity.getStatus() != SalesOrderStatus.CONFIRMED) {
            return;
        }
        Instant now = Instant.now();
        entity.setStatus(SalesOrderStatus.DRAFT);
        entity.setConfirmedAt(null);
        entity.setConfirmedById(null);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    public List<SalesOrderView> findAllActive() {
        return orderRepository.findByRecordingStateOrderByOrderDateDescIdDesc(ACTIVE).stream()
                .map(order -> toView(order, loadLines(order.getId())))
                .toList();
    }

    @Override
    public Optional<SalesOrderView> findActiveById(long id) {
        return orderRepository.findByIdAndRecordingState(id, ACTIVE)
                .map(order -> toView(order, loadLines(order.getId())));
    }

    @Override
    public SalesOrderStatus findStatus(long salesOrderId) {
        return requireActiveOrder(salesOrderId).getStatus();
    }

    @Override
    public List<SalesOrderLineListView> findLineList(SalesOrderLineListCriteria criteria) {
        List<SalesOrderJpaEntity> orders = orderRepository.findByRecordingStateOrderByOrderDateDescIdDesc(ACTIVE);
        Map<Long, SalesOrderJpaEntity> orderMap = orders.stream()
                .collect(Collectors.toMap(SalesOrderJpaEntity::getId, Function.identity()));

        Map<Long, CompanyJpaEntity> companies = companyRepository.findAll().stream()
                .filter(company -> company.getRecordingState() == ACTIVE)
                .collect(Collectors.toMap(CompanyJpaEntity::getId, Function.identity()));

        Map<Long, ItemJpaEntity> items = itemRepository.findAll().stream()
                .filter(item -> item.getRecordingState() == ACTIVE)
                .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));

        List<SalesOrderLineJpaEntity> allLines = lineRepository.findByRecordingStateOrderByIdDesc(ACTIVE);
        Map<Long, Boolean> orderEditableMap = allLines.stream()
                .collect(Collectors.groupingBy(SalesOrderLineJpaEntity::getSalesOrderId))
                .entrySet().stream()
                .collect(Collectors.toMap(
                        Map.Entry::getKey,
                        entry -> {
                            SalesOrderJpaEntity order = orderMap.get(entry.getKey());
                            if (order == null || order.getStatus() != SalesOrderStatus.DRAFT) {
                                return false;
                            }
                            return entry.getValue().stream()
                                    .allMatch(line -> line.getFulfillmentStatus() == SalesLineFulfillmentStatus.WAITING);
                        }
                ));

        return allLines.stream()
                .map(line -> toLineListView(
                        line,
                        orderMap.get(line.getSalesOrderId()),
                        companies,
                        items,
                        orderEditableMap.getOrDefault(line.getSalesOrderId(), false)
                ))
                .filter(Optional::isPresent)
                .map(Optional::get)
                .filter(view -> matchesCriteria(view, criteria))
                .toList();
    }

    @Override
    @Transactional
    public void updateLineFulfillmentStatus(
            long lineId,
            SalesLineFulfillmentStatus status,
            String actorUserId
    ) {
        SalesOrderLineJpaEntity line = requireActiveLine(lineId);
        line.setFulfillmentStatus(status);
        lineRepository.save(line);

        SalesOrderJpaEntity order = requireActiveOrder(line.getSalesOrderId());
        Instant now = Instant.now();
        order.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        order.setUpdatedAt(now);
        orderRepository.save(order);
    }

    @Override
    public Optional<SalesOrderLineListView> findLineListItem(long lineId) {
        return lineRepository.findByIdAndRecordingState(lineId, ACTIVE)
                .flatMap(line -> {
                    Optional<SalesOrderJpaEntity> orderOpt = orderRepository.findByIdAndRecordingState(
                            line.getSalesOrderId(),
                            ACTIVE
                    );
                    if (orderOpt.isEmpty()) {
                        return Optional.empty();
                    }
                    Map<Long, CompanyJpaEntity> companies = companyRepository.findAll().stream()
                            .filter(company -> company.getRecordingState() == ACTIVE)
                            .collect(Collectors.toMap(CompanyJpaEntity::getId, Function.identity()));
                    Map<Long, ItemJpaEntity> items = itemRepository.findAll().stream()
                            .filter(item -> item.getRecordingState() == ACTIVE)
                            .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));
                    return toLineListView(line, orderOpt.get(), companies, items, false);
                });
    }

    private SalesOrderJpaEntity requireActiveOrder(long salesOrderId) {
        return orderRepository.findByIdAndRecordingState(salesOrderId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("수주를 찾을 수 없습니다: " + salesOrderId));
    }

    private List<SalesOrderLineJpaEntity> loadLines(long salesOrderId) {
        return lineRepository.findBySalesOrderIdAndRecordingStateOrderByLineNoAsc(salesOrderId, ACTIVE);
    }

    private void insertLines(long salesOrderId, SalesOrderCommand command, String actorUserId) {
        short lineNo = 1;
        for (SalesOrderLineCommand line : command.lines()) {
            ItemView item = itemLookup.findActiveById(line.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + line.itemId()));
            SalesFulfillmentRoute route = SalesOrderService.resolveRoute(item.propertyClassification());

            SalesOrderLineJpaEntity entity = new SalesOrderLineJpaEntity();
            entity.setSalesOrderId(salesOrderId);
            entity.setLineNo(lineNo++);
            entity.setItemId(line.itemId());
            entity.setOrderQty(line.orderQty());
            entity.setUnitPrice(line.unitPrice() != null ? line.unitPrice() : java.math.BigDecimal.ZERO);
            entity.setAmount(SalesOrderService.lineAmount(line.orderQty(), line.unitPrice()));
            entity.setDeliveryDate(line.deliveryDate() != null ? line.deliveryDate() : command.requestedDeliveryDate());
            entity.setFulfillmentRoute(route);
            entity.setFulfillmentStatus(SalesLineFulfillmentStatus.WAITING);
            entity.setDeliveryStatus(SalesLineDeliveryStatus.NOT_STARTED);
            entity.setRecordingState(ACTIVE);
            lineRepository.save(entity);
        }
    }

    private SalesOrderView toView(SalesOrderJpaEntity order, List<SalesOrderLineJpaEntity> lines) {
        CompanyJpaEntity partner = companyRepository.findById(order.getPartnerId()).orElse(null);
        String partnerName = partner != null ? partner.getCompanyName() : "";
        String partnerBusinessRegNo = partner != null ? partner.getBusinessRegNo() : "";

        Map<Long, ItemJpaEntity> items = lines.stream()
                .map(SalesOrderLineJpaEntity::getItemId)
                .distinct()
                .map(itemRepository::findById)
                .flatMap(Optional::stream)
                .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));

        List<SalesOrderLineView> lineViews = new ArrayList<>();
        for (SalesOrderLineJpaEntity line : lines) {
            ItemJpaEntity item = items.get(line.getItemId());
            lineViews.add(new SalesOrderLineView(
                    line.getId(),
                    line.getLineNo(),
                    line.getItemId(),
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    item != null ? item.getPropertyClassification().name() : "",
                    line.getOrderQty(),
                    line.getUnitPrice(),
                    line.getAmount(),
                    line.getDeliveryDate(),
                    line.getFulfillmentRoute(),
                    line.getFulfillmentStatus(),
                    line.getDeliveryStatus()
            ));
        }

        return new SalesOrderView(
                order.getId(),
                order.getOrderNo(),
                order.getPartnerId(),
                partnerName,
                partnerBusinessRegNo,
                order.getOrderDate(),
                order.getRequestedDeliveryDate(),
                order.getStatus(),
                order.getRemark(),
                order.getConfirmedAt(),
                masterAuditActorLookup.nameOf(order.getConfirmedById()),
                lineViews
        );
    }

    private Optional<SalesOrderLineListView> toLineListView(
            SalesOrderLineJpaEntity line,
            SalesOrderJpaEntity order,
            Map<Long, CompanyJpaEntity> companies,
            Map<Long, ItemJpaEntity> items,
            boolean orderEditable
    ) {
        if (order == null || order.getStatus() == SalesOrderStatus.CANCELLED) {
            return Optional.empty();
        }
        CompanyJpaEntity partner = companies.get(order.getPartnerId());
        ItemJpaEntity item = items.get(line.getItemId());
        return Optional.of(new SalesOrderLineListView(
                order.getId(),
                line.getId(),
                order.getOrderNo(),
                order.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                partner != null ? partner.getBusinessRegNo() : "",
                order.getOrderDate(),
                order.getRequestedDeliveryDate(),
                line.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                line.getUnitPrice(),
                line.getOrderQty(),
                line.getAmount(),
                line.getFulfillmentRoute(),
                line.getFulfillmentStatus(),
                line.getDeliveryStatus(),
                order.getStatus(),
                orderEditable
        ));
    }

    private boolean matchesCriteria(SalesOrderLineListView view, SalesOrderLineListCriteria criteria) {
        if (criteria == null) {
            return true;
        }
        if (criteria.partnerId() != null && view.partnerId() != criteria.partnerId()) {
            return false;
        }
        if (criteria.itemId() != null && view.itemId() != criteria.itemId()) {
            return false;
        }
        if (criteria.requestedDeliveryDateFrom() != null
                && (view.requestedDeliveryDate() == null
                || view.requestedDeliveryDate().isBefore(criteria.requestedDeliveryDateFrom()))) {
            return false;
        }
        if (criteria.requestedDeliveryDateTo() != null
                && (view.requestedDeliveryDate() == null
                || view.requestedDeliveryDate().isAfter(criteria.requestedDeliveryDateTo()))) {
            return false;
        }
        if (criteria.fulfillmentStatus() != null && view.fulfillmentStatus() != criteria.fulfillmentStatus()) {
            return false;
        }
        if (criteria.deliveryStatus() != null && view.deliveryStatus() != criteria.deliveryStatus()) {
            return false;
        }
        return true;
    }

    private SalesOrderLineJpaEntity requireActiveLine(long lineId) {
        return lineRepository.findByIdAndRecordingState(lineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("수주 라인을 찾을 수 없습니다: " + lineId));
    }
}
