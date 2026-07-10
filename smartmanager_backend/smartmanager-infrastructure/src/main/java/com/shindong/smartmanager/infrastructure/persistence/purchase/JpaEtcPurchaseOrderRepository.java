package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.application.purchase.CreateEtcPurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderListCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderRepository;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderService;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderView;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptCandidateCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptCandidateView;
import com.shindong.smartmanager.application.purchase.UpdateEtcPurchaseOrderCommand;
import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import jakarta.persistence.EntityManager;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaEtcPurchaseOrderRepository implements EtcPurchaseOrderRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataEtcPurchaseOrderRepository orderRepository;

    public JpaEtcPurchaseOrderRepository(
            EntityManager entityManager,
            SpringDataEtcPurchaseOrderRepository orderRepository
    ) {
        this.entityManager = entityManager;
        this.orderRepository = orderRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<EtcPurchaseOrderView> findActive(EtcPurchaseOrderListCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT o.id, o.order_no, o.item_name, o.partner_id, c.company_name, c.business_reg_no,
                       o.unit_price, o.order_qty, o.remain_qty, o.amount, o.requested_delivery_date,
                       o.category_code_id, pc.small_name AS category_name, o.status, o.order_date
                FROM etc_purchase_order o
                JOIN company c ON c.id = o.partner_id AND c.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = o.category_code_id AND pc.recording_state = 1
                WHERE o.recording_state = 1
                """);
        Map<String, Object> params = appendOrderFilters(sql, criteria);
        if (criteria != null && Boolean.TRUE.equals(criteria.openOnly())) {
            sql.append(" AND o.status <> 'COMPLETED'");
        }
        sql.append(" ORDER BY o.order_date DESC, o.id DESC");
        return mapOrderViews(sql, params);
    }

    @Override
    @Transactional(readOnly = true)
    public List<EtcPurchaseReceiptCandidateView> findReceiptCandidates(EtcPurchaseReceiptCandidateCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT o.id, o.order_no, o.item_name, o.partner_id, c.company_name,
                       o.unit_price, o.order_qty, o.remain_qty, o.amount, o.requested_delivery_date,
                       pc.small_name AS category_name, o.status
                FROM etc_purchase_order o
                JOIN company c ON c.id = o.partner_id AND c.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = o.category_code_id AND pc.recording_state = 1
                WHERE o.recording_state = 1
                  AND o.status <> 'COMPLETED'
                  AND o.remain_qty > 0
                """);
        Map<String, Object> params = appendCandidateFilters(sql, criteria);
        sql.append(" ORDER BY o.requested_delivery_date ASC, o.id ASC");
        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<EtcPurchaseReceiptCandidateView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new EtcPurchaseReceiptCandidateView(
                    ((Number) row[0]).longValue(),
                    (String) row[1],
                    (String) row[2],
                    ((Number) row[3]).longValue(),
                    (String) row[4],
                    (BigDecimal) row[5],
                    (BigDecimal) row[6],
                    (BigDecimal) row[7],
                    (BigDecimal) row[8],
                    row[9] != null ? ((java.sql.Date) row[9]).toLocalDate() : null,
                    (String) row[10],
                    EtcPurchaseOrderStatus.valueOf((String) row[11])
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<EtcPurchaseOrderView> findActiveById(long id) {
        String sql = """
                SELECT o.id, o.order_no, o.item_name, o.partner_id, c.company_name, c.business_reg_no,
                       o.unit_price, o.order_qty, o.remain_qty, o.amount, o.requested_delivery_date,
                       o.category_code_id, pc.small_name AS category_name, o.status, o.order_date
                FROM etc_purchase_order o
                JOIN company c ON c.id = o.partner_id AND c.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = o.category_code_id AND pc.recording_state = 1
                WHERE o.recording_state = 1 AND o.id = :id
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("id", id);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        if (rows.isEmpty()) {
            return Optional.empty();
        }
        return Optional.of(mapOrderView(rows.get(0)));
    }

    @Override
    @Transactional(readOnly = true)
    public long countByOrderNoPrefix(String prefix) {
        return orderRepository.countByOrderNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional
    public long save(CreateEtcPurchaseOrderCommand command, String orderNo, String actorUserId) {
        Instant now = Instant.now();
        BigDecimal amount = EtcPurchaseOrderService.lineAmount(command.orderQty(), command.unitPrice());
        EtcPurchaseOrderJpaEntity entity = new EtcPurchaseOrderJpaEntity();
        entity.setOrderNo(orderNo);
        entity.setItemName(command.itemName().trim());
        entity.setPartnerId(command.partnerId());
        entity.setUnitPrice(command.unitPrice());
        entity.setOrderQty(command.orderQty());
        entity.setAmount(amount);
        entity.setRemainQty(command.orderQty());
        entity.setRequestedDeliveryDate(command.requestedDeliveryDate());
        entity.setCategoryCodeId(command.categoryCodeId());
        entity.setStatus(EtcPurchaseOrderStatus.WAITING);
        entity.setOrderDate(command.orderDate());
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        return orderRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, UpdateEtcPurchaseOrderCommand command, String actorUserId) {
        EtcPurchaseOrderJpaEntity entity = requireActive(id);
        BigDecimal amount = EtcPurchaseOrderService.lineAmount(command.orderQty(), command.unitPrice());
        entity.setItemName(command.itemName().trim());
        entity.setPartnerId(command.partnerId());
        entity.setUnitPrice(command.unitPrice());
        entity.setOrderQty(command.orderQty());
        entity.setAmount(amount);
        entity.setRequestedDeliveryDate(command.requestedDeliveryDate());
        entity.setCategoryCodeId(command.categoryCodeId());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        orderRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        EtcPurchaseOrderJpaEntity entity = requireActive(id);
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        orderRepository.save(entity);
    }

    @Override
    @Transactional
    public void updateRemainQtyAndStatus(long id, BigDecimal remainQty, String actorUserId) {
        EtcPurchaseOrderJpaEntity entity = requireActive(id);
        entity.setRemainQty(remainQty);
        entity.setStatus(EtcPurchaseOrderService.resolveStatus(entity.getOrderQty(), remainQty));
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        orderRepository.save(entity);
    }

    private EtcPurchaseOrderJpaEntity requireActive(long id) {
        return orderRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("기타구매발주를 찾을 수 없습니다: " + id));
    }

    private List<EtcPurchaseOrderView> mapOrderViews(StringBuilder sql, Map<String, Object> params) {
        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<EtcPurchaseOrderView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(mapOrderView(row));
        }
        return result;
    }

    private EtcPurchaseOrderView mapOrderView(Object[] row) {
        BigDecimal unitPrice = (BigDecimal) row[6];
        BigDecimal orderQty = (BigDecimal) row[7];
        BigDecimal remainQty = (BigDecimal) row[8];
        BigDecimal amount = (BigDecimal) row[9];
        EtcPurchaseOrderStatus status = EtcPurchaseOrderStatus.valueOf((String) row[13]);
        boolean editable = status == EtcPurchaseOrderStatus.WAITING
                && remainQty.compareTo(orderQty) == 0;
        Long categoryCodeId = row[11] != null ? ((Number) row[11]).longValue() : null;
        return new EtcPurchaseOrderView(
                ((Number) row[0]).longValue(),
                (String) row[1],
                (String) row[2],
                ((Number) row[3]).longValue(),
                (String) row[4],
                (String) row[5],
                unitPrice,
                orderQty,
                remainQty,
                amount,
                row[10] != null ? ((java.sql.Date) row[10]).toLocalDate() : null,
                categoryCodeId,
                (String) row[12],
                status,
                row[14] != null ? ((java.sql.Date) row[14]).toLocalDate() : null,
                editable
        );
    }

    private Map<String, Object> appendOrderFilters(StringBuilder sql, EtcPurchaseOrderListCriteria criteria) {
        Map<String, Object> params = new HashMap<>();
        if (criteria == null) {
            return params;
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            sql.append(" AND o.item_name LIKE :itemName");
            params.put("itemName", "%" + criteria.itemName().trim() + "%");
        }
        if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            sql.append(" AND c.company_name LIKE :partnerName");
            params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
        }
        if (criteria.orderNo() != null && !criteria.orderNo().isBlank()) {
            sql.append(" AND o.order_no LIKE :orderNo");
            params.put("orderNo", "%" + criteria.orderNo().trim() + "%");
        }
        if (criteria.orderDateFrom() != null) {
            sql.append(" AND o.order_date >= :orderDateFrom");
            params.put("orderDateFrom", criteria.orderDateFrom());
        }
        if (criteria.orderDateTo() != null) {
            sql.append(" AND o.order_date <= :orderDateTo");
            params.put("orderDateTo", criteria.orderDateTo());
        }
        if (criteria.deliveryFrom() != null) {
            sql.append(" AND o.requested_delivery_date >= :deliveryFrom");
            params.put("deliveryFrom", criteria.deliveryFrom());
        }
        if (criteria.deliveryTo() != null) {
            sql.append(" AND o.requested_delivery_date <= :deliveryTo");
            params.put("deliveryTo", criteria.deliveryTo());
        }
        return params;
    }

    private Map<String, Object> appendCandidateFilters(StringBuilder sql, EtcPurchaseReceiptCandidateCriteria criteria) {
        Map<String, Object> params = new HashMap<>();
        if (criteria == null) {
            return params;
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            sql.append(" AND o.item_name LIKE :itemName");
            params.put("itemName", "%" + criteria.itemName().trim() + "%");
        }
        if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            sql.append(" AND c.company_name LIKE :partnerName");
            params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
        }
        if (criteria.deliveryFrom() != null) {
            sql.append(" AND o.requested_delivery_date >= :deliveryFrom");
            params.put("deliveryFrom", criteria.deliveryFrom());
        }
        if (criteria.deliveryTo() != null) {
            sql.append(" AND o.requested_delivery_date <= :deliveryTo");
            params.put("deliveryTo", criteria.deliveryTo());
        }
        return params;
    }
}
