package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptListCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptRepository;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptSaveCommand;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptView;
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
public class JpaEtcPurchaseReceiptRepository implements EtcPurchaseReceiptRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataEtcPurchaseReceiptRepository receiptRepository;

    public JpaEtcPurchaseReceiptRepository(
            EntityManager entityManager,
            SpringDataEtcPurchaseReceiptRepository receiptRepository
    ) {
        this.entityManager = entityManager;
        this.receiptRepository = receiptRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<EtcPurchaseReceiptView> findActive(EtcPurchaseReceiptListCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT r.id, r.receipt_no, r.etc_purchase_order_id, o.order_no, r.partner_id, c.company_name,
                       r.item_name, r.receipt_qty, r.unit_price, r.amount, r.receipt_date,
                       r.fiscal_year, r.fiscal_month
                FROM etc_purchase_receipt r
                JOIN etc_purchase_order o ON o.id = r.etc_purchase_order_id
                JOIN company c ON c.id = r.partner_id AND c.recording_state = 1
                WHERE r.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND r.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
            if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
                sql.append(" AND c.company_name LIKE :partnerName");
                params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
            }
            if (criteria.receiptFrom() != null) {
                sql.append(" AND r.receipt_date >= :receiptFrom");
                params.put("receiptFrom", criteria.receiptFrom());
            }
            if (criteria.receiptTo() != null) {
                sql.append(" AND r.receipt_date <= :receiptTo");
                params.put("receiptTo", criteria.receiptTo());
            }
            if (criteria.fiscalYear() != null) {
                sql.append(" AND r.fiscal_year = :fiscalYear");
                params.put("fiscalYear", criteria.fiscalYear());
            }
            if (criteria.fiscalMonth() != null) {
                sql.append(" AND r.fiscal_month = :fiscalMonth");
                params.put("fiscalMonth", criteria.fiscalMonth());
            }
        }
        sql.append(" ORDER BY r.receipt_date DESC, r.id DESC");
        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<EtcPurchaseReceiptView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(mapView(row));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<EtcPurchaseReceiptView> findActiveById(long id) {
        String sql = """
                SELECT r.id, r.receipt_no, r.etc_purchase_order_id, o.order_no, r.partner_id, c.company_name,
                       r.item_name, r.receipt_qty, r.unit_price, r.amount, r.receipt_date,
                       r.fiscal_year, r.fiscal_month
                FROM etc_purchase_receipt r
                JOIN etc_purchase_order o ON o.id = r.etc_purchase_order_id
                JOIN company c ON c.id = r.partner_id AND c.recording_state = 1
                WHERE r.recording_state = 1 AND r.id = :id
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("id", id);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        if (rows.isEmpty()) {
            return Optional.empty();
        }
        return Optional.of(mapView(rows.get(0)));
    }

    @Override
    @Transactional(readOnly = true)
    public long countByReceiptNoPrefix(String prefix) {
        return receiptRepository.countByReceiptNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional
    public long save(EtcPurchaseReceiptSaveCommand command, String receiptNo, String actorUserId) {
        Instant now = Instant.now();
        EtcPurchaseReceiptJpaEntity entity = new EtcPurchaseReceiptJpaEntity();
        entity.setReceiptNo(receiptNo);
        entity.setEtcPurchaseOrderId(command.etcPurchaseOrderId());
        entity.setPartnerId(command.partnerId());
        entity.setItemName(command.itemName());
        entity.setReceiptQty(command.receiptQty());
        entity.setUnitPrice(command.unitPrice());
        entity.setAmount(command.amount());
        entity.setReceiptDate(command.receiptDate());
        entity.setFiscalYear((short) command.fiscalYear());
        entity.setFiscalMonth((byte) command.fiscalMonth());
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        return receiptRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, EtcPurchaseReceiptSaveCommand command, String actorUserId) {
        EtcPurchaseReceiptJpaEntity entity = requireActive(id);
        entity.setReceiptQty(command.receiptQty());
        entity.setAmount(command.amount());
        entity.setReceiptDate(command.receiptDate());
        entity.setFiscalYear((short) command.fiscalYear());
        entity.setFiscalMonth((byte) command.fiscalMonth());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        receiptRepository.save(entity);
    }

    @Override
    @Transactional
    public void delete(long id) {
        EtcPurchaseReceiptJpaEntity entity = requireActive(id);
        receiptRepository.delete(entity);
    }

    private EtcPurchaseReceiptJpaEntity requireActive(long id) {
        return receiptRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("기타구매입고를 찾을 수 없습니다: " + id));
    }

    private EtcPurchaseReceiptView mapView(Object[] row) {
        return new EtcPurchaseReceiptView(
                ((Number) row[0]).longValue(),
                (String) row[1],
                ((Number) row[2]).longValue(),
                (String) row[3],
                ((Number) row[4]).longValue(),
                (String) row[5],
                (String) row[6],
                (BigDecimal) row[7],
                (BigDecimal) row[8],
                (BigDecimal) row[9],
                row[10] != null ? ((java.sql.Date) row[10]).toLocalDate() : null,
                ((Number) row[11]).intValue(),
                ((Number) row[12]).intValue()
        );
    }
}
