package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.InventoryBalanceLedgerView;
import com.shindong.smartmanager.application.inventory.InventoryBalanceListCriteria;
import com.shindong.smartmanager.application.inventory.InventoryBalanceMonthlyView;
import com.shindong.smartmanager.application.inventory.InventoryLedgerRepository;
import com.shindong.smartmanager.application.inventory.StockMovementListCriteria;
import com.shindong.smartmanager.application.inventory.StockMovementListItemView;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.sql.Date;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaInventoryLedgerRepository implements InventoryLedgerRepository {

    private static final int ACTIVE = 1;
    private static final BigDecimal[] ZERO_PAIR = { BigDecimal.ZERO, BigDecimal.ZERO };

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    @Transactional(readOnly = true)
    public List<StockMovementListItemView> findStockMovements(StockMovementListCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT sm.id, sm.item_id, i.item_no, i.item_name,
                       il.location_code, il.location_name,
                       ps.process_sequence, pc.small_name,
                       sm.movement_type, sm.qty, sm.amount,
                       sm.reference_type, sm.reference_id,
                       sm.movement_date, sm.fiscal_year, sm.fiscal_month
                FROM stock_movement sm
                JOIN item i ON i.id = sm.item_id
                JOIN inventory_location il ON il.id = sm.location_id
                JOIN inventory_balance ib ON ib.id = sm.inventory_balance_id
                LEFT JOIN process_sequence ps ON ps.id = COALESCE(sm.output_process_id, ib.output_process_id)
                        AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                WHERE sm.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.itemId() != null) {
                sql.append(" AND sm.item_id = :itemId");
                params.put("itemId", criteria.itemId());
            } else if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND (i.item_no LIKE :itemNo OR i.item_name LIKE :itemNo)");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.locationCode() != null && !criteria.locationCode().isBlank()) {
                sql.append(" AND il.location_code = :locationCode");
                params.put("locationCode", criteria.locationCode().trim());
            }
            if (criteria.referenceType() != null && !criteria.referenceType().isBlank()) {
                sql.append(" AND sm.reference_type = :referenceType");
                params.put("referenceType", criteria.referenceType().trim());
            }
            if (criteria.movementDateFrom() != null) {
                sql.append(" AND sm.movement_date >= :movementDateFrom");
                params.put("movementDateFrom", criteria.movementDateFrom());
            }
            if (criteria.movementDateTo() != null) {
                sql.append(" AND sm.movement_date <= :movementDateTo");
                params.put("movementDateTo", criteria.movementDateTo());
            }
        }
        sql.append(" ORDER BY sm.movement_date DESC, sm.id DESC LIMIT 500");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();

        List<StockMovementListItemView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new StockMovementListItemView(
                    ((Number) row[0]).longValue(),
                    ((Number) row[1]).longValue(),
                    row[2].toString(),
                    row[3].toString(),
                    row[4].toString(),
                    row[5].toString(),
                    toInteger(row[6]),
                    row[7] != null ? row[7].toString() : null,
                    StockMovementType.valueOf(row[8].toString()),
                    toBigDecimal(row[9]),
                    toBigDecimal(row[10]),
                    row[11].toString(),
                    ((Number) row[12]).longValue(),
                    toLocalDate(row[13]),
                    ((Number) row[14]).intValue(),
                    ((Number) row[15]).intValue()
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<InventoryBalanceLedgerView> findBalances(InventoryBalanceListCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT ib.id, ib.item_id, i.item_no, i.item_name,
                       il.location_code, il.location_name,
                       ps.process_sequence, pc.small_name,
                       ib.fiscal_year,
                       (SELECT COALESCE(SUM(prev.stock_qty), 0)
                          FROM inventory_balance prev
                         WHERE prev.recording_state = 1
                           AND prev.item_id = ib.item_id
                           AND prev.location_id = ib.location_id
                           AND prev.fiscal_year <= ib.fiscal_year
                           AND prev.output_process_id <=> ib.output_process_id
                           AND prev.input_process_id <=> ib.input_process_id
                           AND prev.partner_id <=> ib.partner_id) AS stock_qty,
                       ib.stock_amount
                FROM inventory_balance ib
                JOIN item i ON i.id = ib.item_id
                JOIN inventory_location il ON il.id = ib.location_id
                LEFT JOIN process_sequence ps ON ps.id = ib.output_process_id AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                WHERE ib.recording_state = 1
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.itemId() != null) {
                sql.append(" AND ib.item_id = :itemId");
                params.put("itemId", criteria.itemId());
            } else if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND (i.item_no LIKE :itemNo OR i.item_name LIKE :itemNo)");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.locationCode() != null && !criteria.locationCode().isBlank()) {
                sql.append(" AND il.location_code = :locationCode");
                params.put("locationCode", criteria.locationCode().trim());
            }
            if (criteria.fiscalYear() != null) {
                sql.append(" AND ib.fiscal_year = :fiscalYear");
                params.put("fiscalYear", criteria.fiscalYear().shortValue());
            }
        }
        sql.append(" AND ((SELECT COALESCE(SUM(prev.stock_qty), 0)")
                .append(" FROM inventory_balance prev")
                .append(" WHERE prev.recording_state = 1")
                .append(" AND prev.item_id = ib.item_id")
                .append(" AND prev.location_id = ib.location_id")
                .append(" AND prev.fiscal_year <= ib.fiscal_year")
                .append(" AND prev.output_process_id <=> ib.output_process_id")
                .append(" AND prev.input_process_id <=> ib.input_process_id")
                .append(" AND prev.partner_id <=> ib.partner_id) <> 0")
                .append(" OR EXISTS (SELECT 1 FROM inventory_balance_monthly m")
                .append(" WHERE m.inventory_balance_id = ib.id AND m.recording_state = 1")
                .append(" AND (m.in_qty <> 0 OR m.out_qty <> 0)))");
        sql.append(" ORDER BY i.item_no, il.location_code, ps.process_sequence, ib.id");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();

        Map<Long, InventoryBalanceLedgerView> balanceMap = new LinkedHashMap<>();
        for (Object[] row : rows) {
            long balanceId = ((Number) row[0]).longValue();
            balanceMap.put(balanceId, new InventoryBalanceLedgerView(
                    balanceId,
                    ((Number) row[1]).longValue(),
                    row[2].toString(),
                    row[3].toString(),
                    row[4].toString(),
                    row[5].toString(),
                    toInteger(row[6]),
                    row[7] != null ? row[7].toString() : null,
                    ((Number) row[8]).intValue(),
                    toBigDecimal(row[9]),
                    toBigDecimal(row[10]),
                    loadMonthly(balanceId)
            ));
        }
        return new ArrayList<>(balanceMap.values());
    }

    private List<InventoryBalanceMonthlyView> loadMonthly(long balanceId) {
        String sql = """
                SELECT month_num, in_qty, out_qty, stock_qty
                FROM inventory_balance_monthly
                WHERE inventory_balance_id = :balanceId AND recording_state = 1
                ORDER BY month_num
                """;
        @SuppressWarnings("unchecked")
        List<Object[]> rows = entityManager.createNativeQuery(sql)
                .setParameter("balanceId", balanceId)
                .getResultList();
        Map<Integer, BigDecimal[]> voided = loadVoidedMonthlyQty(balanceId);
        List<InventoryBalanceMonthlyView> months = new ArrayList<>();
        for (Object[] row : rows) {
            int monthNum = ((Number) row[0]).intValue();
            BigDecimal[] excluded = voided.getOrDefault(monthNum, ZERO_PAIR);
            months.add(new InventoryBalanceMonthlyView(
                    monthNum,
                    floorZero(toBigDecimal(row[1]).subtract(excluded[0])),
                    floorZero(toBigDecimal(row[2]).subtract(excluded[1])),
                    toBigDecimal(row[3])
            ));
        }
        return months;
    }

    /**
     * 취소된 원래 이동과 취소로 생긴 반대 이동. 재고 잔고의 월별 입고·출고에서만 뺀다.
     * 월말 재고와 입출고 이력은 그대로 둔다.
     */
    private Map<Integer, BigDecimal[]> loadVoidedMonthlyQty(long balanceId) {
        String sql = """
                SELECT sm.fiscal_month,
                       SUM(CASE
                             WHEN sm.movement_type = 'IN' THEN sm.qty
                             WHEN sm.movement_type = 'ADJUST' AND sm.qty > 0 THEN sm.qty
                             ELSE 0 END),
                       SUM(CASE
                             WHEN sm.movement_type = 'OUT' THEN sm.qty
                             WHEN sm.movement_type = 'ADJUST' AND sm.qty < 0 THEN ABS(sm.qty)
                             ELSE 0 END)
                FROM stock_movement sm
                WHERE sm.inventory_balance_id = :balanceId
                  AND sm.recording_state = 1
                  AND (
                    RIGHT(sm.reference_type, 7) = '_CANCEL'
                    OR EXISTS (
                      SELECT 1 FROM stock_movement c
                      WHERE c.recording_state = 1
                        AND c.inventory_balance_id = sm.inventory_balance_id
                        AND c.reference_id = sm.reference_id
                        AND c.reference_type = CONCAT(sm.reference_type, '_CANCEL')
                    )
                  )
                GROUP BY sm.fiscal_month
                """;
        @SuppressWarnings("unchecked")
        List<Object[]> rows = entityManager.createNativeQuery(sql)
                .setParameter("balanceId", balanceId)
                .getResultList();
        Map<Integer, BigDecimal[]> byMonth = new HashMap<>();
        for (Object[] row : rows) {
            byMonth.put(
                    ((Number) row[0]).intValue(),
                    new BigDecimal[] { toBigDecimal(row[1]), toBigDecimal(row[2]) }
            );
        }
        return byMonth;
    }

    private static BigDecimal floorZero(BigDecimal value) {
        return value.signum() < 0 ? BigDecimal.ZERO : value;
    }

    private static Integer toInteger(Object value) {
        if (value == null) {
            return null;
        }
        return ((Number) value).intValue();
    }

    private static BigDecimal toBigDecimal(Object value) {
        if (value == null) {
            return BigDecimal.ZERO;
        }
        if (value instanceof BigDecimal bigDecimal) {
            return bigDecimal;
        }
        return BigDecimal.valueOf(((Number) value).doubleValue());
    }

    private static LocalDate toLocalDate(Object value) {
        if (value instanceof LocalDate localDate) {
            return localDate;
        }
        if (value instanceof Date date) {
            return date.toLocalDate();
        }
        return LocalDate.parse(value.toString());
    }
}
