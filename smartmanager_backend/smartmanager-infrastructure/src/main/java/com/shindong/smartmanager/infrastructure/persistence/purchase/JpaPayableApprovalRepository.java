package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.application.outsource.OutsourceHistoryRecord;
import com.shindong.smartmanager.application.purchase.PayableApprovalCriteria;
import com.shindong.smartmanager.application.purchase.PayableApprovalRepository;
import com.shindong.smartmanager.application.purchase.PayableApprovalView;
import com.shindong.smartmanager.application.purchase.PurchaseHistoryRecord;
import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import com.shindong.smartmanager.infrastructure.persistence.outsource.OutsourceHistoryJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.outsource.SpringDataOutsourceHistoryRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.sql.Timestamp;
import java.time.Instant;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPayableApprovalRepository implements PayableApprovalRepository {

    private static final int ACTIVE = 1;

    private static final String PURCHASE_SELECT = """
            SELECT 'PURCHASE' AS ledger_kind,
                   ph.id AS history_id,
                   ph.company_id AS partner_id,
                   c.company_name AS partner_name,
                   ph.history_date AS receipt_date,
                   COALESCE(i.item_no, '') AS item_no,
                   COALESCE(i.item_name, ph.item_name, '') AS item_name,
                   COALESCE(i.model_type, '') AS drawing_no,
                   '' AS process_name,
                   ph.purchase_qty AS qty,
                   COALESCE(i.standard_unit_cost, 0) AS standard_unit_price,
                   ph.unit_price,
                   ph.amount,
                   ph.fiscal_year,
                   ph.fiscal_month,
                   CASE WHEN ph.source_type = 'ETC_PURCHASE_RECEIPT' THEN '기타' ELSE '구매' END AS category_label,
                   ph.approval_status,
                   ph.approved_at,
                   u1.name AS approved_by_name,
                   ph.approval_cancelled_at,
                   u2.name AS approval_cancelled_by_name
            FROM purchase_history ph
            JOIN company c ON c.id = ph.company_id AND c.recording_state = 1
            LEFT JOIN item i ON i.id = ph.item_id AND i.recording_state = 1
            LEFT JOIN user u1 ON u1.id = ph.approved_by_user_id
            LEFT JOIN user u2 ON u2.id = ph.approval_cancelled_by_user_id
            WHERE ph.recording_state = 1
            """;

    private static final String OUTSOURCE_SELECT = """
            SELECT 'OUTSOURCE' AS ledger_kind,
                   oh.id AS history_id,
                   oh.company_id AS partner_id,
                   c.company_name AS partner_name,
                   oh.history_date AS receipt_date,
                   COALESCE(i.item_no, '') AS item_no,
                   COALESCE(i.item_name, '') AS item_name,
                   COALESCE(i.model_type, '') AS drawing_no,
                   COALESCE(CONCAT(pc.small_code, ' ', pc.small_name), '') AS process_name,
                   oh.outsource_qty AS qty,
                   COALESCE(i.standard_unit_cost, 0) AS standard_unit_price,
                   oh.unit_price,
                   oh.amount,
                   oh.fiscal_year,
                   oh.fiscal_month,
                   '외주' AS category_label,
                   oh.approval_status,
                   oh.approved_at,
                   u1.name AS approved_by_name,
                   oh.approval_cancelled_at,
                   u2.name AS approval_cancelled_by_name
            FROM outsource_history oh
            JOIN company c ON c.id = oh.company_id AND c.recording_state = 1
            LEFT JOIN item i ON i.id = oh.item_id AND i.recording_state = 1
            LEFT JOIN user u1 ON u1.id = oh.approved_by_user_id
            LEFT JOIN user u2 ON u2.id = oh.approval_cancelled_by_user_id
            LEFT JOIN outsourcing_receipt_line orl_direct
                   ON oh.source_type = 'OUTSOURCING_RECEIPT' AND oh.source_id = orl_direct.id AND orl_direct.recording_state = 1
            LEFT JOIN quality_inspection qi
                   ON oh.source_type = 'QUALITY_INSPECTION' AND oh.source_id = qi.id AND qi.recording_state = 1
            LEFT JOIN outsourcing_receipt_line orl_qi
                   ON qi.id IS NOT NULL AND qi.source_receipt_line_id = orl_qi.id AND orl_qi.recording_state = 1
            LEFT JOIN outsourcing_order_line ool
                   ON ool.id = COALESCE(orl_direct.outsourcing_order_line_id, orl_qi.outsourcing_order_line_id)
            LEFT JOIN process_sequence ps ON ps.id = ool.process_sequence_id AND ps.recording_state = 1
            LEFT JOIN public_code pc ON pc.id = ps.public_code_id AND pc.recording_state = 1
            WHERE oh.recording_state = 1
            """;

    private final EntityManager entityManager;
    private final SpringDataPurchaseHistoryRepository purchaseHistoryRepository;
    private final SpringDataOutsourceHistoryRepository outsourceHistoryRepository;

    public JpaPayableApprovalRepository(
            EntityManager entityManager,
            SpringDataPurchaseHistoryRepository purchaseHistoryRepository,
            SpringDataOutsourceHistoryRepository outsourceHistoryRepository
    ) {
        this.entityManager = entityManager;
        this.purchaseHistoryRepository = purchaseHistoryRepository;
        this.outsourceHistoryRepository = outsourceHistoryRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PayableApprovalView> findPending(PayableApprovalCriteria criteria) {
        return findByStatus(PayableApprovalStatus.PENDING, criteria);
    }

    @Override
    @Transactional(readOnly = true)
    public List<PayableApprovalView> findApproved(PayableApprovalCriteria criteria) {
        return findByStatus(PayableApprovalStatus.APPROVED, criteria);
    }

    @Override
    @Transactional(readOnly = true)
    public PurchaseHistoryRecord findActivePurchaseHistory(long id) {
        PurchaseHistoryJpaEntity entity = purchaseHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("매입 이력을 찾을 수 없습니다: " + id));
        return toPurchaseRecord(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public OutsourceHistoryRecord findActiveOutsourceHistory(long id) {
        OutsourceHistoryJpaEntity entity = outsourceHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("외주 이력을 찾을 수 없습니다: " + id));
        return toOutsourceRecord(entity);
    }

    @Override
    @Transactional
    public void approvePurchaseHistory(long id, long userId) {
        PurchaseHistoryJpaEntity entity = purchaseHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("매입 이력을 찾을 수 없습니다: " + id));
        if (entity.getApprovalStatus() == PayableApprovalStatus.APPROVED) {
            return;
        }
        entity.setApprovalStatus(PayableApprovalStatus.APPROVED);
        entity.setApprovedAt(Instant.now());
        entity.setApprovedByUserId(userId);
        entity.setApprovalCancelledAt(null);
        entity.setApprovalCancelledByUserId(null);
        purchaseHistoryRepository.save(entity);
    }

    @Override
    @Transactional
    public void cancelApprovalPurchaseHistory(long id, long userId) {
        PurchaseHistoryJpaEntity entity = purchaseHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("매입 이력을 찾을 수 없습니다: " + id));
        if (entity.getApprovalStatus() == PayableApprovalStatus.PENDING) {
            return;
        }
        entity.setApprovalStatus(PayableApprovalStatus.PENDING);
        entity.setApprovalCancelledAt(Instant.now());
        entity.setApprovalCancelledByUserId(userId);
        purchaseHistoryRepository.save(entity);
    }

    @Override
    @Transactional
    public void approveOutsourceHistory(long id, long userId) {
        OutsourceHistoryJpaEntity entity = outsourceHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("외주 이력을 찾을 수 없습니다: " + id));
        if (entity.getApprovalStatus() == PayableApprovalStatus.APPROVED) {
            return;
        }
        entity.setApprovalStatus(PayableApprovalStatus.APPROVED);
        entity.setApprovedAt(Instant.now());
        entity.setApprovedByUserId(userId);
        entity.setApprovalCancelledAt(null);
        entity.setApprovalCancelledByUserId(null);
        outsourceHistoryRepository.save(entity);
    }

    @Override
    @Transactional
    public void cancelApprovalOutsourceHistory(long id, long userId) {
        OutsourceHistoryJpaEntity entity = outsourceHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("외주 이력을 찾을 수 없습니다: " + id));
        if (entity.getApprovalStatus() == PayableApprovalStatus.PENDING) {
            return;
        }
        entity.setApprovalStatus(PayableApprovalStatus.PENDING);
        entity.setApprovalCancelledAt(Instant.now());
        entity.setApprovalCancelledByUserId(userId);
        outsourceHistoryRepository.save(entity);
    }

    @Override
    @Transactional
    public void updatePurchaseHistoryFiscalPeriod(long id, int fiscalYear, int fiscalMonth) {
        PurchaseHistoryJpaEntity entity = purchaseHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("매입 이력을 찾을 수 없습니다: " + id));
        entity.setFiscalYear((short) fiscalYear);
        entity.setFiscalMonth((byte) fiscalMonth);
        purchaseHistoryRepository.save(entity);
    }

    @Override
    @Transactional
    public void updateOutsourceHistoryFiscalPeriod(long id, int fiscalYear, int fiscalMonth) {
        OutsourceHistoryJpaEntity entity = outsourceHistoryRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("외주 이력을 찾을 수 없습니다: " + id));
        entity.setFiscalYear((short) fiscalYear);
        entity.setFiscalMonth((byte) fiscalMonth);
        outsourceHistoryRepository.save(entity);
    }

    private List<PayableApprovalView> findByStatus(PayableApprovalStatus status, PayableApprovalCriteria criteria) {
        List<PayableApprovalView> result = new ArrayList<>();
        result.addAll(queryPurchase(status, criteria));
        result.addAll(queryOutsource(status, criteria));
        result.sort((a, b) -> {
            int dateCompare = b.receiptDate().compareTo(a.receiptDate());
            if (dateCompare != 0) {
                return dateCompare;
            }
            return Long.compare(b.historyId(), a.historyId());
        });
        return result;
    }

    private List<PayableApprovalView> queryPurchase(PayableApprovalStatus status, PayableApprovalCriteria criteria) {
        StringBuilder sql = new StringBuilder(PURCHASE_SELECT);
        sql.append(" AND ph.approval_status = :status");
        appendCommonFilters(sql, criteria, true);
        sql.append(" ORDER BY ph.history_date DESC, ph.id DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        query.setParameter("status", status.name());
        bindCommonParams(query, criteria);
        return mapRows(query.getResultList());
    }

    private List<PayableApprovalView> queryOutsource(PayableApprovalStatus status, PayableApprovalCriteria criteria) {
        StringBuilder sql = new StringBuilder(OUTSOURCE_SELECT);
        sql.append(" AND oh.approval_status = :status");
        appendCommonFilters(sql, criteria, false);
        sql.append(" ORDER BY oh.history_date DESC, oh.id DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        query.setParameter("status", status.name());
        bindCommonParams(query, criteria);
        return mapRows(query.getResultList());
    }

    private void appendCommonFilters(StringBuilder sql, PayableApprovalCriteria criteria, boolean purchase) {
        if (criteria == null) {
            return;
        }
        if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            sql.append(" AND c.company_name LIKE :partnerName");
        }
        if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND i.item_no LIKE :itemNo");
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            if (purchase) {
                sql.append(" AND (i.item_name LIKE :itemName OR ph.item_name LIKE :itemName)");
            } else {
                sql.append(" AND i.item_name LIKE :itemName");
            }
        }
        String dateColumn = purchase ? "ph.history_date" : "oh.history_date";
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND ").append(dateColumn).append(" >= :receiptDateFrom");
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND ").append(dateColumn).append(" <= :receiptDateTo");
        }
        String fiscalYearColumn = purchase ? "ph.fiscal_year" : "oh.fiscal_year";
        String fiscalMonthColumn = purchase ? "ph.fiscal_month" : "oh.fiscal_month";
        if (criteria.fiscalYear() != null) {
            sql.append(" AND ").append(fiscalYearColumn).append(" = :fiscalYear");
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND ").append(fiscalMonthColumn).append(" = :fiscalMonth");
        }
    }

    private void bindCommonParams(Query query, PayableApprovalCriteria criteria) {
        if (criteria == null) {
            return;
        }
        if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            query.setParameter("partnerName", "%" + criteria.partnerName().trim() + "%");
        }
        if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            query.setParameter("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            query.setParameter("itemName", "%" + criteria.itemName().trim() + "%");
        }
        if (criteria.receiptDateFrom() != null) {
            query.setParameter("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            query.setParameter("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.fiscalYear() != null) {
            query.setParameter("fiscalYear", criteria.fiscalYear());
        }
        if (criteria.fiscalMonth() != null) {
            query.setParameter("fiscalMonth", criteria.fiscalMonth());
        }
    }

    @SuppressWarnings("unchecked")
    private List<PayableApprovalView> mapRows(List<?> rows) {
        List<PayableApprovalView> result = new ArrayList<>();
        for (Object rowObj : rows) {
            result.add(toView((Object[]) rowObj));
        }
        return result;
    }

    private PayableApprovalView toView(Object[] row) {
        return new PayableApprovalView(
                PayableApprovalLedgerKind.valueOf(row[0].toString()),
                ((Number) row[1]).longValue(),
                ((Number) row[2]).longValue(),
                row[3] != null ? row[3].toString() : "",
                toLocalDate(row[4]),
                row[5] != null ? row[5].toString() : "",
                row[6] != null ? row[6].toString() : "",
                row[7] != null ? row[7].toString() : "",
                row[8] != null ? row[8].toString() : "",
                toBigDecimal(row[9]),
                toBigDecimal(row[10]),
                toBigDecimal(row[11]),
                toBigDecimal(row[12]),
                ((Number) row[13]).intValue(),
                ((Number) row[14]).intValue(),
                row[15] != null ? row[15].toString() : "",
                PayableApprovalStatus.valueOf(row[16].toString()),
                toInstant(row[17]),
                row[18] != null ? row[18].toString() : null,
                toInstant(row[19]),
                row[20] != null ? row[20].toString() : null
        );
    }

    private static PurchaseHistoryRecord toPurchaseRecord(PurchaseHistoryJpaEntity entity) {
        return new PurchaseHistoryRecord(
                entity.getId(),
                entity.getCompanyId(),
                entity.getAmount(),
                entity.getHistoryDate(),
                entity.getFiscalYear(),
                entity.getFiscalMonth(),
                entity.getApprovalStatus()
        );
    }

    private static OutsourceHistoryRecord toOutsourceRecord(OutsourceHistoryJpaEntity entity) {
        return new OutsourceHistoryRecord(
                entity.getId(),
                entity.getCompanyId(),
                entity.getAmount(),
                entity.getHistoryDate(),
                entity.getFiscalYear(),
                entity.getFiscalMonth(),
                entity.getApprovalStatus()
        );
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
        if (value instanceof java.sql.Date date) {
            return date.toLocalDate();
        }
        return LocalDate.parse(value.toString());
    }

    private static Instant toInstant(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof Instant instant) {
            return instant;
        }
        if (value instanceof Timestamp timestamp) {
            return timestamp.toInstant();
        }
        return Instant.parse(value.toString());
    }
}
