package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.sales.SalesRevenueCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesRevenueCandidateView;
import com.shindong.smartmanager.application.sales.SalesRevenueLineSaveCommand;
import com.shindong.smartmanager.application.sales.SalesRevenueLineView;
import com.shindong.smartmanager.application.sales.SalesRevenueListCriteria;
import com.shindong.smartmanager.application.sales.SalesRevenueRepository;
import com.shindong.smartmanager.application.sales.SalesRevenueSaveCommand;
import com.shindong.smartmanager.application.sales.SalesRevenueView;
import com.shindong.smartmanager.application.sales.SalesShipmentLineRevenueContext;
import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
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
public class JpaSalesRevenueRepository implements SalesRevenueRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataSalesRevenueRepository revenueRepository;
    private final SpringDataSalesRevenueLineRepository revenueLineRepository;
    private final SpringDataSalesShipmentRepository shipmentRepository;
    private final SpringDataSalesShipmentLineRepository shipmentLineRepository;
    private final SpringDataSalesOrderRepository orderRepository;
    private final SpringDataSalesOrderLineRepository orderLineRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final InventoryBalanceService inventoryBalanceService;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaSalesRevenueRepository(
            EntityManager entityManager,
            SpringDataSalesRevenueRepository revenueRepository,
            SpringDataSalesRevenueLineRepository revenueLineRepository,
            SpringDataSalesShipmentRepository shipmentRepository,
            SpringDataSalesShipmentLineRepository shipmentLineRepository,
            SpringDataSalesOrderRepository orderRepository,
            SpringDataSalesOrderLineRepository orderLineRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            InventoryBalanceService inventoryBalanceService,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.entityManager = entityManager;
        this.revenueRepository = revenueRepository;
        this.revenueLineRepository = revenueLineRepository;
        this.shipmentRepository = shipmentRepository;
        this.shipmentLineRepository = shipmentLineRepository;
        this.orderRepository = orderRepository;
        this.orderLineRepository = orderLineRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.inventoryBalanceService = inventoryBalanceService;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    public long countByRevenueNoPrefix(String prefix) {
        return revenueRepository.countByRevenueNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional(readOnly = true)
    public List<SalesRevenueCandidateView> findCandidates(SalesRevenueCandidateCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT ss.id, shl.id, ss.shipment_no, ss.shipment_date, ss.partner_id, c.company_name,
                       shl.sales_order_line_id, so.order_no, shl.item_id, i.item_no, i.item_name,
                       shl.shipment_qty, shl.invoiced_qty, shl.unit_price, i.lot_tracked, shl.lot_id
                FROM sales_shipment_line shl
                JOIN sales_shipment ss ON ss.id = shl.sales_shipment_id
                JOIN company c ON c.id = ss.partner_id
                JOIN sales_order_line sol ON sol.id = shl.sales_order_line_id
                JOIN sales_order so ON so.id = sol.sales_order_id
                JOIN item i ON i.id = shl.item_id
                WHERE shl.recording_state = 1 AND ss.recording_state = 1
                  AND ss.status = 'ISSUED'
                  AND (shl.shipment_qty - shl.invoiced_qty) > 0
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
                sql.append(" AND c.company_name LIKE :partnerName");
                params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
            }
            if (criteria.shipmentNo() != null && !criteria.shipmentNo().isBlank()) {
                sql.append(" AND ss.shipment_no LIKE :shipmentNo");
                params.put("shipmentNo", "%" + criteria.shipmentNo().trim() + "%");
            }
            if (criteria.shipmentDateFrom() != null) {
                sql.append(" AND ss.shipment_date >= :shipmentDateFrom");
                params.put("shipmentDateFrom", criteria.shipmentDateFrom());
            }
            if (criteria.shipmentDateTo() != null) {
                sql.append(" AND ss.shipment_date <= :shipmentDateTo");
                params.put("shipmentDateTo", criteria.shipmentDateTo());
            }
            if (criteria.orderNo() != null && !criteria.orderNo().isBlank()) {
                sql.append(" AND so.order_no LIKE :orderNo");
                params.put("orderNo", "%" + criteria.orderNo().trim() + "%");
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
        sql.append(" ORDER BY ss.shipment_date DESC, ss.shipment_no, shl.line_no");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<SalesRevenueCandidateView> result = new ArrayList<>();
        LocalDate today = LocalDate.now();
        for (Object[] row : rows) {
            BigDecimal shippedQty = toBigDecimal(row[11]);
            BigDecimal invoicedQty = toBigDecimal(row[12]);
            BigDecimal remainingQty = shippedQty.subtract(invoicedQty);
            long itemId = ((Number) row[8]).longValue();
            String itemNo = row[9].toString();
            BigDecimal unitPrice = toBigDecimal(row[13]);
            BigDecimal deliveryOnHand = inventoryBalanceService.currentStockQty(
                    itemId, "DELIVERY", today, null, null, null);
            boolean billable = remainingQty.compareTo(BigDecimal.ZERO) > 0;
            String message = null;
            if (billable && deliveryOnHand.compareTo(remainingQty) < 0) {
                message = InventoryLocationLabels.label("DELIVERY") + " 재고(" + deliveryOnHand.stripTrailingZeros().toPlainString()
                        + ")가 매출 잔량보다 적을 수 있습니다.";
            }
            result.add(new SalesRevenueCandidateView(
                    ((Number) row[1]).longValue(),
                    ((Number) row[0]).longValue(),
                    row[2].toString(),
                    toLocalDate(row[3]),
                    ((Number) row[4]).longValue(),
                    row[5].toString(),
                    ((Number) row[6]).longValue(),
                    row[7].toString(),
                    itemId,
                    itemNo,
                    row[10].toString(),
                    shippedQty,
                    invoicedQty,
                    remainingQty,
                    deliveryOnHand,
                    unitPrice,
                    billable,
                    message,
                    toBooleanFlag(row[14]),
                    row[15] != null ? ((Number) row[15]).longValue() : null
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public SalesShipmentLineRevenueContext findShipmentLineContext(long salesShipmentLineId) {
        SalesShipmentLineJpaEntity line = requireActiveShipmentLine(salesShipmentLineId);
        SalesShipmentJpaEntity shipment = requireActiveShipment(line.getSalesShipmentId());
        if (shipment.getStatus() != SalesShipmentStatus.ISSUED) {
            throw new IllegalArgumentException("출고·납품 상태가 유효하지 않습니다: " + salesShipmentLineId);
        }
        SalesOrderLineJpaEntity orderLine = orderLineRepository
                .findByIdAndRecordingState(line.getSalesOrderLineId(), ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("수주 라인을 찾을 수 없습니다: " + line.getSalesOrderLineId()));
        SalesOrderJpaEntity order = orderRepository
                .findByIdAndRecordingState(orderLine.getSalesOrderId(), ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("수주를 찾을 수 없습니다: " + orderLine.getSalesOrderId()));
        CompanyJpaEntity partner = companyRepository.findById(shipment.getPartnerId()).orElse(null);
        ItemJpaEntity item = itemRepository.findById(line.getItemId()).orElse(null);
        return new SalesShipmentLineRevenueContext(
                shipment.getId(),
                line.getId(),
                shipment.getShipmentNo(),
                shipment.getShipmentDate(),
                shipment.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                line.getSalesOrderLineId(),
                order.getOrderNo(),
                line.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                line.getShipmentQty(),
                line.getInvoicedQty(),
                line.getUnitPrice()
        );
    }

    @Override
    @Transactional
    public SalesRevenueView save(SalesRevenueSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        SalesRevenueJpaEntity header = new SalesRevenueJpaEntity();
        header.setRevenueNo(command.revenueNo());
        header.setPartnerId(command.partnerId());
        header.setRevenueDate(command.revenueDate());
        header.setSalesShipmentId(command.salesShipmentId());
        header.setStatus(SalesRevenueStatus.ISSUED);
        header.setRecordingState(ACTIVE);
        header.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setCreatedAt(now);
        header.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setUpdatedAt(now);
        SalesRevenueJpaEntity savedHeader = revenueRepository.save(header);

        short lineNo = 1;
        for (SalesRevenueLineSaveCommand lineCommand : command.lines()) {
            SalesRevenueLineJpaEntity line = new SalesRevenueLineJpaEntity();
            line.setSalesRevenueId(savedHeader.getId());
            line.setLineNo(lineNo++);
            line.setSalesShipmentLineId(lineCommand.salesShipmentLineId());
            line.setItemId(lineCommand.itemId());
            line.setRevenueQty(lineCommand.revenueQty());
            line.setUnitPrice(lineCommand.unitPrice());
            line.setAmount(lineCommand.amount());
            line.setLotId(lineCommand.lotId());
            line.setRecordingState(ACTIVE);
            line.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
            line.setCreatedAt(now);
            line.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            line.setUpdatedAt(now);
            revenueLineRepository.save(line);
        }

        return findActiveIssuedById(savedHeader.getId())
                .orElseThrow(() -> new IllegalStateException("저장된 매출을 찾을 수 없습니다."));
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        SalesRevenueJpaEntity entity = revenueRepository
                .findByIdAndRecordingStateAndStatus(id, ACTIVE, SalesRevenueStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("매출을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setStatus(SalesRevenueStatus.CANCELLED);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        revenueRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public List<SalesRevenueView> findAllActive(SalesRevenueListCriteria criteria) {
        List<SalesRevenueJpaEntity> rows;
        if (criteria == null) {
            rows = revenueRepository.findByRecordingStateOrderByRevenueDateDescIdDesc(ACTIVE);
        } else {
            rows = revenueRepository.searchActive(
                    ACTIVE,
                    criteria.revenueDateFrom(),
                    criteria.revenueDateTo(),
                    normalize(criteria.revenueNo()),
                    criteria.status(),
                    criteria.excludeCancelled(),
                    SalesRevenueStatus.CANCELLED
            );
        }
        return rows.stream()
                .map(this::toView)
                .filter(view -> matchesPartnerName(view, criteria))
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<SalesRevenueView> findActiveIssuedById(long id) {
        return revenueRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, SalesRevenueStatus.ISSUED)
                .map(this::toView);
    }

    @Override
    @Transactional
    public void addInvoicedQty(long salesShipmentLineId, BigDecimal qty, String actorUserId) {
        SalesShipmentLineJpaEntity line = requireActiveShipmentLine(salesShipmentLineId);
        line.setInvoicedQty(line.getInvoicedQty().add(qty));
        shipmentLineRepository.save(line);
    }

    @Override
    @Transactional
    public void subtractInvoicedQty(long salesShipmentLineId, BigDecimal qty, String actorUserId) {
        SalesShipmentLineJpaEntity line = requireActiveShipmentLine(salesShipmentLineId);
        line.setInvoicedQty(line.getInvoicedQty().subtract(qty));
        shipmentLineRepository.save(line);
    }

    private SalesRevenueView toView(SalesRevenueJpaEntity entity) {
        CompanyJpaEntity partner = companyRepository.findById(entity.getPartnerId()).orElse(null);
        List<SalesRevenueLineJpaEntity> lineEntities =
                revenueLineRepository.findBySalesRevenueIdAndRecordingStateOrderByLineNoAsc(
                        entity.getId(), ACTIVE);
        List<SalesRevenueLineView> lines = new ArrayList<>();
        for (SalesRevenueLineJpaEntity lineEntity : lineEntities) {
            SalesShipmentLineJpaEntity shipmentLine = shipmentLineRepository
                    .findByIdAndRecordingState(lineEntity.getSalesShipmentLineId(), ACTIVE)
                    .orElse(null);
            SalesShipmentJpaEntity shipment = shipmentLine != null
                    ? shipmentRepository.findById(shipmentLine.getSalesShipmentId()).orElse(null)
                    : null;
            SalesOrderLineJpaEntity orderLine = shipmentLine != null
                    ? orderLineRepository.findByIdAndRecordingState(shipmentLine.getSalesOrderLineId(), ACTIVE).orElse(null)
                    : null;
            SalesOrderJpaEntity order = orderLine != null
                    ? orderRepository.findByIdAndRecordingState(orderLine.getSalesOrderId(), ACTIVE).orElse(null)
                    : null;
            ItemJpaEntity item = itemRepository.findById(lineEntity.getItemId()).orElse(null);
            lines.add(new SalesRevenueLineView(
                    lineEntity.getId(),
                    lineEntity.getLineNo(),
                    lineEntity.getSalesShipmentLineId(),
                    shipment != null ? shipment.getShipmentNo() : "",
                    order != null ? order.getOrderNo() : "",
                    entity.getPartnerId(),
                    partner != null ? partner.getCompanyName() : "",
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    lineEntity.getRevenueQty(),
                    lineEntity.getUnitPrice(),
                    lineEntity.getAmount(),
                    lineEntity.getLotId()
            ));
        }
        return new SalesRevenueView(
                entity.getId(),
                entity.getRevenueNo(),
                entity.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                entity.getRevenueDate(),
                entity.getSalesShipmentId(),
                entity.getStatus(),
                entity.getCreatedAt(),
                masterAuditActorLookup.nameOf(entity.getCreatedById()),
                entity.getStatus() == SalesRevenueStatus.ISSUED,
                lines
        );
    }

    private boolean matchesPartnerName(SalesRevenueView view, SalesRevenueListCriteria criteria) {
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

    private SalesShipmentJpaEntity requireActiveShipment(long shipmentId) {
        return shipmentRepository.findByIdAndRecordingStateAndStatus(shipmentId, ACTIVE, SalesShipmentStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("출고·납품을 찾을 수 없습니다: " + shipmentId));
    }

    private SalesShipmentLineJpaEntity requireActiveShipmentLine(long lineId) {
        return shipmentLineRepository.findByIdAndRecordingState(lineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("출고 라인을 찾을 수 없습니다: " + lineId));
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
