package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.StocktakeBookLine;
import com.shindong.smartmanager.application.inventory.StocktakeBookQuery;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaStocktakeBookQuery implements StocktakeBookQuery {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    @Transactional(readOnly = true)
    public List<StocktakeBookLine> listInhouseNonLot(LocalDate stocktakeDate, int fiscalYear) {
        Query query = entityManager.createNativeQuery("""
                SELECT i.item_no, i.item_name, i.property_classification,
                       il.location_code, il.location_name,
                       CASE
                         WHEN ib.output_process_id IS NOT NULL THEN ib.output_process_id
                         WHEN il.location_code = 'SALES' AND i.property_classification = '제품' THEN (
                              SELECT ps2.id FROM process_sequence ps2
                              WHERE ps2.item_id = i.id
                                AND ps2.recording_state = 1
                                AND ps2.variant = 'plan'
                              ORDER BY ps2.process_sequence DESC, ps2.id DESC
                              LIMIT 1)
                         ELSE NULL
                       END,
                       CASE
                         WHEN ib.output_process_id IS NOT NULL THEN pc.small_name
                         WHEN il.location_code = 'SALES' AND i.property_classification = '제품' THEN (
                              SELECT pc2.small_name
                              FROM process_sequence ps2
                              LEFT JOIN public_code pc2 ON pc2.id = ps2.public_code_id
                              WHERE ps2.item_id = i.id
                                AND ps2.recording_state = 1
                                AND ps2.variant = 'plan'
                              ORDER BY ps2.process_sequence DESC, ps2.id DESC
                              LIMIT 1)
                         ELSE NULL
                       END,
                       (SELECT COALESCE(SUM(prev.stock_qty), 0)
                          FROM inventory_balance prev
                         WHERE prev.recording_state = 1
                           AND prev.item_id = ib.item_id
                           AND prev.location_id = ib.location_id
                           AND prev.fiscal_year <= :fiscalYear
                           AND prev.output_process_id <=> ib.output_process_id
                           AND prev.input_process_id <=> ib.input_process_id
                           AND prev.partner_id <=> ib.partner_id) AS book_qty,
                       (SELECT COUNT(*)
                          FROM stock_movement sm
                         WHERE sm.recording_state = 1
                           AND sm.item_id = ib.item_id
                           AND sm.location_id = ib.location_id
                           AND sm.fiscal_year <= :fiscalYear
                           AND sm.movement_date > :stocktakeDate
                           AND sm.output_process_id <=> ib.output_process_id
                           AND sm.input_process_id IS NULL) AS later_count,
                       i.id, CAST(i.lot_tracked AS SIGNED)
                FROM inventory_balance ib
                JOIN item i ON i.id = ib.item_id AND i.recording_state = 1
                JOIN inventory_location il ON il.id = ib.location_id
                LEFT JOIN process_sequence ps ON ps.id = ib.output_process_id
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                WHERE ib.recording_state = 1
                  AND ib.fiscal_year <= :fiscalYear
                  AND il.location_code IN ('RAW', 'WIP', 'SALES')
                  AND ib.input_process_id IS NULL
                  AND ib.partner_id IS NULL
                  AND ib.fiscal_year = (
                       SELECT MAX(latest.fiscal_year)
                       FROM inventory_balance latest
                       WHERE latest.recording_state = 1
                         AND latest.item_id = ib.item_id
                         AND latest.location_id = ib.location_id
                         AND latest.fiscal_year <= :fiscalYear
                         AND latest.output_process_id <=> ib.output_process_id
                         AND latest.input_process_id IS NULL
                         AND latest.partner_id IS NULL
                  )
                ORDER BY i.item_no, il.location_code, ps.process_sequence, ib.id
                """);
        query.setParameter("fiscalYear", fiscalYear);
        query.setParameter("stocktakeDate", stocktakeDate);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<StocktakeBookLine> result = new ArrayList<>();
        for (Object[] row : rows) {
            long later = ((Number) row[8]).longValue();
            result.add(new StocktakeBookLine(
                    row[0].toString(),
                    row[1].toString(),
                    row[2] != null ? row[2].toString() : null,
                    row[3].toString(),
                    row[4].toString(),
                    row[5] != null ? ((Number) row[5]).longValue() : null,
                    row[6] != null ? row[6].toString() : null,
                    row[7] != null ? new BigDecimal(row[7].toString()) : BigDecimal.ZERO,
                    later > 0 ? "실사일 이후 입출고가 있어 반영할 수 없습니다." : null,
                    null,
                    null,
                    null,
                    null,
                    row[9] != null ? ((Number) row[9]).longValue() : 0L,
                    tracked(row[10])
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<StocktakeBookLine> listRawWithoutBalance(int fiscalYear) {
        Query query = entityManager.createNativeQuery("""
                SELECT i.item_no, i.item_name, i.id, CAST(i.lot_tracked AS SIGNED)
                FROM item i
                WHERE i.recording_state = 1
                  AND i.property_classification = '원자재'
                  AND NOT EXISTS (
                       SELECT 1
                       FROM inventory_balance ib
                       JOIN inventory_location il ON il.id = ib.location_id
                       WHERE ib.recording_state = 1
                         AND ib.item_id = i.id
                         AND ib.fiscal_year <= :fiscalYear
                         AND il.location_code = 'RAW'
                         AND ib.input_process_id IS NULL
                         AND ib.partner_id IS NULL
                  )
                ORDER BY i.item_no
                """);
        query.setParameter("fiscalYear", fiscalYear);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<StocktakeBookLine> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new StocktakeBookLine(
                    row[0].toString(),
                    row[1].toString(),
                    "원자재",
                    "RAW",
                    "원자재창고",
                    null,
                    null,
                    BigDecimal.ZERO,
                    null,
                    null,
                    null,
                    null,
                    null,
                    ((Number) row[2]).longValue(),
                    tracked(row[3])
            ));
        }
        return result;
    }

    /** MariaDB TINYINT(1)은 Boolean으로, CAST 결과는 Number로 온다. */
    private static boolean tracked(Object value) {
        if (value instanceof Boolean bool) {
            return bool;
        }
        if (value instanceof Number number) {
            return number.intValue() != 0;
        }
        return false;
    }
}
