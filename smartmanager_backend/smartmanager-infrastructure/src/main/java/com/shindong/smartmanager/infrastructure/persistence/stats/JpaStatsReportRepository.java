package com.shindong.smartmanager.infrastructure.persistence.stats;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.FiscalPeriodDateRange;
import com.shindong.smartmanager.application.stats.ItemStockMovementCriteria;
import com.shindong.smartmanager.application.stats.ItemStockMovementView;
import com.shindong.smartmanager.application.stats.OrderVsReceiptCriteria;
import com.shindong.smartmanager.application.stats.OrderVsReceiptView;
import com.shindong.smartmanager.application.stats.PartnerMonthlyPayableCriteria;
import com.shindong.smartmanager.application.stats.PartnerMonthlyPayableView;
import com.shindong.smartmanager.application.stats.PurchaseDailyReportCriteria;
import com.shindong.smartmanager.application.stats.PurchaseDailyReportView;
import com.shindong.smartmanager.application.stats.StatsReportRepository;
import com.shindong.smartmanager.application.stats.VendorPurchaseStatusCriteria;
import com.shindong.smartmanager.application.stats.VendorPurchaseStatusView;
import com.shindong.smartmanager.application.stats.VendorPurchaseTotalCriteria;
import com.shindong.smartmanager.application.stats.VendorPurchaseTotalView;
import com.shindong.smartmanager.application.stats.WarehouseMonthlyIoCriteria;
import com.shindong.smartmanager.application.stats.WarehouseMonthlyIoView;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.sql.Date;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaStatsReportRepository implements StatsReportRepository {

    private static final int ACTIVE = 1;

    @PersistenceContext
    private EntityManager entityManager;

    private final FiscalCalendarService fiscalCalendarService;

    public JpaStatsReportRepository(FiscalCalendarService fiscalCalendarService) {
        this.fiscalCalendarService = fiscalCalendarService;
    }

    @Override
    @Transactional(readOnly = true)
    public List<VendorPurchaseTotalView> findVendorPurchaseTotals(VendorPurchaseTotalCriteria criteria) {
        String division = criteria.division() == null || criteria.division().isBlank()
                ? "ALL"
                : criteria.division().trim().toUpperCase();

        List<VendorPurchaseTotalView> result = new ArrayList<>();
        if ("ALL".equals(division) || "PURCHASE".equals(division) || "ETC".equals(division)) {
            result.addAll(findPurchaseHistoryTotals(criteria, division));
        }
        if ("ALL".equals(division) || "OUTSOURCE".equals(division)) {
            result.addAll(findOutsourceHistoryTotals(criteria));
        }
        if ("ALL".equals(division) || "CLAIM".equals(division)) {
            result.addAll(findEtcClaimTotals(criteria));
            result.addAll(findDefectClaimTotals(criteria));
        }
        result.sort((a, b) -> {
            int c = a.companyName().compareToIgnoreCase(b.companyName());
            if (c != 0) return c;
            c = Integer.compare(a.fiscalYear(), b.fiscalYear());
            if (c != 0) return c;
            c = Integer.compare(a.fiscalMonth(), b.fiscalMonth());
            if (c != 0) return c;
            String an = a.itemNo() != null ? a.itemNo() : a.itemName();
            String bn = b.itemNo() != null ? b.itemNo() : b.itemName();
            return an.compareToIgnoreCase(bn);
        });
        return result;
    }

    private List<VendorPurchaseTotalView> findPurchaseHistoryTotals(
            VendorPurchaseTotalCriteria criteria,
            String division
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT ph.company_id, c.company_name,
                       ph.item_id, i.item_no,
                       COALESCE(i.item_name, ph.item_name, '') AS item_name,
                       CASE
                         WHEN ph.source_type = 'ETC_PURCHASE_RECEIPT' THEN 'ETC'
                         ELSE 'PURCHASE'
                       END AS division,
                       SUM(ph.purchase_qty) AS purchase_qty,
                       CASE WHEN SUM(ph.purchase_qty) = 0 THEN 0
                            ELSE ROUND(SUM(ph.amount) / SUM(ph.purchase_qty), 2)
                       END AS unit_price,
                       SUM(ph.amount) AS amount,
                       ph.fiscal_year, ph.fiscal_month
                FROM purchase_history ph
                JOIN company c ON c.id = ph.company_id
                LEFT JOIN item i ON i.id = ph.item_id AND i.recording_state = 1
                WHERE ph.recording_state = 1
                  AND ph.approval_status = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendVendorCommonFilters(sql, params, criteria, "ph", true);
        if ("PURCHASE".equals(division)) {
            sql.append(" AND ph.source_type IN ('PURCHASE_RECEIPT', 'QUALITY_INSPECTION')");
        } else if ("ETC".equals(division)) {
            sql.append(" AND ph.source_type = 'ETC_PURCHASE_RECEIPT'");
        }
        sql.append("""
                 GROUP BY ph.company_id, c.company_name, ph.item_id, i.item_no,
                          COALESCE(i.item_name, ph.item_name, ''),
                          CASE WHEN ph.source_type = 'ETC_PURCHASE_RECEIPT' THEN 'ETC' ELSE 'PURCHASE' END,
                          ph.fiscal_year, ph.fiscal_month
                 ORDER BY c.company_name, ph.fiscal_year, ph.fiscal_month, i.item_no
                 LIMIT 2000
                """);
        return mapVendorRows(sql.toString(), params);
    }

    private List<VendorPurchaseTotalView> findOutsourceHistoryTotals(VendorPurchaseTotalCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT oh.company_id, c.company_name,
                       oh.item_id, i.item_no, i.item_name,
                       'OUTSOURCE' AS division,
                       SUM(oh.outsource_qty) AS purchase_qty,
                       CASE WHEN SUM(oh.outsource_qty) = 0 THEN 0
                            ELSE ROUND(SUM(oh.amount) / SUM(oh.outsource_qty), 2)
                       END AS unit_price,
                       SUM(oh.amount) AS amount,
                       oh.fiscal_year, oh.fiscal_month
                FROM outsource_history oh
                JOIN company c ON c.id = oh.company_id
                JOIN item i ON i.id = oh.item_id AND i.recording_state = 1
                WHERE oh.recording_state = 1
                  AND oh.approval_status = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendVendorCommonFilters(sql, params, criteria, "oh", false);
        sql.append("""
                 GROUP BY oh.company_id, c.company_name, oh.item_id, i.item_no, i.item_name,
                          oh.fiscal_year, oh.fiscal_month
                 ORDER BY c.company_name, oh.fiscal_year, oh.fiscal_month, i.item_no
                 LIMIT 2000
                """);
        return mapVendorRows(sql.toString(), params);
    }

    private List<VendorPurchaseTotalView> findEtcClaimTotals(VendorPurchaseTotalCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT ec.partner_id AS company_id, c.company_name,
                       NULL AS item_id, '' AS item_no,
                       ec.reason AS item_name,
                       'CLAIM' AS division,
                       0 AS purchase_qty,
                       0 AS unit_price,
                       SUM(-ec.amount) AS amount,
                       ec.fiscal_year, ec.fiscal_month
                FROM etc_claim ec
                JOIN company c ON c.id = ec.partner_id
                WHERE ec.recording_state = 1
                  AND ec.recognition = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria.companyId() != null) {
            sql.append(" AND ec.partner_id = :companyId");
            params.put("companyId", criteria.companyId());
        }
        if (criteria.itemId() != null) {
            sql.append(" AND 1 = 0");
        } else if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND ec.reason LIKE :itemNo");
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND ec.fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND ec.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
        sql.append("""
                 GROUP BY ec.partner_id, c.company_name, ec.reason, ec.fiscal_year, ec.fiscal_month
                 ORDER BY c.company_name, ec.fiscal_year, ec.fiscal_month, ec.reason
                 LIMIT 2000
                """);
        return mapVendorRows(sql.toString(), params);
    }

    private List<VendorPurchaseTotalView> findDefectClaimTotals(VendorPurchaseTotalCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT dc.partner_id AS company_id, c.company_name,
                       dc.item_id, i.item_no, i.item_name,
                       'CLAIM' AS division,
                       SUM(dc.claim_qty) AS purchase_qty,
                       CASE WHEN SUM(dc.claim_qty) = 0 THEN 0
                            ELSE ROUND(SUM(dc.amount) / SUM(dc.claim_qty), 2)
                       END AS unit_price,
                       SUM(-dc.amount) AS amount,
                       dc.fiscal_year, dc.fiscal_month
                FROM defect_claim dc
                JOIN company c ON c.id = dc.partner_id
                JOIN item i ON i.id = dc.item_id AND i.recording_state = 1
                WHERE dc.recording_state = 1
                  AND dc.recognition = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria.companyId() != null) {
            sql.append(" AND dc.partner_id = :companyId");
            params.put("companyId", criteria.companyId());
        }
        if (criteria.itemId() != null) {
            sql.append(" AND dc.item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND (i.item_no LIKE :itemNo OR i.item_name LIKE :itemNo)");
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND dc.fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND dc.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
        sql.append("""
                 GROUP BY dc.partner_id, c.company_name, dc.item_id, i.item_no, i.item_name,
                          dc.fiscal_year, dc.fiscal_month
                 ORDER BY c.company_name, dc.fiscal_year, dc.fiscal_month, i.item_no
                 LIMIT 2000
                """);
        return mapVendorRows(sql.toString(), params);
    }

    private void appendVendorCommonFilters(
            StringBuilder sql,
            Map<String, Object> params,
            VendorPurchaseTotalCriteria criteria,
            String alias,
            boolean allowItemNameFallback
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND ").append(alias).append(".company_id = :companyId");
            params.put("companyId", criteria.companyId());
        }
        if (criteria.itemId() != null) {
            sql.append(" AND ").append(alias).append(".item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            if (allowItemNameFallback) {
                sql.append(" AND (i.item_no LIKE :itemNo OR i.item_name LIKE :itemNo OR ")
                        .append(alias).append(".item_name LIKE :itemNo)");
            } else {
                sql.append(" AND (i.item_no LIKE :itemNo OR i.item_name LIKE :itemNo)");
            }
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND ").append(alias).append(".fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND ").append(alias).append(".fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
    }

    private List<VendorPurchaseTotalView> mapVendorRows(String sql, Map<String, Object> params) {
        Query query = entityManager.createNativeQuery(sql);
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<VendorPurchaseTotalView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new VendorPurchaseTotalView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    row[2] != null ? ((Number) row[2]).longValue() : null,
                    row[3] != null ? row[3].toString() : null,
                    row[4] != null ? row[4].toString() : "",
                    row[5].toString(),
                    toBigDecimal(row[6]),
                    toBigDecimal(row[7]),
                    toBigDecimal(row[8]),
                    ((Number) row[9]).intValue(),
                    ((Number) row[10]).intValue()
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<VendorPurchaseStatusView> findVendorPurchaseStatus(VendorPurchaseStatusCriteria criteria) {
        String division = criteria.division() == null || criteria.division().isBlank()
                ? "ALL"
                : criteria.division().trim().toUpperCase();

        List<VendorPurchaseStatusView> result = new ArrayList<>();
        if ("ALL".equals(division) || "PURCHASE".equals(division) || "ETC".equals(division)) {
            result.addAll(findPurchaseHistoryStatus(criteria, division));
        }
        if ("ALL".equals(division) || "OUTSOURCE".equals(division)) {
            result.addAll(findOutsourceHistoryStatus(criteria));
        }
        if ("ALL".equals(division) || "CLAIM".equals(division)) {
            result.addAll(findEtcClaimStatus(criteria));
            result.addAll(findDefectClaimStatus(criteria));
        }
        result.sort((a, b) -> {
            int c = a.companyName().compareToIgnoreCase(b.companyName());
            if (c != 0) {
                return c;
            }
            c = a.receiptDate().compareTo(b.receiptDate());
            if (c != 0) {
                return c;
            }
            String an = a.itemNo() != null && !a.itemNo().isBlank() ? a.itemNo() : a.itemName();
            String bn = b.itemNo() != null && !b.itemNo().isBlank() ? b.itemNo() : b.itemName();
            c = an.compareToIgnoreCase(bn);
            if (c != 0) {
                return c;
            }
            return Long.compare(a.historyId(), b.historyId());
        });
        return result;
    }

    private List<VendorPurchaseStatusView> findPurchaseHistoryStatus(
            VendorPurchaseStatusCriteria criteria,
            String division
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT ph.id AS history_id,
                       'PURCHASE' AS ledger_kind,
                       ph.company_id,
                       c.company_name,
                       ph.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, ph.item_name, '') AS item_name,
                       COALESCE(i.model_type, '') AS model_type,
                       CASE i.property_classification
                         WHEN '원자재' THEN '소재'
                         WHEN '상품' THEN '최종품'
                         WHEN '제품' THEN '제품'
                         WHEN '공정품' THEN '공정품'
                         ELSE ''
                       END AS process_name,
                       ph.history_date AS receipt_date,
                       COALESCE((
                         SELECT SUM(ib.stock_qty)
                         FROM inventory_balance ib
                         WHERE ib.recording_state = 1
                           AND ib.item_id = ph.item_id
                           AND ib.fiscal_year = ph.fiscal_year
                       ), 0) AS current_stock_qty,
                       ph.purchase_qty AS receipt_qty,
                       ph.unit_price,
                       ph.amount,
                       CASE
                         WHEN ph.source_type = 'ETC_PURCHASE_RECEIPT' THEN 'ETC'
                         ELSE 'PURCHASE'
                       END AS division,
                       ph.fiscal_year,
                       ph.fiscal_month
                FROM purchase_history ph
                JOIN company c ON c.id = ph.company_id AND c.recording_state = 1
                LEFT JOIN item i ON i.id = ph.item_id AND i.recording_state = 1
                WHERE ph.recording_state = 1
                  AND ph.approval_status = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendStatusCommonFilters(sql, params, criteria, "ph", true);
        if ("PURCHASE".equals(division)) {
            sql.append(" AND ph.source_type IN ('PURCHASE_RECEIPT', 'QUALITY_INSPECTION')");
        } else if ("ETC".equals(division)) {
            sql.append(" AND ph.source_type = 'ETC_PURCHASE_RECEIPT'");
        }
        sql.append(" ORDER BY c.company_name, ph.history_date, ph.id LIMIT 5000");
        return mapStatusRows(sql.toString(), params);
    }

    private List<VendorPurchaseStatusView> findOutsourceHistoryStatus(VendorPurchaseStatusCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT oh.id AS history_id,
                       'OUTSOURCE' AS ledger_kind,
                       oh.company_id,
                       c.company_name,
                       oh.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, '') AS item_name,
                       COALESCE(i.model_type, '') AS model_type,
                       COALESCE(CONCAT(pc.small_code, ' ', pc.small_name), '') AS process_name,
                       oh.history_date AS receipt_date,
                       COALESCE((
                         SELECT SUM(ib.stock_qty)
                         FROM inventory_balance ib
                         WHERE ib.recording_state = 1
                           AND ib.item_id = oh.item_id
                           AND ib.fiscal_year = oh.fiscal_year
                       ), 0) AS current_stock_qty,
                       oh.outsource_qty AS receipt_qty,
                       oh.unit_price,
                       oh.amount,
                       'OUTSOURCE' AS division,
                       oh.fiscal_year,
                       oh.fiscal_month
                FROM outsource_history oh
                JOIN company c ON c.id = oh.company_id AND c.recording_state = 1
                LEFT JOIN item i ON i.id = oh.item_id AND i.recording_state = 1
                LEFT JOIN outsourcing_receipt_line orl_direct
                       ON oh.source_type = 'OUTSOURCING_RECEIPT' AND oh.source_id = orl_direct.id
                      AND orl_direct.recording_state = 1
                LEFT JOIN quality_inspection qi
                       ON oh.source_type = 'QUALITY_INSPECTION' AND oh.source_id = qi.id
                      AND qi.recording_state = 1
                LEFT JOIN outsourcing_receipt_line orl_qi
                       ON qi.id IS NOT NULL AND qi.source_receipt_line_id = orl_qi.id
                      AND orl_qi.recording_state = 1
                LEFT JOIN outsourcing_order_line ool
                       ON ool.id = COALESCE(orl_direct.outsourcing_order_line_id, orl_qi.outsourcing_order_line_id)
                LEFT JOIN process_sequence ps ON ps.id = ool.process_sequence_id AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id AND pc.recording_state = 1
                WHERE oh.recording_state = 1
                  AND oh.approval_status = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendStatusCommonFilters(sql, params, criteria, "oh", false);
        sql.append(" ORDER BY c.company_name, oh.history_date, oh.id LIMIT 5000");
        return mapStatusRows(sql.toString(), params);
    }

    private List<VendorPurchaseStatusView> findEtcClaimStatus(VendorPurchaseStatusCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT ec.id AS history_id,
                       'ETC_CLAIM' AS ledger_kind,
                       ec.partner_id AS company_id,
                       c.company_name,
                       NULL AS item_id,
                       '' AS item_no,
                       ec.reason AS item_name,
                       '' AS model_type,
                       '' AS process_name,
                       ec.receipt_date AS receipt_date,
                       0 AS current_stock_qty,
                       0 AS receipt_qty,
                       0 AS unit_price,
                       -ec.amount AS amount,
                       'CLAIM' AS division,
                       ec.fiscal_year,
                       ec.fiscal_month
                FROM etc_claim ec
                JOIN company c ON c.id = ec.partner_id AND c.recording_state = 1
                WHERE ec.recording_state = 1
                  AND ec.recognition = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendClaimStatusFilters(sql, params, criteria);
        sql.append(" ORDER BY c.company_name, ec.receipt_date, ec.id LIMIT 5000");
        return mapStatusRows(sql.toString(), params);
    }

    private List<VendorPurchaseStatusView> findDefectClaimStatus(VendorPurchaseStatusCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT dc.id AS history_id,
                       'DEFECT_CLAIM' AS ledger_kind,
                       dc.partner_id AS company_id,
                       c.company_name,
                       dc.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, '') AS item_name,
                       COALESCE(i.standard, '') AS model_type,
                       '' AS process_name,
                       dc.receipt_date AS receipt_date,
                       0 AS current_stock_qty,
                       dc.claim_qty AS receipt_qty,
                       CASE WHEN dc.claim_qty = 0 THEN 0
                            ELSE ROUND(dc.amount / dc.claim_qty, 2)
                       END AS unit_price,
                       -dc.amount AS amount,
                       'CLAIM' AS division,
                       dc.fiscal_year,
                       dc.fiscal_month
                FROM defect_claim dc
                JOIN company c ON c.id = dc.partner_id AND c.recording_state = 1
                JOIN item i ON i.id = dc.item_id AND i.recording_state = 1
                WHERE dc.recording_state = 1
                  AND dc.recognition = 'APPROVED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendDefectClaimStatusFilters(sql, params, criteria);
        sql.append(" ORDER BY c.company_name, dc.receipt_date, dc.id LIMIT 5000");
        return mapStatusRows(sql.toString(), params);
    }

    private void appendClaimStatusFilters(
            StringBuilder sql,
            Map<String, Object> params,
            VendorPurchaseStatusCriteria criteria
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND ec.partner_id = :companyId");
            params.put("companyId", criteria.companyId());
        } else if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND c.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        if (criteria.itemId() != null) {
            sql.append(" AND 1 = 0");
        } else {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND ec.reason LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND ec.reason LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND ec.receipt_date >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND ec.receipt_date <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND ec.fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND ec.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
    }

    private void appendDefectClaimStatusFilters(
            StringBuilder sql,
            Map<String, Object> params,
            VendorPurchaseStatusCriteria criteria
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND dc.partner_id = :companyId");
            params.put("companyId", criteria.companyId());
        } else if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND c.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        if (criteria.itemId() != null) {
            sql.append(" AND dc.item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND dc.receipt_date >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND dc.receipt_date <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND dc.fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND dc.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
    }

    private void appendStatusCommonFilters(
            StringBuilder sql,
            Map<String, Object> params,
            VendorPurchaseStatusCriteria criteria,
            String alias,
            boolean allowItemNameFallback
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND ").append(alias).append(".company_id = :companyId");
            params.put("companyId", criteria.companyId());
        } else if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND c.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        if (criteria.itemId() != null) {
            sql.append(" AND ").append(alias).append(".item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                if (allowItemNameFallback) {
                    sql.append(" AND (i.item_name LIKE :itemName OR ")
                            .append(alias).append(".item_name LIKE :itemName)");
                } else {
                    sql.append(" AND i.item_name LIKE :itemName");
                }
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
            if (criteria.modelType() != null && !criteria.modelType().isBlank()) {
                sql.append(" AND i.model_type LIKE :modelType");
                params.put("modelType", "%" + criteria.modelType().trim() + "%");
            }
        }
        String dateColumn = alias + ".history_date";
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND ").append(dateColumn).append(" >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND ").append(dateColumn).append(" <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND ").append(alias).append(".fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND ").append(alias).append(".fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
    }

    private List<VendorPurchaseStatusView> mapStatusRows(String sql, Map<String, Object> params) {
        Query query = entityManager.createNativeQuery(sql);
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<VendorPurchaseStatusView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new VendorPurchaseStatusView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    ((Number) row[2]).longValue(),
                    row[3] != null ? row[3].toString() : "",
                    row[4] != null ? ((Number) row[4]).longValue() : null,
                    row[5] != null ? row[5].toString() : "",
                    row[6] != null ? row[6].toString() : "",
                    row[7] != null ? row[7].toString() : "",
                    row[8] != null ? row[8].toString() : "",
                    toLocalDate(row[9]),
                    toBigDecimal(row[10]),
                    toBigDecimal(row[11]),
                    toBigDecimal(row[12]),
                    toBigDecimal(row[13]),
                    row[14].toString(),
                    ((Number) row[15]).intValue(),
                    ((Number) row[16]).intValue()
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PurchaseDailyReportView> findPurchaseDailyReport(PurchaseDailyReportCriteria criteria) {
        String division = criteria.division() == null || criteria.division().isBlank()
                ? "ALL"
                : criteria.division().trim().toUpperCase();

        List<PurchaseDailyReportView> result = new ArrayList<>();
        if ("ALL".equals(division) || "PURCHASE".equals(division) || "ETC".equals(division)) {
            result.addAll(findPurchaseDailyPurchaseRows(criteria, division));
        }
        if ("ALL".equals(division) || "OUTSOURCE".equals(division)) {
            result.addAll(findPurchaseDailyOutsourceRows(criteria));
        }
        if ("ALL".equals(division) || "CLAIM".equals(division)) {
            result.addAll(findPurchaseDailyEtcClaimRows(criteria));
            result.addAll(findPurchaseDailyDefectClaimRows(criteria));
        }
        result.sort((a, b) -> {
            int c = a.companyName().compareToIgnoreCase(b.companyName());
            if (c != 0) {
                return c;
            }
            LocalDate ad = a.receiptDate() != null ? a.receiptDate() : LocalDate.MIN;
            LocalDate bd = b.receiptDate() != null ? b.receiptDate() : LocalDate.MIN;
            c = ad.compareTo(bd);
            if (c != 0) {
                return c;
            }
            return Long.compare(a.historyId(), b.historyId());
        });
        return result;
    }

    private List<PurchaseDailyReportView> findPurchaseDailyPurchaseRows(
            PurchaseDailyReportCriteria criteria,
            String division
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT ph.id AS history_id,
                       'PURCHASE' AS ledger_kind,
                       ph.company_id,
                       c.company_name,
                       ph.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, ph.item_name, '') AS item_name,
                       COALESCE(i.unit, '') AS unit,
                       CASE i.property_classification
                         WHEN '원자재' THEN '소재'
                         WHEN '상품' THEN '최종품'
                         WHEN '제품' THEN '제품'
                         WHEN '공정품' THEN '공정품'
                         ELSE ''
                       END AS process_name,
                       DATE(ph.created_at) AS input_date,
                       ph.history_date AS receipt_date,
                       COALESCE((
                         SELECT SUM(ib.stock_qty)
                         FROM inventory_balance ib
                         WHERE ib.recording_state = 1
                           AND ib.item_id = ph.item_id
                           AND ib.fiscal_year = ph.fiscal_year
                       ), 0) AS current_stock_qty,
                       COALESCE(qi.request_qty, ph.purchase_qty) AS receipt_qty,
                       COALESCE(qi.passed_qty, ph.purchase_qty) AS passed_qty,
                       COALESCE(qi.failed_qty, 0) AS failed_qty,
                       COALESCE((
                         SELECT up.standard_unit_cost
                         FROM unit_price up
                         WHERE up.recording_state = 1
                           AND up.cost_type = 'PURCHASE'
                           AND up.item_id = ph.item_id
                           AND up.company_id = ph.company_id
                           AND up.begin_date <= ph.history_date
                           AND (up.end_date IS NULL OR up.end_date >= ph.history_date)
                         ORDER BY up.begin_date DESC, up.id DESC
                         LIMIT 1
                       ), 0) AS standard_unit_price,
                       ph.unit_price,
                       ph.amount,
                       COALESCE(ppo.offset_amount, 0) AS offset_amount,
                       CASE
                         WHEN ph.approval_status = 'APPROVED'
                         THEN ph.amount - COALESCE(ppo.offset_amount, 0)
                         ELSE 0
                       END AS unpaid_increase,
                       CASE
                         WHEN ph.source_type = 'ETC_PURCHASE_RECEIPT' THEN 'ETC'
                         ELSE 'PURCHASE'
                       END AS division,
                       ph.approval_status,
                       ph.fiscal_year,
                       ph.fiscal_month
                FROM purchase_history ph
                JOIN company c ON c.id = ph.company_id AND c.recording_state = 1
                LEFT JOIN item i ON i.id = ph.item_id AND i.recording_state = 1
                LEFT JOIN quality_inspection qi
                       ON ph.source_type = 'QUALITY_INSPECTION' AND ph.source_id = qi.id
                      AND qi.recording_state = 1
                LEFT JOIN (
                  SELECT history_id, SUM(amount) AS offset_amount
                  FROM partner_prepaid_offset
                  WHERE recording_state = 1
                    AND ledger_kind = 'PURCHASE_HISTORY'
                  GROUP BY history_id
                ) ppo ON ppo.history_id = ph.id
                WHERE ph.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        appendDailyCommonFilters(sql, params, criteria, "ph", true);
        if ("PURCHASE".equals(division)) {
            sql.append(" AND ph.source_type IN ('PURCHASE_RECEIPT', 'QUALITY_INSPECTION')");
        } else if ("ETC".equals(division)) {
            sql.append(" AND ph.source_type = 'ETC_PURCHASE_RECEIPT'");
        }
        sql.append(" ORDER BY c.company_name, ph.history_date, ph.id LIMIT 5000");
        return mapDailyRows(sql.toString(), params);
    }

    private List<PurchaseDailyReportView> findPurchaseDailyOutsourceRows(PurchaseDailyReportCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT oh.id AS history_id,
                       'OUTSOURCE' AS ledger_kind,
                       oh.company_id,
                       c.company_name,
                       oh.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, '') AS item_name,
                       COALESCE(i.unit, '') AS unit,
                       COALESCE(CONCAT(pc.small_code, ' ', pc.small_name), '') AS process_name,
                       DATE(oh.created_at) AS input_date,
                       oh.history_date AS receipt_date,
                       COALESCE((
                         SELECT SUM(ib.stock_qty)
                         FROM inventory_balance ib
                         WHERE ib.recording_state = 1
                           AND ib.item_id = oh.item_id
                           AND ib.fiscal_year = oh.fiscal_year
                       ), 0) AS current_stock_qty,
                       COALESCE(qi.request_qty, oh.outsource_qty) AS receipt_qty,
                       COALESCE(qi.passed_qty, oh.outsource_qty) AS passed_qty,
                       COALESCE(qi.failed_qty, 0) AS failed_qty,
                       COALESCE((
                         SELECT up.standard_unit_cost
                         FROM unit_price up
                         WHERE up.recording_state = 1
                           AND up.cost_type = 'OUTSOURCE'
                           AND up.item_id = oh.item_id
                           AND up.company_id = oh.company_id
                           AND up.begin_date <= oh.history_date
                           AND (up.end_date IS NULL OR up.end_date >= oh.history_date)
                         ORDER BY up.begin_date DESC, up.id DESC
                         LIMIT 1
                       ), 0) AS standard_unit_price,
                       oh.unit_price,
                       oh.amount,
                       COALESCE(ppo.offset_amount, 0) AS offset_amount,
                       CASE
                         WHEN oh.approval_status = 'APPROVED'
                         THEN oh.amount - COALESCE(ppo.offset_amount, 0)
                         ELSE 0
                       END AS unpaid_increase,
                       'OUTSOURCE' AS division,
                       oh.approval_status,
                       oh.fiscal_year,
                       oh.fiscal_month
                FROM outsource_history oh
                JOIN company c ON c.id = oh.company_id AND c.recording_state = 1
                LEFT JOIN item i ON i.id = oh.item_id AND i.recording_state = 1
                LEFT JOIN outsourcing_receipt_line orl_direct
                       ON oh.source_type = 'OUTSOURCING_RECEIPT' AND oh.source_id = orl_direct.id
                      AND orl_direct.recording_state = 1
                LEFT JOIN quality_inspection qi
                       ON oh.source_type = 'QUALITY_INSPECTION' AND oh.source_id = qi.id
                      AND qi.recording_state = 1
                LEFT JOIN outsourcing_receipt_line orl_qi
                       ON qi.id IS NOT NULL AND qi.source_receipt_line_id = orl_qi.id
                      AND orl_qi.recording_state = 1
                LEFT JOIN outsourcing_order_line ool
                       ON ool.id = COALESCE(orl_direct.outsourcing_order_line_id, orl_qi.outsourcing_order_line_id)
                LEFT JOIN process_sequence ps ON ps.id = ool.process_sequence_id AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id AND pc.recording_state = 1
                LEFT JOIN (
                  SELECT history_id, SUM(amount) AS offset_amount
                  FROM partner_prepaid_offset
                  WHERE recording_state = 1
                    AND ledger_kind = 'OUTSOURCE_HISTORY'
                  GROUP BY history_id
                ) ppo ON ppo.history_id = oh.id
                WHERE oh.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        appendDailyCommonFilters(sql, params, criteria, "oh", false);
        sql.append(" ORDER BY c.company_name, oh.history_date, oh.id LIMIT 5000");
        return mapDailyRows(sql.toString(), params);
    }

    private List<PurchaseDailyReportView> findPurchaseDailyEtcClaimRows(PurchaseDailyReportCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT ec.id AS history_id,
                       'ETC_CLAIM' AS ledger_kind,
                       ec.partner_id AS company_id,
                       c.company_name,
                       NULL AS item_id,
                       '' AS item_no,
                       ec.reason AS item_name,
                       '' AS unit,
                       '' AS process_name,
                       DATE(ec.created_at) AS input_date,
                       ec.receipt_date AS receipt_date,
                       0 AS current_stock_qty,
                       0 AS receipt_qty,
                       0 AS passed_qty,
                       0 AS failed_qty,
                       0 AS standard_unit_price,
                       0 AS unit_price,
                       -ec.amount AS amount,
                       0 AS offset_amount,
                       0 AS unpaid_increase,
                       'CLAIM' AS division,
                       CASE WHEN ec.recognition = 'APPROVED' THEN 'APPROVED' ELSE 'PENDING' END AS approval_status,
                       ec.fiscal_year,
                       ec.fiscal_month
                FROM etc_claim ec
                JOIN company c ON c.id = ec.partner_id AND c.recording_state = 1
                WHERE ec.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        appendClaimDailyFilters(sql, params, criteria);
        sql.append(" ORDER BY c.company_name, ec.receipt_date, ec.id LIMIT 5000");
        return mapDailyRows(sql.toString(), params);
    }

    private List<PurchaseDailyReportView> findPurchaseDailyDefectClaimRows(PurchaseDailyReportCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT dc.id AS history_id,
                       'DEFECT_CLAIM' AS ledger_kind,
                       dc.partner_id AS company_id,
                       c.company_name,
                       dc.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, '') AS item_name,
                       COALESCE(i.unit, '') AS unit,
                       '' AS process_name,
                       DATE(dc.created_at) AS input_date,
                       dc.receipt_date AS receipt_date,
                       0 AS current_stock_qty,
                       dc.claim_qty AS receipt_qty,
                       0 AS passed_qty,
                       0 AS failed_qty,
                       COALESCE(i.standard_unit_cost, 0) AS standard_unit_price,
                       CASE WHEN dc.claim_qty = 0 THEN 0
                            ELSE ROUND(dc.amount / dc.claim_qty, 2)
                       END AS unit_price,
                       -dc.amount AS amount,
                       0 AS offset_amount,
                       0 AS unpaid_increase,
                       'CLAIM' AS division,
                       CASE WHEN dc.recognition = 'APPROVED' THEN 'APPROVED' ELSE 'PENDING' END AS approval_status,
                       dc.fiscal_year,
                       dc.fiscal_month
                FROM defect_claim dc
                JOIN company c ON c.id = dc.partner_id AND c.recording_state = 1
                JOIN item i ON i.id = dc.item_id AND i.recording_state = 1
                WHERE dc.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        appendDefectClaimDailyFilters(sql, params, criteria);
        sql.append(" ORDER BY c.company_name, dc.receipt_date, dc.id LIMIT 5000");
        return mapDailyRows(sql.toString(), params);
    }

    private void appendClaimDailyFilters(
            StringBuilder sql,
            Map<String, Object> params,
            PurchaseDailyReportCriteria criteria
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND ec.partner_id = :companyId");
            params.put("companyId", criteria.companyId());
        } else if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND c.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        if (criteria.itemId() != null) {
            sql.append(" AND 1 = 0");
        } else {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND ec.reason LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND ec.reason LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        if (criteria.inputDateFrom() != null) {
            sql.append(" AND DATE(ec.created_at) >= :inputDateFrom");
            params.put("inputDateFrom", criteria.inputDateFrom());
        }
        if (criteria.inputDateTo() != null) {
            sql.append(" AND DATE(ec.created_at) <= :inputDateTo");
            params.put("inputDateTo", criteria.inputDateTo());
        }
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND ec.receipt_date >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND ec.receipt_date <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND ec.fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND ec.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
        if (criteria.approvalStatus() != null && !criteria.approvalStatus().isBlank()
                && !"ALL".equalsIgnoreCase(criteria.approvalStatus().trim())) {
            sql.append(" AND ec.recognition = :recognition");
            params.put("recognition", criteria.approvalStatus().trim().toUpperCase());
        }
    }

    private void appendDefectClaimDailyFilters(
            StringBuilder sql,
            Map<String, Object> params,
            PurchaseDailyReportCriteria criteria
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND dc.partner_id = :companyId");
            params.put("companyId", criteria.companyId());
        } else if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND c.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        if (criteria.itemId() != null) {
            sql.append(" AND dc.item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        if (criteria.inputDateFrom() != null) {
            sql.append(" AND DATE(dc.created_at) >= :inputDateFrom");
            params.put("inputDateFrom", criteria.inputDateFrom());
        }
        if (criteria.inputDateTo() != null) {
            sql.append(" AND DATE(dc.created_at) <= :inputDateTo");
            params.put("inputDateTo", criteria.inputDateTo());
        }
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND dc.receipt_date >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND dc.receipt_date <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND dc.fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND dc.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
        if (criteria.approvalStatus() != null && !criteria.approvalStatus().isBlank()
                && !"ALL".equalsIgnoreCase(criteria.approvalStatus().trim())) {
            sql.append(" AND dc.recognition = :recognition");
            params.put("recognition", criteria.approvalStatus().trim().toUpperCase());
        }
    }

    private void appendDailyCommonFilters(
            StringBuilder sql,
            Map<String, Object> params,
            PurchaseDailyReportCriteria criteria,
            String alias,
            boolean allowItemNameFallback
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND ").append(alias).append(".company_id = :companyId");
            params.put("companyId", criteria.companyId());
        } else if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND c.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        if (criteria.itemId() != null) {
            sql.append(" AND ").append(alias).append(".item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                if (allowItemNameFallback) {
                    sql.append(" AND (i.item_name LIKE :itemName OR ")
                            .append(alias).append(".item_name LIKE :itemName)");
                } else {
                    sql.append(" AND i.item_name LIKE :itemName");
                }
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        if (criteria.inputDateFrom() != null) {
            sql.append(" AND DATE(").append(alias).append(".created_at) >= :inputDateFrom");
            params.put("inputDateFrom", criteria.inputDateFrom());
        }
        if (criteria.inputDateTo() != null) {
            sql.append(" AND DATE(").append(alias).append(".created_at) <= :inputDateTo");
            params.put("inputDateTo", criteria.inputDateTo());
        }
        String dateColumn = alias + ".history_date";
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND ").append(dateColumn).append(" >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND ").append(dateColumn).append(" <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.fiscalYear() != null) {
            sql.append(" AND ").append(alias).append(".fiscal_year = :fiscalYear");
            params.put("fiscalYear", criteria.fiscalYear().shortValue());
        }
        if (criteria.fiscalMonth() != null) {
            sql.append(" AND ").append(alias).append(".fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", criteria.fiscalMonth().byteValue());
        }
        if (criteria.approvalStatus() != null && !criteria.approvalStatus().isBlank()
                && !"ALL".equalsIgnoreCase(criteria.approvalStatus().trim())) {
            sql.append(" AND ").append(alias).append(".approval_status = :approvalStatus");
            params.put("approvalStatus", criteria.approvalStatus().trim().toUpperCase());
        }
    }

    private List<PurchaseDailyReportView> mapDailyRows(String sql, Map<String, Object> params) {
        Query query = entityManager.createNativeQuery(sql);
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<PurchaseDailyReportView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new PurchaseDailyReportView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    ((Number) row[2]).longValue(),
                    row[3] != null ? row[3].toString() : "",
                    row[4] != null ? ((Number) row[4]).longValue() : null,
                    row[5] != null ? row[5].toString() : "",
                    row[6] != null ? row[6].toString() : "",
                    row[7] != null ? row[7].toString() : "",
                    row[8] != null ? row[8].toString() : "",
                    toLocalDate(row[9]),
                    toLocalDate(row[10]),
                    toBigDecimal(row[11]),
                    toBigDecimal(row[12]),
                    toBigDecimal(row[13]),
                    toBigDecimal(row[14]),
                    toBigDecimal(row[15]),
                    toBigDecimal(row[16]),
                    toBigDecimal(row[17]),
                    toBigDecimal(row[18]),
                    toBigDecimal(row[19]),
                    row[20].toString(),
                    row[21] != null ? row[21].toString() : "",
                    ((Number) row[22]).intValue(),
                    ((Number) row[23]).intValue(),
                    BigDecimal.ZERO,
                    BigDecimal.ZERO
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PartnerMonthlyPayableView> findPartnerMonthlyPayable(PartnerMonthlyPayableCriteria criteria) {
        FiscalPeriodDateRange paymentRange = fiscalCalendarService.toCalendarDateRange(
                new FiscalPeriod(criteria.fiscalYear(), criteria.fiscalMonth())
        );
        StringBuilder sql = new StringBuilder("""
                SELECT t.company_id,
                       t.company_name,
                       t.fiscal_year,
                       t.fiscal_month,
                       SUM(t.approved_amount) AS approved_amount,
                       SUM(t.offset_amount) AS offset_amount,
                       SUM(t.payable_amount) AS payable_amount,
                       COALESCE(MAX(pay.paid_amount), 0) AS paid_amount
                FROM (
                  SELECT ph.company_id,
                         c.company_name,
                         ph.fiscal_year,
                         ph.fiscal_month,
                         ph.amount AS approved_amount,
                         COALESCE(ppo.offset_amount, 0) AS offset_amount,
                         ph.amount - COALESCE(ppo.offset_amount, 0) AS payable_amount
                  FROM purchase_history ph
                  JOIN company c ON c.id = ph.company_id AND c.recording_state = 1
                  LEFT JOIN (
                    SELECT history_id, SUM(amount) AS offset_amount
                    FROM partner_prepaid_offset
                    WHERE recording_state = 1
                      AND ledger_kind = 'PURCHASE_HISTORY'
                    GROUP BY history_id
                  ) ppo ON ppo.history_id = ph.id
                  WHERE ph.recording_state = 1
                    AND ph.approval_status = 'APPROVED'
                    AND ph.fiscal_year = :fiscalYear
                    AND ph.fiscal_month = :fiscalMonth
                  UNION ALL
                  SELECT oh.company_id,
                         c.company_name,
                         oh.fiscal_year,
                         oh.fiscal_month,
                         oh.amount AS approved_amount,
                         COALESCE(ppo.offset_amount, 0) AS offset_amount,
                         oh.amount - COALESCE(ppo.offset_amount, 0) AS payable_amount
                  FROM outsource_history oh
                  JOIN company c ON c.id = oh.company_id AND c.recording_state = 1
                  LEFT JOIN (
                    SELECT history_id, SUM(amount) AS offset_amount
                    FROM partner_prepaid_offset
                    WHERE recording_state = 1
                      AND ledger_kind = 'OUTSOURCE_HISTORY'
                    GROUP BY history_id
                  ) ppo ON ppo.history_id = oh.id
                  WHERE oh.recording_state = 1
                    AND oh.approval_status = 'APPROVED'
                    AND oh.fiscal_year = :fiscalYear
                    AND oh.fiscal_month = :fiscalMonth
                ) t
                LEFT JOIN (
                  SELECT partner_id, SUM(supply_amount) AS paid_amount
                  FROM partner_payment
                  WHERE recording_state = 1
                    AND status = 'ISSUED'
                    AND payment_kind = 'NORMAL'
                    AND payment_date >= :paymentDateFrom
                    AND payment_date <= :paymentDateTo
                  GROUP BY partner_id
                ) pay ON pay.partner_id = t.company_id
                WHERE 1 = 1
                """);
        Map<String, Object> params = new HashMap<>();
        params.put("fiscalYear", criteria.fiscalYear());
        params.put("fiscalMonth", criteria.fiscalMonth());
        params.put("paymentDateFrom", Date.valueOf(paymentRange.startInclusive()));
        params.put("paymentDateTo", Date.valueOf(paymentRange.endInclusive()));
        if (criteria.companyId() != null) {
            sql.append(" AND t.company_id = :companyId");
            params.put("companyId", criteria.companyId());
        }
        if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND t.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        sql.append("""
                 GROUP BY t.company_id, t.company_name, t.fiscal_year, t.fiscal_month
                 ORDER BY t.company_name
                 LIMIT 5000
                """);

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<PartnerMonthlyPayableView> result = new ArrayList<>();
        for (Object[] row : rows) {
            BigDecimal payableAmount = toBigDecimal(row[6]);
            BigDecimal paidAmount = toBigDecimal(row[7]);
            BigDecimal unpaidAmount = payableAmount.subtract(paidAmount).max(BigDecimal.ZERO);
            result.add(new PartnerMonthlyPayableView(
                    ((Number) row[0]).longValue(),
                    row[1] != null ? row[1].toString() : "",
                    ((Number) row[2]).intValue(),
                    ((Number) row[3]).intValue(),
                    toBigDecimal(row[4]),
                    toBigDecimal(row[5]),
                    payableAmount,
                    paidAmount,
                    unpaidAmount
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public Map<Long, BigDecimal> sumPurchaseDailyAmountsByCompany(
            PurchaseDailyReportCriteria criteria,
            Collection<Long> companyIds,
            int fiscalYear,
            Integer fiscalMonth
    ) {
        Map<Long, BigDecimal> totals = new HashMap<>();
        if (companyIds == null || companyIds.isEmpty()) {
            return totals;
        }

        String division = criteria.division() == null || criteria.division().isBlank()
                ? "ALL"
                : criteria.division().trim().toUpperCase();

        if ("ALL".equals(division) || "PURCHASE".equals(division) || "ETC".equals(division)) {
            mergeAmountTotals(totals, sumPurchaseDailyPurchaseAmounts(criteria, companyIds, fiscalYear, fiscalMonth, division));
        }
        if ("ALL".equals(division) || "OUTSOURCE".equals(division)) {
            mergeAmountTotals(totals, sumPurchaseDailyOutsourceAmounts(criteria, companyIds, fiscalYear, fiscalMonth));
        }
        if ("ALL".equals(division) || "CLAIM".equals(division)) {
            mergeAmountTotals(totals, sumPurchaseDailyEtcClaimAmounts(criteria, companyIds, fiscalYear, fiscalMonth));
            mergeAmountTotals(totals, sumPurchaseDailyDefectClaimAmounts(criteria, companyIds, fiscalYear, fiscalMonth));
        }
        return totals;
    }

    private Map<Long, BigDecimal> sumPurchaseDailyPurchaseAmounts(
            PurchaseDailyReportCriteria criteria,
            Collection<Long> companyIds,
            int fiscalYear,
            Integer fiscalMonth,
            String division
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT ph.company_id, COALESCE(SUM(ph.amount), 0)
                FROM purchase_history ph
                WHERE ph.recording_state = 1
                  AND ph.company_id IN (:companyIds)
                  AND ph.fiscal_year = :fiscalYear
                """);
        Map<String, Object> params = new HashMap<>();
        params.put("companyIds", companyIds);
        params.put("fiscalYear", (short) fiscalYear);
        if (fiscalMonth != null) {
            sql.append(" AND ph.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", fiscalMonth.byteValue());
        }
        if ("PURCHASE".equals(division)) {
            sql.append(" AND ph.source_type IN ('PURCHASE_RECEIPT', 'QUALITY_INSPECTION')");
        } else if ("ETC".equals(division)) {
            sql.append(" AND ph.source_type = 'ETC_PURCHASE_RECEIPT'");
        }
        if (criteria.approvalStatus() != null && !criteria.approvalStatus().isBlank()
                && !"ALL".equalsIgnoreCase(criteria.approvalStatus().trim())) {
            sql.append(" AND ph.approval_status = :approvalStatus");
            params.put("approvalStatus", criteria.approvalStatus().trim().toUpperCase());
        }
        sql.append(" GROUP BY ph.company_id");
        return queryAmountTotals(sql.toString(), params);
    }

    private Map<Long, BigDecimal> sumPurchaseDailyOutsourceAmounts(
            PurchaseDailyReportCriteria criteria,
            Collection<Long> companyIds,
            int fiscalYear,
            Integer fiscalMonth
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT oh.company_id, COALESCE(SUM(oh.amount), 0)
                FROM outsource_history oh
                WHERE oh.recording_state = 1
                  AND oh.company_id IN (:companyIds)
                  AND oh.fiscal_year = :fiscalYear
                """);
        Map<String, Object> params = new HashMap<>();
        params.put("companyIds", companyIds);
        params.put("fiscalYear", (short) fiscalYear);
        if (fiscalMonth != null) {
            sql.append(" AND oh.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", fiscalMonth.byteValue());
        }
        if (criteria.approvalStatus() != null && !criteria.approvalStatus().isBlank()
                && !"ALL".equalsIgnoreCase(criteria.approvalStatus().trim())) {
            sql.append(" AND oh.approval_status = :approvalStatus");
            params.put("approvalStatus", criteria.approvalStatus().trim().toUpperCase());
        }
        sql.append(" GROUP BY oh.company_id");
        return queryAmountTotals(sql.toString(), params);
    }

    private Map<Long, BigDecimal> sumPurchaseDailyEtcClaimAmounts(
            PurchaseDailyReportCriteria criteria,
            Collection<Long> companyIds,
            int fiscalYear,
            Integer fiscalMonth
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT ec.partner_id, COALESCE(SUM(-ec.amount), 0)
                FROM etc_claim ec
                WHERE ec.recording_state = 1
                  AND ec.partner_id IN (:companyIds)
                  AND ec.fiscal_year = :fiscalYear
                """);
        Map<String, Object> params = new HashMap<>();
        params.put("companyIds", companyIds);
        params.put("fiscalYear", (short) fiscalYear);
        if (fiscalMonth != null) {
            sql.append(" AND ec.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", fiscalMonth.byteValue());
        }
        if (criteria.approvalStatus() != null && !criteria.approvalStatus().isBlank()
                && !"ALL".equalsIgnoreCase(criteria.approvalStatus().trim())) {
            sql.append(" AND ec.recognition = :recognition");
            params.put("recognition", criteria.approvalStatus().trim().toUpperCase());
        }
        sql.append(" GROUP BY ec.partner_id");
        return queryAmountTotals(sql.toString(), params);
    }

    private Map<Long, BigDecimal> sumPurchaseDailyDefectClaimAmounts(
            PurchaseDailyReportCriteria criteria,
            Collection<Long> companyIds,
            int fiscalYear,
            Integer fiscalMonth
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT dc.partner_id, COALESCE(SUM(-dc.amount), 0)
                FROM defect_claim dc
                WHERE dc.recording_state = 1
                  AND dc.partner_id IN (:companyIds)
                  AND dc.fiscal_year = :fiscalYear
                """);
        Map<String, Object> params = new HashMap<>();
        params.put("companyIds", companyIds);
        params.put("fiscalYear", (short) fiscalYear);
        if (fiscalMonth != null) {
            sql.append(" AND dc.fiscal_month = :fiscalMonth");
            params.put("fiscalMonth", fiscalMonth.byteValue());
        }
        if (criteria.approvalStatus() != null && !criteria.approvalStatus().isBlank()
                && !"ALL".equalsIgnoreCase(criteria.approvalStatus().trim())) {
            sql.append(" AND dc.recognition = :recognition");
            params.put("recognition", criteria.approvalStatus().trim().toUpperCase());
        }
        sql.append(" GROUP BY dc.partner_id");
        return queryAmountTotals(sql.toString(), params);
    }

    private Map<Long, BigDecimal> queryAmountTotals(String sql, Map<String, Object> params) {
        Query query = entityManager.createNativeQuery(sql);
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        Map<Long, BigDecimal> result = new HashMap<>();
        for (Object[] row : rows) {
            result.put(((Number) row[0]).longValue(), toBigDecimal(row[1]));
        }
        return result;
    }

    private static void mergeAmountTotals(Map<Long, BigDecimal> target, Map<Long, BigDecimal> source) {
        for (Map.Entry<Long, BigDecimal> entry : source.entrySet()) {
            target.merge(entry.getKey(), entry.getValue(), BigDecimal::add);
        }
    }

    @Override
    @Transactional(readOnly = true)
    public List<WarehouseMonthlyIoView> findWarehouseMonthlyIo(WarehouseMonthlyIoCriteria criteria) {
        int year = criteria.fiscalYear();
        int month = criteria.fiscalMonth();
        int prevYear = month == 1 ? year - 1 : year;
        int prevMonth = month == 1 ? 12 : month - 1;

        StringBuilder sql = new StringBuilder("""
                SELECT ib.item_id, i.item_no, i.item_name,
                       il.location_code, il.location_name,
                       ps.process_sequence, pc.small_name,
                       :fiscalYear AS fiscal_year, :fiscalMonth AS fiscal_month,
                       COALESCE(prev.stock_qty, 0) AS carry_in_qty,
                       COALESCE(cur.in_qty, 0) AS in_qty,
                       COALESCE(cur.out_qty, 0) AS out_qty,
                       COALESCE(cur.stock_qty, COALESCE(prev.stock_qty, 0)) AS ending_qty
                FROM inventory_balance ib
                JOIN item i ON i.id = ib.item_id
                JOIN inventory_location il ON il.id = ib.location_id
                LEFT JOIN process_sequence ps ON ps.id = ib.output_process_id AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                LEFT JOIN inventory_balance_monthly cur
                  ON cur.inventory_balance_id = ib.id
                 AND cur.month_num = :fiscalMonth
                 AND cur.recording_state = 1
                LEFT JOIN inventory_balance prev_ib
                  ON :fiscalMonth = 1
                 AND prev_ib.item_id = ib.item_id
                 AND prev_ib.location_id = ib.location_id
                 AND prev_ib.fiscal_year = :prevYear
                 AND ((prev_ib.output_process_id IS NULL AND ib.output_process_id IS NULL)
                      OR prev_ib.output_process_id = ib.output_process_id)
                 AND ((prev_ib.partner_id IS NULL AND ib.partner_id IS NULL)
                      OR prev_ib.partner_id = ib.partner_id)
                 AND prev_ib.recording_state = 1
                LEFT JOIN inventory_balance_monthly prev
                  ON prev.recording_state = 1
                 AND prev.month_num = :prevMonth
                 AND (
                      (:fiscalMonth > 1 AND prev.inventory_balance_id = ib.id)
                   OR (:fiscalMonth = 1 AND prev.inventory_balance_id = prev_ib.id)
                 )
                WHERE ib.recording_state = 1
                  AND ib.fiscal_year = :fiscalYear
                  AND il.location_code = :locationCode
                """);
        Map<String, Object> params = new HashMap<>();
        params.put("fiscalYear", (short) year);
        params.put("fiscalMonth", (byte) month);
        params.put("prevYear", (short) prevYear);
        params.put("prevMonth", (byte) prevMonth);
        params.put("locationCode", criteria.locationCode());

        if (criteria.itemId() != null) {
            sql.append(" AND ib.item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND (i.item_no LIKE :itemNo OR i.item_name LIKE :itemNo)");
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        sql.append("""
                  AND (COALESCE(cur.in_qty, 0) <> 0
                    OR COALESCE(cur.out_qty, 0) <> 0
                    OR COALESCE(cur.stock_qty, 0) <> 0
                    OR COALESCE(prev.stock_qty, 0) <> 0
                    OR ib.stock_qty <> 0)
                 ORDER BY i.item_no, ps.process_sequence, ib.id
                 LIMIT 2000
                """);

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<WarehouseMonthlyIoView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new WarehouseMonthlyIoView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    row[2].toString(),
                    row[3].toString(),
                    row[4].toString(),
                    toInteger(row[5]),
                    row[6] != null ? row[6].toString() : null,
                    ((Number) row[7]).intValue(),
                    ((Number) row[8]).intValue(),
                    toBigDecimal(row[9]),
                    toBigDecimal(row[10]),
                    toBigDecimal(row[11]),
                    toBigDecimal(row[12])
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<ItemStockMovementView> findItemStockMovements(ItemStockMovementCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT sm.id, sm.item_id, i.item_no, i.item_name,
                       il.location_code, il.location_name,
                       ps.process_sequence, pc.small_name,
                       sm.partner_id, c.company_name,
                       sm.movement_type, sm.qty, sm.amount,
                       sm.reference_type, sm.reference_id, sm.movement_date
                FROM stock_movement sm
                JOIN item i ON i.id = sm.item_id
                JOIN inventory_location il ON il.id = sm.location_id
                JOIN inventory_balance ib ON ib.id = sm.inventory_balance_id
                LEFT JOIN process_sequence ps ON ps.id = COALESCE(sm.output_process_id, ib.output_process_id)
                        AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                LEFT JOIN company c ON c.id = sm.partner_id
                WHERE sm.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria.itemId() != null) {
            sql.append(" AND sm.item_id = :itemId");
            params.put("itemId", criteria.itemId());
        } else if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND (i.item_no LIKE :itemNo OR i.item_name LIKE :itemNo)");
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.companyId() != null) {
            sql.append(" AND sm.partner_id = :companyId");
            params.put("companyId", criteria.companyId());
        }
        if (criteria.locationCode() != null && !criteria.locationCode().isBlank()) {
            sql.append(" AND il.location_code = :locationCode");
            params.put("locationCode", criteria.locationCode().trim());
        }
        if (criteria.outputProcessId() != null) {
            sql.append(" AND COALESCE(sm.output_process_id, ib.output_process_id) = :outputProcessId");
            params.put("outputProcessId", criteria.outputProcessId());
        }
        if (criteria.movementDateFrom() != null) {
            sql.append(" AND sm.movement_date >= :movementDateFrom");
            params.put("movementDateFrom", criteria.movementDateFrom());
        }
        if (criteria.movementDateTo() != null) {
            sql.append(" AND sm.movement_date <= :movementDateTo");
            params.put("movementDateTo", criteria.movementDateTo());
        }
        sql.append(" ORDER BY sm.movement_date DESC, sm.id DESC LIMIT 2000");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<ItemStockMovementView> result = new ArrayList<>();
        for (Object[] row : rows) {
            String movementType = row[10].toString();
            BigDecimal qty = toBigDecimal(row[11]);
            BigDecimal inQty = BigDecimal.ZERO;
            BigDecimal outQty = BigDecimal.ZERO;
            if ("IN".equals(movementType) || ("ADJUST".equals(movementType) && qty.signum() >= 0)) {
                inQty = qty.abs();
            } else if ("OUT".equals(movementType) || ("ADJUST".equals(movementType) && qty.signum() < 0)) {
                outQty = qty.abs();
            }
            result.add(new ItemStockMovementView(
                    ((Number) row[0]).longValue(),
                    ((Number) row[1]).longValue(),
                    row[2].toString(),
                    row[3].toString(),
                    row[4].toString(),
                    row[5].toString(),
                    toInteger(row[6]),
                    row[7] != null ? row[7].toString() : null,
                    row[8] != null ? ((Number) row[8]).longValue() : null,
                    row[9] != null ? row[9].toString() : null,
                    movementType,
                    inQty,
                    outQty,
                    toBigDecimal(row[12]),
                    row[13].toString(),
                    ((Number) row[14]).longValue(),
                    toLocalDate(row[15])
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<OrderVsReceiptView> findOrderVsReceipt(OrderVsReceiptCriteria criteria) {
        String division = criteria.division() == null || criteria.division().isBlank()
                ? "ALL"
                : criteria.division().trim().toUpperCase();

        List<OrderVsReceiptView> result = new ArrayList<>();
        if ("ALL".equals(division) || "PURCHASE".equals(division)) {
            result.addAll(findPurchaseOrderVsReceipt(criteria));
        }
        if ("ALL".equals(division) || "OUTSOURCE".equals(division)) {
            result.addAll(findOutsourceOrderVsReceipt(criteria));
        }
        result.sort((a, b) -> {
            int c = a.companyName().compareToIgnoreCase(b.companyName());
            if (c != 0) {
                return c;
            }
            c = b.orderDate().compareTo(a.orderDate());
            if (c != 0) {
                return c;
            }
            String an = a.itemNo() != null ? a.itemNo() : "";
            String bn = b.itemNo() != null ? b.itemNo() : "";
            return an.compareToIgnoreCase(bn);
        });
        if (result.size() > 5000) {
            return new ArrayList<>(result.subList(0, 5000));
        }
        return result;
    }

    private List<OrderVsReceiptView> findPurchaseOrderVsReceipt(OrderVsReceiptCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT pol.id AS order_line_id,
                       'PURCHASE' AS division,
                       po.partner_id AS company_id,
                       c.company_name,
                       pol.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, '') AS item_name,
                       COALESCE(i.model_type, '') AS model_type,
                       CASE WHEN i.property_classification = '원자재' THEN '소재'
                            ELSE COALESCE(i.property_classification, '')
                       END AS process_name,
                       COALESCE(i.unit, '') AS unit,
                       COALESCE(i.standard, '') AS standard,
                       po.order_date,
                       pol.order_qty,
                       pol.unit_price AS order_unit_price,
                       pol.amount AS order_amount,
                       pol.requested_delivery_date,
                       (
                         SELECT MAX(pr.receipt_date)
                         FROM purchase_receipt_line prl
                         JOIN purchase_receipt pr ON pr.id = prl.purchase_receipt_id
                         WHERE prl.purchase_order_line_id = pol.id
                           AND prl.recording_state = 1
                           AND pr.recording_state = 1
                           AND pr.status <> 'CANCELLED'
                       ) AS last_receipt_date,
                       pol.received_qty,
                       pol.waiting_inspection_qty,
                       (pol.received_qty * pol.unit_price) AS receipt_amount,
                       (pol.order_qty - pol.received_qty - pol.waiting_inspection_qty) AS remain_qty,
                       ((pol.order_qty - pol.received_qty - pol.waiting_inspection_qty) * pol.unit_price)
                         AS remain_amount,
                       COALESCE((
                         SELECT SUM(ib.stock_qty)
                         FROM inventory_balance ib
                         WHERE ib.item_id = pol.item_id
                           AND ib.recording_state = 1
                       ), 0) AS current_stock_qty
                FROM purchase_order_line pol
                JOIN purchase_order po ON po.id = pol.purchase_order_id
                JOIN company c ON c.id = po.partner_id AND c.recording_state = 1
                JOIN item i ON i.id = pol.item_id AND i.recording_state = 1
                WHERE pol.recording_state = 1
                  AND po.recording_state = 1
                  AND po.status <> 'CANCELLED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendOrderVsReceiptFilters(
                sql, params, criteria,
                "po.partner_id", "po.order_date",
                "purchase_receipt_line", "purchase_receipt", "purchase_receipt_id",
                "purchase_order_line_id", "pol.id"
        );
        sql.append(" ORDER BY c.company_name, po.order_date DESC, i.item_no LIMIT 5000");
        return mapOrderVsReceiptRows(sql.toString(), params);
    }

    private List<OrderVsReceiptView> findOutsourceOrderVsReceipt(OrderVsReceiptCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT ool.id AS order_line_id,
                       'OUTSOURCE' AS division,
                       oo.partner_id AS company_id,
                       c.company_name,
                       ool.item_id,
                       COALESCE(i.item_no, '') AS item_no,
                       COALESCE(i.item_name, '') AS item_name,
                       COALESCE(i.model_type, '') AS model_type,
                       COALESCE(CONCAT(pc.small_code, ' ', pc.small_name), '') AS process_name,
                       COALESCE(i.unit, '') AS unit,
                       COALESCE(i.standard, '') AS standard,
                       oo.order_date,
                       ool.order_qty,
                       ool.unit_price AS order_unit_price,
                       ool.amount AS order_amount,
                       ool.requested_delivery_date,
                       (
                         SELECT MAX(orr.receipt_date)
                         FROM outsourcing_receipt_line orl
                         JOIN outsourcing_receipt orr ON orr.id = orl.outsourcing_receipt_id
                         WHERE orl.outsourcing_order_line_id = ool.id
                           AND orl.recording_state = 1
                           AND orr.recording_state = 1
                           AND orr.status <> 'CANCELLED'
                       ) AS last_receipt_date,
                       ool.received_qty,
                       ool.waiting_inspection_qty,
                       (ool.received_qty * ool.unit_price) AS receipt_amount,
                       (ool.order_qty - ool.received_qty - ool.waiting_inspection_qty) AS remain_qty,
                       ((ool.order_qty - ool.received_qty - ool.waiting_inspection_qty) * ool.unit_price)
                         AS remain_amount,
                       COALESCE((
                         SELECT SUM(ib.stock_qty)
                         FROM inventory_balance ib
                         WHERE ib.item_id = ool.item_id
                           AND ib.recording_state = 1
                       ), 0) AS current_stock_qty
                FROM outsourcing_order_line ool
                JOIN outsourcing_order oo ON oo.id = ool.outsourcing_order_id
                JOIN company c ON c.id = oo.partner_id AND c.recording_state = 1
                JOIN item i ON i.id = ool.item_id AND i.recording_state = 1
                LEFT JOIN process_sequence ps ON ps.id = ool.process_sequence_id AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id AND pc.recording_state = 1
                WHERE ool.recording_state = 1
                  AND oo.recording_state = 1
                  AND oo.status <> 'CANCELLED'
                """);
        Map<String, Object> params = new HashMap<>();
        appendOrderVsReceiptFilters(
                sql, params, criteria,
                "oo.partner_id", "oo.order_date",
                "outsourcing_receipt_line", "outsourcing_receipt", "outsourcing_receipt_id",
                "outsourcing_order_line_id", "ool.id"
        );
        sql.append(" ORDER BY c.company_name, oo.order_date DESC, i.item_no LIMIT 5000");
        return mapOrderVsReceiptRows(sql.toString(), params);
    }

    private void appendOrderVsReceiptFilters(
            StringBuilder sql,
            Map<String, Object> params,
            OrderVsReceiptCriteria criteria,
            String companyIdColumn,
            String orderDateColumn,
            String receiptLineTable,
            String receiptHeaderTable,
            String receiptHeaderFk,
            String orderLineFk,
            String orderLineIdExpr
    ) {
        if (criteria.companyId() != null) {
            sql.append(" AND ").append(companyIdColumn).append(" = :companyId");
            params.put("companyId", criteria.companyId());
        } else if (criteria.companyName() != null && !criteria.companyName().isBlank()) {
            sql.append(" AND c.company_name LIKE :companyName");
            params.put("companyName", "%" + criteria.companyName().trim() + "%");
        }
        if (criteria.itemId() != null) {
            sql.append(" AND i.id = :itemId");
            params.put("itemId", criteria.itemId());
        } else {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        if (criteria.orderDateFrom() != null) {
            sql.append(" AND ").append(orderDateColumn).append(" >= :orderDateFrom");
            params.put("orderDateFrom", criteria.orderDateFrom());
        }
        if (criteria.orderDateTo() != null) {
            sql.append(" AND ").append(orderDateColumn).append(" <= :orderDateTo");
            params.put("orderDateTo", criteria.orderDateTo());
        }
        boolean hasReceiptDateFilter = criteria.receiptDateFrom() != null || criteria.receiptDateTo() != null;
        if (hasReceiptDateFilter) {
            sql.append(" AND EXISTS (")
                    .append(" SELECT 1 FROM ").append(receiptLineTable).append(" rfl")
                    .append(" JOIN ").append(receiptHeaderTable).append(" rfh")
                    .append(" ON rfh.id = rfl.").append(receiptHeaderFk)
                    .append(" WHERE rfl.").append(orderLineFk).append(" = ").append(orderLineIdExpr)
                    .append(" AND rfl.recording_state = 1")
                    .append(" AND rfh.recording_state = 1")
                    .append(" AND rfh.status <> 'CANCELLED'");
            if (criteria.receiptDateFrom() != null) {
                sql.append(" AND rfh.receipt_date >= :receiptDateFrom");
                params.put("receiptDateFrom", criteria.receiptDateFrom());
            }
            if (criteria.receiptDateTo() != null) {
                sql.append(" AND rfh.receipt_date <= :receiptDateTo");
                params.put("receiptDateTo", criteria.receiptDateTo());
            }
            sql.append(")");
        }
    }

    private List<OrderVsReceiptView> mapOrderVsReceiptRows(String sql, Map<String, Object> params) {
        Query query = entityManager.createNativeQuery(sql);
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<OrderVsReceiptView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new OrderVsReceiptView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    ((Number) row[2]).longValue(),
                    row[3].toString(),
                    ((Number) row[4]).longValue(),
                    row[5] != null ? row[5].toString() : "",
                    row[6] != null ? row[6].toString() : "",
                    row[7] != null ? row[7].toString() : "",
                    row[8] != null ? row[8].toString() : "",
                    row[9] != null ? row[9].toString() : "",
                    row[10] != null ? row[10].toString() : "",
                    toLocalDate(row[11]),
                    toBigDecimal(row[12]),
                    toBigDecimal(row[13]),
                    toBigDecimal(row[14]),
                    toLocalDate(row[15]),
                    toLocalDate(row[16]),
                    toBigDecimal(row[17]),
                    toBigDecimal(row[18]),
                    toBigDecimal(row[19]),
                    toBigDecimal(row[20]),
                    toBigDecimal(row[21]),
                    toBigDecimal(row[22])
            ));
        }
        return result;
    }

    private static BigDecimal toBigDecimal(Object value) {
        if (value == null) {
            return BigDecimal.ZERO;
        }
        if (value instanceof BigDecimal bd) {
            return bd;
        }
        return new BigDecimal(value.toString());
    }

    private static Integer toInteger(Object value) {
        if (value == null) {
            return null;
        }
        return ((Number) value).intValue();
    }

    private static LocalDate toLocalDate(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof LocalDate localDate) {
            return localDate;
        }
        if (value instanceof Date date) {
            return date.toLocalDate();
        }
        return LocalDate.parse(value.toString());
    }
}
