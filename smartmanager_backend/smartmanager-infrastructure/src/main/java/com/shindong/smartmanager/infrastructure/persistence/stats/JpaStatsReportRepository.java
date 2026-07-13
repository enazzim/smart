package com.shindong.smartmanager.infrastructure.persistence.stats;

import com.shindong.smartmanager.application.stats.ItemStockMovementCriteria;
import com.shindong.smartmanager.application.stats.ItemStockMovementView;
import com.shindong.smartmanager.application.stats.StatsReportRepository;
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
