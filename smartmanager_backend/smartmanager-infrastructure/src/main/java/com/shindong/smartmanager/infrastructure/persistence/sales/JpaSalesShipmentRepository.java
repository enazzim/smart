package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.sales.SalesOrderLineShipmentContext;
import com.shindong.smartmanager.application.sales.SalesShipmentCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesShipmentCandidateView;
import com.shindong.smartmanager.application.sales.SalesShipmentInventoryService;
import com.shindong.smartmanager.application.sales.SalesShipmentLineSaveCommand;
import com.shindong.smartmanager.application.sales.SalesShipmentLineView;
import com.shindong.smartmanager.application.sales.SalesShipmentListCriteria;
import com.shindong.smartmanager.application.sales.SalesShipmentRepository;
import com.shindong.smartmanager.application.sales.SalesShipmentSaveCommand;
import com.shindong.smartmanager.application.sales.SalesShipmentView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import com.shindong.smartmanager.domain.sales.SalesShipmentStatus;
import com.shindong.smartmanager.domain.inventory.InventoryLocationLabels;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.sql.Date;
import java.time.Instant;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaSalesShipmentRepository implements SalesShipmentRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataSalesShipmentRepository shipmentRepository;
    private final SpringDataSalesShipmentLineRepository shipmentLineRepository;
    private final SpringDataSalesOrderRepository orderRepository;
    private final SpringDataSalesOrderLineRepository orderLineRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final InventoryBalanceService inventoryBalanceService;
    private final SalesShipmentInventoryService salesShipmentInventoryService;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaSalesShipmentRepository(
            EntityManager entityManager,
            SpringDataSalesShipmentRepository shipmentRepository,
            SpringDataSalesShipmentLineRepository shipmentLineRepository,
            SpringDataSalesOrderRepository orderRepository,
            SpringDataSalesOrderLineRepository orderLineRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            InventoryBalanceService inventoryBalanceService,
            SalesShipmentInventoryService salesShipmentInventoryService,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.entityManager = entityManager;
        this.shipmentRepository = shipmentRepository;
        this.shipmentLineRepository = shipmentLineRepository;
        this.orderRepository = orderRepository;
        this.orderLineRepository = orderLineRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.inventoryBalanceService = inventoryBalanceService;
        this.salesShipmentInventoryService = salesShipmentInventoryService;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    public long countByShipmentNoPrefix(String prefix) {
        return shipmentRepository.countByShipmentNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional(readOnly = true)
    public List<SalesShipmentCandidateView> findCandidates(SalesShipmentCandidateCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT so.id, sol.id, so.order_no, so.order_date, so.partner_id, c.company_name,
                       sol.item_id, i.item_no, i.item_name, i.property_classification,
                       sol.order_qty, sol.shipped_qty, sol.delivery_date, i.lot_tracked
                FROM sales_order_line sol
                JOIN sales_order so ON so.id = sol.sales_order_id
                JOIN company c ON c.id = so.partner_id
                JOIN item i ON i.id = sol.item_id
                WHERE sol.recording_state = 1 AND so.recording_state = 1
                  AND so.status <> 'CANCELLED'
                  AND (sol.order_qty - sol.shipped_qty) > 0
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
                sql.append(" AND c.company_name LIKE :partnerName");
                params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
            }
            if (criteria.orderNo() != null && !criteria.orderNo().isBlank()) {
                sql.append(" AND so.order_no LIKE :orderNo");
                params.put("orderNo", "%" + criteria.orderNo().trim() + "%");
            }
            if (criteria.orderDateFrom() != null) {
                sql.append(" AND so.order_date >= :orderDateFrom");
                params.put("orderDateFrom", criteria.orderDateFrom());
            }
            if (criteria.orderDateTo() != null) {
                sql.append(" AND so.order_date <= :orderDateTo");
                params.put("orderDateTo", criteria.orderDateTo());
            }
            if (criteria.itemNum() != null && !criteria.itemNum().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNum");
                params.put("itemNum", "%" + criteria.itemNum().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        sql.append(" ORDER BY so.order_date DESC, so.order_no, sol.line_no");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<SalesShipmentCandidateView> result = new ArrayList<>();
        LocalDate today = LocalDate.now();
        for (Object[] row : rows) {
            BigDecimal orderQty = toBigDecimal(row[10]);
            BigDecimal shippedQty = toBigDecimal(row[11]);
            BigDecimal remainingQty = orderQty.subtract(shippedQty);
            long itemId = ((Number) row[6]).longValue();
            String itemNo = row[7].toString();
            PropertyClassification propertyClassification = PropertyClassification.valueOf(row[9].toString());
            BigDecimal salesOnHand = inventoryBalanceService.currentStockQty(itemId, "SALES", today, null, null, null);
            BigDecimal wipOnHand = propertyClassification.shipmentFromWipFinalProcess()
                    ? salesShipmentInventoryService.resolveWipFinalOnHandQty(today, itemId)
                    : BigDecimal.ZERO;
            BigDecimal availableQty = salesShipmentInventoryService.resolveShipmentAvailableQty(
                    today, itemId, propertyClassification);
            boolean shippable = remainingQty.compareTo(BigDecimal.ZERO) > 0;
            String message = null;
            if (shippable && availableQty.compareTo(remainingQty) < 0) {
                if (propertyClassification.shipmentFromWipFinalProcess()) {
                    message = InventoryLocationLabels.label("WIP") + "(최종공정) 재고("
                            + wipOnHand.stripTrailingZeros().toPlainString()
                            + ")가 출고 잔량보다 적을 수 있습니다.";
                } else {
                    message = InventoryLocationLabels.label("SALES") + " 재고(" + salesOnHand.stripTrailingZeros().toPlainString()
                            + ")가 출고 잔량보다 적을 수 있습니다.";
                }
            }
            boolean lotTracked = toBooleanFlag(row[13]);
            String lotLocationCode = propertyClassification.shipmentFromWipFinalProcess() ? "WIP" : "SALES";
            Long finalProcessId = propertyClassification.shipmentFromWipFinalProcess()
                    ? salesShipmentInventoryService.resolveFinalProcessId(itemId, propertyClassification)
                    : null;
            result.add(new SalesShipmentCandidateView(
                    ((Number) row[1]).longValue(),
                    ((Number) row[0]).longValue(),
                    row[2].toString(),
                    toLocalDate(row[3]),
                    ((Number) row[4]).longValue(),
                    row[5].toString(),
                    itemId,
                    itemNo,
                    row[8].toString(),
                    orderQty,
                    shippedQty,
                    remainingQty,
                    salesOnHand,
                    wipOnHand,
                    shippable,
                    message,
                    lotTracked,
                    lotLocationCode,
                    finalProcessId
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public SalesOrderLineShipmentContext findOrderLineContext(long salesOrderLineId) {
        SalesOrderLineJpaEntity line = requireActiveLine(salesOrderLineId);
        SalesOrderJpaEntity order = requireActiveOrder(line.getSalesOrderId());
        CompanyJpaEntity partner = companyRepository.findById(order.getPartnerId()).orElse(null);
        ItemJpaEntity item = itemRepository.findById(line.getItemId()).orElse(null);
        return new SalesOrderLineShipmentContext(
                order.getId(),
                line.getId(),
                order.getOrderNo(),
                order.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                order.getStatus(),
                line.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                item != null ? item.getPropertyClassification() : null,
                line.getOrderQty(),
                line.getShippedQty(),
                line.getUnitPrice(),
                line.getDeliveryDate()
        );
    }

    @Override
    @Transactional
    public SalesShipmentView save(SalesShipmentSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        SalesShipmentJpaEntity header = new SalesShipmentJpaEntity();
        header.setShipmentNo(command.shipmentNo());
        header.setPartnerId(command.partnerId());
        header.setShipmentDate(command.shipmentDate());
        header.setSalesOrderId(command.salesOrderId());
        header.setStatus(SalesShipmentStatus.ISSUED);
        header.setRecordingState(ACTIVE);
        header.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setCreatedAt(now);
        header.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setUpdatedAt(now);
        SalesShipmentJpaEntity savedHeader = shipmentRepository.save(header);

        short lineNo = 1;
        for (SalesShipmentLineSaveCommand lineCommand : command.lines()) {
            SalesShipmentLineJpaEntity line = new SalesShipmentLineJpaEntity();
            line.setSalesShipmentId(savedHeader.getId());
            line.setLineNo(lineNo++);
            line.setSalesOrderLineId(lineCommand.salesOrderLineId());
            line.setItemId(lineCommand.itemId());
            line.setShipmentQty(lineCommand.shipmentQty());
            line.setUnitPrice(lineCommand.unitPrice());
            line.setAmount(lineCommand.amount());
            line.setLotId(lineCommand.lotId());
            line.setRecordingState(ACTIVE);
            line.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
            line.setCreatedAt(now);
            line.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            line.setUpdatedAt(now);
            shipmentLineRepository.save(line);
        }

        return findActiveIssuedById(savedHeader.getId())
                .orElseThrow(() -> new IllegalStateException("저장된 출고·납품을 찾을 수 없습니다."));
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        SalesShipmentJpaEntity entity = shipmentRepository
                .findByIdAndRecordingStateAndStatus(id, ACTIVE, SalesShipmentStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("출고·납품을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setStatus(SalesShipmentStatus.CANCELLED);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        shipmentRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public List<SalesShipmentView> findAllActive(SalesShipmentListCriteria criteria) {
        List<SalesShipmentJpaEntity> rows;
        if (criteria == null) {
            rows = shipmentRepository.findByRecordingStateOrderByShipmentDateDescIdDesc(ACTIVE);
        } else {
            rows = shipmentRepository.searchActive(
                    ACTIVE,
                    criteria.shipmentDateFrom(),
                    criteria.shipmentDateTo(),
                    normalize(criteria.shipmentNo()),
                    criteria.status(),
                    criteria.excludeCancelled(),
                    SalesShipmentStatus.CANCELLED
            );
        }
        return rows.stream()
                .map(this::toView)
                .filter(view -> matchesPartnerName(view, criteria))
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<SalesShipmentView> findActiveIssuedById(long id) {
        return shipmentRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, SalesShipmentStatus.ISSUED)
                .map(this::toView);
    }

    @Override
    @Transactional
    public void addShippedQty(long salesOrderLineId, BigDecimal qty, String actorUserId) {
        SalesOrderLineJpaEntity line = requireActiveLine(salesOrderLineId);
        line.setShippedQty(line.getShippedQty().add(qty));
        orderLineRepository.save(line);
    }

    @Override
    @Transactional
    public void subtractShippedQty(long salesOrderLineId, BigDecimal qty, String actorUserId) {
        SalesOrderLineJpaEntity line = requireActiveLine(salesOrderLineId);
        line.setShippedQty(line.getShippedQty().subtract(qty));
        orderLineRepository.save(line);
    }

    @Override
    @Transactional
    public void refreshLineDeliveryStatus(long salesOrderLineId, String actorUserId) {
        SalesOrderLineJpaEntity line = requireActiveLine(salesOrderLineId);
        BigDecimal shipped = line.getShippedQty();
        BigDecimal orderQty = line.getOrderQty();
        SalesLineDeliveryStatus status;
        if (shipped.compareTo(BigDecimal.ZERO) <= 0) {
            status = SalesLineDeliveryStatus.NOT_STARTED;
        } else if (shipped.compareTo(orderQty) >= 0) {
            status = SalesLineDeliveryStatus.COMPLETED;
        } else {
            status = SalesLineDeliveryStatus.IN_PROGRESS;
        }
        line.setDeliveryStatus(status);
        orderLineRepository.save(line);

        SalesOrderJpaEntity order = requireActiveOrder(line.getSalesOrderId());
        Instant now = Instant.now();
        order.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        order.setUpdatedAt(now);
        orderRepository.save(order);
    }

    private SalesShipmentView toView(SalesShipmentJpaEntity entity) {
        CompanyJpaEntity partner = companyRepository.findById(entity.getPartnerId()).orElse(null);
        List<SalesShipmentLineJpaEntity> lineEntities =
                shipmentLineRepository.findBySalesShipmentIdAndRecordingStateOrderByLineNoAsc(
                        entity.getId(), ACTIVE);
        List<SalesShipmentLineView> lines = new ArrayList<>();
        boolean hasInvoicedQty = false;
        for (SalesShipmentLineJpaEntity lineEntity : lineEntities) {
            if (lineEntity.getInvoicedQty().compareTo(BigDecimal.ZERO) > 0) {
                hasInvoicedQty = true;
            }
            SalesOrderLineJpaEntity orderLine = orderLineRepository
                    .findByIdAndRecordingState(lineEntity.getSalesOrderLineId(), ACTIVE)
                    .orElse(null);
            SalesOrderJpaEntity order = orderLine != null
                    ? orderRepository.findByIdAndRecordingState(orderLine.getSalesOrderId(), ACTIVE).orElse(null)
                    : null;
            ItemJpaEntity item = itemRepository.findById(lineEntity.getItemId()).orElse(null);
            lines.add(new SalesShipmentLineView(
                    lineEntity.getId(),
                    lineEntity.getLineNo(),
                    lineEntity.getSalesOrderLineId(),
                    order != null ? order.getOrderNo() : "",
                    entity.getPartnerId(),
                    partner != null ? partner.getCompanyName() : "",
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    lineEntity.getShipmentQty(),
                    lineEntity.getUnitPrice(),
                    lineEntity.getAmount(),
                    lineEntity.getLotId()
            ));
        }
        boolean cancelable = entity.getStatus() == SalesShipmentStatus.ISSUED && !hasInvoicedQty;
        return new SalesShipmentView(
                entity.getId(),
                entity.getShipmentNo(),
                entity.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                entity.getShipmentDate(),
                entity.getSalesOrderId(),
                entity.getStatus(),
                entity.getCreatedAt(),
                masterAuditActorLookup.nameOf(entity.getCreatedById()),
                cancelable,
                lines
        );
    }

    private boolean matchesPartnerName(SalesShipmentView view, SalesShipmentListCriteria criteria) {
        if (criteria == null || criteria.partnerName() == null || criteria.partnerName().isBlank()) {
            return true;
        }
        String needle = criteria.partnerName().trim().toLowerCase();
        return view.partnerName().toLowerCase().contains(needle);
    }

    private String normalize(String value) {
        if (value == null || value.isBlank()) {
            return null;
        }
        return value.trim();
    }

    private SalesOrderJpaEntity requireActiveOrder(long salesOrderId) {
        return orderRepository.findByIdAndRecordingState(salesOrderId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("수주를 찾을 수 없습니다: " + salesOrderId));
    }

    private SalesOrderLineJpaEntity requireActiveLine(long lineId) {
        return orderLineRepository.findByIdAndRecordingState(lineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("수주 라인을 찾을 수 없습니다: " + lineId));
    }

    private static boolean toBooleanFlag(Object value) {
        if (value == null) {
            return false;
        }
        if (value instanceof Boolean bool) {
            return bool;
        }
        if (value instanceof Number number) {
            return number.intValue() != 0;
        }
        String text = value.toString().trim();
        return "1".equals(text) || "true".equalsIgnoreCase(text) || "Y".equalsIgnoreCase(text);
    }

    private static BigDecimal toBigDecimal(Object value) {
        if (value == null) {
            return BigDecimal.ZERO;
        }
        if (value instanceof BigDecimal decimal) {
            return decimal;
        }
        return new BigDecimal(value.toString());
    }

    private static LocalDate toLocalDate(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof LocalDate date) {
            return date;
        }
        if (value instanceof Date sqlDate) {
            return sqlDate.toLocalDate();
        }
        return LocalDate.parse(value.toString());
    }
}
