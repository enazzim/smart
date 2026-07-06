package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderListCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderService;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderView;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.process.ProcessSequenceJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;
import com.shindong.smartmanager.infrastructure.persistence.production.ProductionPlanJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.production.SpringDataProductionPlanRepository;
import com.shindong.smartmanager.infrastructure.persistence.production.SpringDataWorkPlanRepository;
import com.shindong.smartmanager.infrastructure.persistence.production.WorkPlanJpaEntity;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaOutsourcingOrderRepository implements OutsourcingOrderRepository {

    private static final int ACTIVE = 1;

    private final SpringDataOutsourcingOrderRepository orderRepository;
    private final SpringDataOutsourcingOrderLineRepository lineRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataProcessSequenceRepository processRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final SpringDataProductionPlanRepository productionPlanRepository;
    private final SpringDataWorkPlanRepository workPlanRepository;
    private final ItemRepository itemLookup;

    public JpaOutsourcingOrderRepository(
            SpringDataOutsourcingOrderRepository orderRepository,
            SpringDataOutsourcingOrderLineRepository lineRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            SpringDataProcessSequenceRepository processRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            SpringDataProductionPlanRepository productionPlanRepository,
            SpringDataWorkPlanRepository workPlanRepository,
            ItemRepository itemLookup
    ) {
        this.orderRepository = orderRepository;
        this.lineRepository = lineRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.processRepository = processRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.workPlanRepository = workPlanRepository;
        this.itemLookup = itemLookup;
    }

    @Override
    public Map<Long, BigDecimal> sumOrderedQtyByWorkPlanIds(Collection<Long> workPlanIds) {
        if (workPlanIds == null || workPlanIds.isEmpty()) {
            return Map.of();
        }
        Map<Long, BigDecimal> totals = new HashMap<>();
        for (Object[] row : lineRepository.sumOrderedQtyGroupedByWorkPlanId(
                workPlanIds,
                ACTIVE,
                OutsourcingOrderStatus.CANCELLED
        )) {
            totals.put((Long) row[0], (BigDecimal) row[1]);
        }
        return totals;
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
    public long save(OutsourcingOrderCommand command, String orderNo, String actorUserId) {
        Instant now = Instant.now();
        OutsourcingOrderJpaEntity entity = new OutsourcingOrderJpaEntity();
        entity.setOrderNo(orderNo);
        entity.setPartnerId(command.partnerId());
        entity.setOrderDate(command.orderDate());
        entity.setSourceType(command.sourceType());
        entity.setStatus(OutsourcingOrderStatus.CONFIRMED);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        OutsourcingOrderJpaEntity saved = orderRepository.save(entity);
        insertLines(saved.getId(), command, actorUserId, now);
        return saved.getId();
    }

    @Override
    @Transactional
    public void updateStatus(long outsourcingOrderId, OutsourcingOrderStatus status, String actorUserId) {
        OutsourcingOrderJpaEntity entity = requireActiveOrder(outsourcingOrderId);
        Instant now = Instant.now();
        entity.setStatus(status);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        orderRepository.save(entity);
    }

    @Override
    public List<OutsourcingOrderView> findAllActive() {
        return orderRepository.findByRecordingStateOrderByOrderDateDescIdDesc(ACTIVE).stream()
                .map(order -> toView(order, loadLines(order.getId())))
                .toList();
    }

    @Override
    public List<OutsourcingOrderView> findAllActive(OutsourcingOrderListCriteria criteria) {
        if (criteria == null) {
            return findAllActive();
        }
        String partnerName = normalizeQuery(criteria.partnerName());
        String orderNo = normalizeQuery(criteria.orderNo());
        return orderRepository.searchActive(
                ACTIVE,
                criteria.orderDateFrom(),
                criteria.orderDateTo(),
                partnerName,
                orderNo,
                criteria.status(),
                criteria.excludeCancelled(),
                OutsourcingOrderStatus.CANCELLED
        ).stream()
                .map(order -> toView(order, loadLines(order.getId())))
                .toList();
    }

    @Override
    public Optional<OutsourcingOrderView> findActiveById(long id) {
        return orderRepository.findByIdAndRecordingState(id, ACTIVE)
                .map(order -> toView(order, loadLines(order.getId())));
    }

    private String normalizeQuery(String value) {
        if (value == null) {
            return null;
        }
        String trimmed = value.trim();
        return trimmed.isEmpty() ? null : trimmed;
    }

    @Override
    public Optional<OutsourcingOrderLineView> findActiveLineById(long orderLineId) {
        return lineRepository.findByIdAndRecordingState(orderLineId, ACTIVE)
                .map(line -> toLineView(line, loadLineContext(List.of(line))));
    }

    @Override
    public Optional<OutsourcingOrderView> findActiveByOrderLineId(long orderLineId) {
        return lineRepository.findByIdAndRecordingState(orderLineId, ACTIVE)
                .flatMap(line -> findActiveById(line.getOutsourcingOrderId()));
    }

    @Override
    @Transactional
    public void addShippedQty(long orderLineId, BigDecimal qty, String actorUserId) {
        OutsourcingOrderLineJpaEntity line = requireActiveLine(orderLineId);
        line.setShippedQty(line.getShippedQty().add(qty));
        touchLine(line, actorUserId);
    }

    @Override
    @Transactional
    public void subtractShippedQty(long orderLineId, BigDecimal qty, String actorUserId) {
        OutsourcingOrderLineJpaEntity line = requireActiveLine(orderLineId);
        BigDecimal next = line.getShippedQty().subtract(qty);
        if (next.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalStateException("출고 수량이 음수가 됩니다.");
        }
        line.setShippedQty(next);
        touchLine(line, actorUserId);
    }

    @Override
    @Transactional
    public void addReceivedQty(long orderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(orderLineId, qty, true, false, actorUserId);
    }

    @Override
    @Transactional
    public void subtractReceivedQty(long orderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(orderLineId, qty.negate(), true, false, actorUserId);
    }

    @Override
    @Transactional
    public void addWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(orderLineId, qty, false, true, actorUserId);
    }

    @Override
    @Transactional
    public void releaseWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(orderLineId, qty.negate(), false, true, actorUserId);
    }

    @Override
    @Transactional
    public void refreshOrderStatus(long orderId, String actorUserId) {
        OutsourcingOrderJpaEntity order = requireActiveOrder(orderId);
        List<OutsourcingOrderLineJpaEntity> lines = loadLines(orderId);
        OutsourcingOrderStatus next = resolveOrderStatus(lines);
        if (order.getStatus() != next && order.getStatus() != OutsourcingOrderStatus.CANCELLED) {
            order.setStatus(next);
            Instant now = Instant.now();
            order.setUpdatedBy(actorUserId);
            order.setUpdatedById(actorUserId);
            order.setUpdatedAt(now);
            orderRepository.save(order);
        }
    }

    private OutsourcingOrderStatus resolveOrderStatus(List<OutsourcingOrderLineJpaEntity> lines) {
        boolean allReceived = !lines.isEmpty() && lines.stream().allMatch(line ->
                line.getReceivedQty().compareTo(line.getOrderQty()) >= 0);
        if (allReceived) {
            return OutsourcingOrderStatus.RECEIVED;
        }
        boolean anyShipped = lines.stream().anyMatch(line ->
                line.getShippedQty().compareTo(BigDecimal.ZERO) > 0);
        if (anyShipped) {
            return OutsourcingOrderStatus.IN_PROGRESS;
        }
        return OutsourcingOrderStatus.CONFIRMED;
    }

    private OutsourcingOrderLineJpaEntity requireActiveLine(long orderLineId) {
        return lineRepository.findByIdAndRecordingState(orderLineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("외주발주 라인을 찾을 수 없습니다: " + orderLineId));
    }

    private void touchLine(OutsourcingOrderLineJpaEntity line, String actorUserId) {
        Instant now = Instant.now();
        line.setUpdatedBy(actorUserId);
        line.setUpdatedById(actorUserId);
        line.setUpdatedAt(now);
        lineRepository.save(line);
    }

    private void updateOrderLineQty(
            long orderLineId,
            BigDecimal delta,
            boolean received,
            boolean waiting,
            String actorUserId
    ) {
        OutsourcingOrderLineJpaEntity line = requireActiveLine(orderLineId);
        if (received) {
            line.setReceivedQty(line.getReceivedQty().add(delta));
        }
        if (waiting) {
            line.setWaitingInspectionQty(line.getWaitingInspectionQty().add(delta));
        }
        touchLine(line, actorUserId);
    }

    private OutsourcingOrderLineView toLineView(
            OutsourcingOrderLineJpaEntity line,
            LineContext context
    ) {
        ItemJpaEntity item = context.items().get(line.getItemId());
        ProcessSequenceJpaEntity process = context.processes().get(line.getProcessSequenceId());
        PublicCodeJpaEntity beginCode = context.codes().get(line.getBeginProcessCodeId());
        PublicCodeJpaEntity endCode = context.codes().get(line.getEndProcessCodeId());
        PublicCodeJpaEntity processCode = process != null ? context.codes().get(process.getPublicCodeId()) : null;
        return new OutsourcingOrderLineView(
                line.getId(),
                line.getLineNo(),
                line.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                item != null && item.getPropertyClassification() != null
                        ? item.getPropertyClassification().name() : "",
                line.getProcessSequenceId(),
                process != null ? process.getProcessSequenceNum() : 0,
                processCode != null ? processCode.getSmallCode() : "",
                processCode != null ? processCode.getSmallName() : "",
                line.getBeginProcessCodeId(),
                beginCode != null ? beginCode.getSmallCode() : "",
                beginCode != null ? beginCode.getSmallName() : "",
                line.getEndProcessCodeId(),
                endCode != null ? endCode.getSmallCode() : "",
                endCode != null ? endCode.getSmallName() : "",
                line.getWorkPlanId(),
                line.getWorkPlanId() != null ? context.planNos().get(line.getWorkPlanId()) : null,
                line.getOrderQty(),
                line.getShippedQty(),
                line.getReceivedQty(),
                line.getUnitPrice(),
                line.getAmount(),
                line.getRequestedDeliveryDate()
        );
    }

    private LineContext loadLineContext(List<OutsourcingOrderLineJpaEntity> lines) {
        Map<Long, ItemJpaEntity> items = lines.stream()
                .map(OutsourcingOrderLineJpaEntity::getItemId)
                .distinct()
                .map(itemRepository::findById)
                .flatMap(Optional::stream)
                .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));

        Map<Long, ProcessSequenceJpaEntity> processes = lines.stream()
                .map(OutsourcingOrderLineJpaEntity::getProcessSequenceId)
                .distinct()
                .map(processRepository::findById)
                .flatMap(Optional::stream)
                .collect(Collectors.toMap(ProcessSequenceJpaEntity::getId, Function.identity()));

        Set<Long> codeIds = lines.stream()
                .flatMap(line -> java.util.stream.Stream.of(line.getBeginProcessCodeId(), line.getEndProcessCodeId()))
                .collect(Collectors.toSet());
        for (ProcessSequenceJpaEntity process : processes.values()) {
            codeIds.add(process.getPublicCodeId());
        }
        Map<Long, PublicCodeJpaEntity> codes = publicCodeRepository.findAllById(codeIds).stream()
                .collect(Collectors.toMap(PublicCodeJpaEntity::getId, Function.identity()));

        return new LineContext(items, processes, codes, loadPlanNos(lines));
    }

    private record LineContext(
            Map<Long, ItemJpaEntity> items,
            Map<Long, ProcessSequenceJpaEntity> processes,
            Map<Long, PublicCodeJpaEntity> codes,
            Map<Long, String> planNos
    ) {
    }

    private OutsourcingOrderJpaEntity requireActiveOrder(long outsourcingOrderId) {
        return orderRepository.findByIdAndRecordingState(outsourcingOrderId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("외주발주를 찾을 수 없습니다: " + outsourcingOrderId));
    }

    private List<OutsourcingOrderLineJpaEntity> loadLines(long outsourcingOrderId) {
        return lineRepository.findByOutsourcingOrderIdAndRecordingStateOrderByLineNoAsc(outsourcingOrderId, ACTIVE);
    }

    private void insertLines(long outsourcingOrderId, OutsourcingOrderCommand command, String actorUserId, Instant now) {
        short lineNo = 1;
        for (OutsourcingOrderLineCommand line : command.lines()) {
            itemLookup.findActiveById(line.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + line.itemId()));

            OutsourcingOrderLineJpaEntity entity = new OutsourcingOrderLineJpaEntity();
            entity.setOutsourcingOrderId(outsourcingOrderId);
            entity.setLineNo(lineNo++);
            entity.setItemId(line.itemId());
            entity.setProcessSequenceId(line.processSequenceId());
            entity.setBeginProcessCodeId(line.beginProcessCodeId());
            entity.setEndProcessCodeId(line.endProcessCodeId());
            entity.setWorkPlanId(line.workPlanId());
            entity.setOrderQty(line.orderQty());
            entity.setUnitPrice(line.unitPrice() != null ? line.unitPrice() : BigDecimal.ZERO);
            entity.setAmount(OutsourcingOrderService.lineAmount(line.orderQty(), line.unitPrice()));
            entity.setRequestedDeliveryDate(line.requestedDeliveryDate());
            entity.setRecordingState(ACTIVE);
            entity.setCreatedBy(actorUserId);
            entity.setCreatedById(actorUserId);
            entity.setCreatedAt(now);
            entity.setUpdatedBy(actorUserId);
            entity.setUpdatedById(actorUserId);
            entity.setUpdatedAt(now);
            lineRepository.save(entity);
        }
    }

    private OutsourcingOrderView toView(OutsourcingOrderJpaEntity order, List<OutsourcingOrderLineJpaEntity> lines) {
        CompanyJpaEntity partner = companyRepository.findById(order.getPartnerId()).orElse(null);
        String partnerName = partner != null ? partner.getCompanyName() : "";
        String partnerBusinessRegNo = partner != null ? partner.getBusinessRegNo() : "";

        Map<Long, ItemJpaEntity> items = lines.stream()
                .map(OutsourcingOrderLineJpaEntity::getItemId)
                .distinct()
                .map(itemRepository::findById)
                .flatMap(Optional::stream)
                .collect(Collectors.toMap(ItemJpaEntity::getId, Function.identity()));

        Map<Long, ProcessSequenceJpaEntity> processes = lines.stream()
                .map(OutsourcingOrderLineJpaEntity::getProcessSequenceId)
                .distinct()
                .map(processRepository::findById)
                .flatMap(Optional::stream)
                .collect(Collectors.toMap(ProcessSequenceJpaEntity::getId, Function.identity()));

        Set<Long> codeIds = lines.stream()
                .flatMap(line -> java.util.stream.Stream.of(line.getBeginProcessCodeId(), line.getEndProcessCodeId()))
                .collect(Collectors.toSet());
        for (ProcessSequenceJpaEntity process : processes.values()) {
            codeIds.add(process.getPublicCodeId());
        }
        Map<Long, PublicCodeJpaEntity> codes = publicCodeRepository.findAllById(codeIds).stream()
                .collect(Collectors.toMap(PublicCodeJpaEntity::getId, Function.identity()));

        Map<Long, String> planNoByWorkPlanId = loadPlanNos(lines);

        List<OutsourcingOrderLineView> lineViews = new ArrayList<>();
        for (OutsourcingOrderLineJpaEntity line : lines) {
            ItemJpaEntity item = items.get(line.getItemId());
            ProcessSequenceJpaEntity process = processes.get(line.getProcessSequenceId());
            PublicCodeJpaEntity beginCode = codes.get(line.getBeginProcessCodeId());
            PublicCodeJpaEntity endCode = codes.get(line.getEndProcessCodeId());
            PublicCodeJpaEntity processCode = process != null ? codes.get(process.getPublicCodeId()) : null;
            lineViews.add(new OutsourcingOrderLineView(
                    line.getId(),
                    line.getLineNo(),
                    line.getItemId(),
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    item != null && item.getPropertyClassification() != null
                            ? item.getPropertyClassification().name() : "",
                    line.getProcessSequenceId(),
                    process != null ? process.getProcessSequenceNum() : 0,
                    processCode != null ? processCode.getSmallCode() : "",
                    processCode != null ? processCode.getSmallName() : "",
                    line.getBeginProcessCodeId(),
                    beginCode != null ? beginCode.getSmallCode() : "",
                    beginCode != null ? beginCode.getSmallName() : "",
                    line.getEndProcessCodeId(),
                    endCode != null ? endCode.getSmallCode() : "",
                    endCode != null ? endCode.getSmallName() : "",
                    line.getWorkPlanId(),
                    line.getWorkPlanId() != null ? planNoByWorkPlanId.get(line.getWorkPlanId()) : null,
                    line.getOrderQty(),
                    line.getShippedQty(),
                    line.getReceivedQty(),
                    line.getUnitPrice(),
                    line.getAmount(),
                    line.getRequestedDeliveryDate()
            ));
        }

        return new OutsourcingOrderView(
                order.getId(),
                order.getOrderNo(),
                order.getPartnerId(),
                partnerName,
                partnerBusinessRegNo,
                order.getOrderDate(),
                order.getSourceType(),
                order.getStatus(),
                order.getCreatedAt(),
                order.getCreatedBy(),
                lineViews
        );
    }

    private Map<Long, String> loadPlanNos(List<OutsourcingOrderLineJpaEntity> lines) {
        Set<Long> workPlanIds = lines.stream()
                .map(OutsourcingOrderLineJpaEntity::getWorkPlanId)
                .filter(id -> id != null && id > 0)
                .collect(Collectors.toSet());
        if (workPlanIds.isEmpty()) {
            return Map.of();
        }
        Map<Long, String> planNos = new HashMap<>();
        for (WorkPlanJpaEntity workPlan : workPlanRepository.findAllById(workPlanIds)) {
            productionPlanRepository.findById(workPlan.getProductionPlanId())
                    .map(ProductionPlanJpaEntity::getPlanNo)
                    .ifPresent(planNo -> planNos.put(workPlan.getId(), planNo));
        }
        return planNos;
    }
}
