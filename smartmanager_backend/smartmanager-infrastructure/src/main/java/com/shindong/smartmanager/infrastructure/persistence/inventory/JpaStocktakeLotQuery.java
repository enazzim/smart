package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.LotRepository;
import com.shindong.smartmanager.application.inventory.StocktakeLotQty;
import com.shindong.smartmanager.application.inventory.StocktakeLotQuery;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaStocktakeLotQuery implements StocktakeLotQuery {

    private final LotRepository lotRepository;

    @PersistenceContext
    private EntityManager entityManager;

    public JpaStocktakeLotQuery(LotRepository lotRepository) {
        this.lotRepository = lotRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<Long> findLotId(long itemId, String lotNo) {
        if (lotNo == null || lotNo.isBlank()) {
            return Optional.empty();
        }
        return lotRepository.findActiveByItemIdAndLotNo(itemId, lotNo.trim()).map(lot -> lot.id());
    }

    @Override
    @Transactional(readOnly = true)
    public List<StocktakeLotQty> listThrough(
            long itemId,
            String locationCode,
            int fiscalYear,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT l.id, l.lot_no, COALESCE(SUM(lb.qty_on_hand), 0)
                FROM inventory_lot l
                JOIN inventory_lot_balance lb ON lb.lot_id = l.id AND lb.recording_state = 1
                JOIN inventory_balance ib ON ib.id = lb.inventory_balance_id AND ib.recording_state = 1
                JOIN inventory_location il ON il.id = ib.location_id
                WHERE l.recording_state = 1
                  AND l.item_id = :itemId
                  AND il.location_code = :locationCode
                  AND ib.fiscal_year <= :fiscalYear
                """);
        appendProcess(sql, "ib.output_process_id", outputProcessId, "outputProcessId");
        appendProcess(sql, "ib.input_process_id", inputProcessId, "inputProcessId");
        appendProcess(sql, "ib.partner_id", partnerId, "partnerId");
        sql.append(" GROUP BY l.id, l.lot_no ORDER BY l.lot_no");
        Query query = entityManager.createNativeQuery(sql.toString());
        query.setParameter("itemId", itemId);
        query.setParameter("locationCode", locationCode);
        query.setParameter("fiscalYear", fiscalYear);
        if (outputProcessId != null) {
            query.setParameter("outputProcessId", outputProcessId);
        }
        if (inputProcessId != null) {
            query.setParameter("inputProcessId", inputProcessId);
        }
        if (partnerId != null) {
            query.setParameter("partnerId", partnerId);
        }
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<StocktakeLotQty> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new StocktakeLotQty(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    row[2] != null ? new BigDecimal(row[2].toString()) : BigDecimal.ZERO
            ));
        }
        return result;
    }

    private static void appendProcess(StringBuilder sql, String column, Long value, String param) {
        if (value == null) {
            sql.append(" AND ").append(column).append(" IS NULL");
        } else {
            sql.append(" AND ").append(column).append(" = :").append(param);
        }
    }
}
