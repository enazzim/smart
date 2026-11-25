package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.StocktakeHistoryQuery;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.time.LocalDate;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaStocktakeHistoryQuery implements StocktakeHistoryQuery {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    @Transactional(readOnly = true)
    public boolean hasHistory(
            long itemId,
            long locationId,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId,
            int fiscalYear
    ) {
        return count(itemId, locationId, outputProcessId, inputProcessId, partnerId, fiscalYear, null) > 0;
    }

    @Override
    @Transactional(readOnly = true)
    public boolean hasLaterMovementInSummedYears(
            long itemId,
            long locationId,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId,
            int fiscalYear,
            LocalDate stocktakeDate
    ) {
        return count(itemId, locationId, outputProcessId, inputProcessId, partnerId, fiscalYear, stocktakeDate) > 0;
    }

    private long count(
            long itemId,
            long locationId,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId,
            int fiscalYear,
            LocalDate afterDate
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT COUNT(*)
                FROM stock_movement sm
                WHERE sm.recording_state = 1
                  AND sm.item_id = :itemId
                  AND sm.location_id = :locationId
                  AND sm.fiscal_year <= :fiscalYear
                """);
        if (outputProcessId == null) {
            sql.append(" AND sm.output_process_id IS NULL");
        } else {
            sql.append(" AND sm.output_process_id = :outputProcessId");
        }
        if (inputProcessId == null) {
            sql.append(" AND sm.input_process_id IS NULL");
        } else {
            sql.append(" AND sm.input_process_id = :inputProcessId");
        }
        if (partnerId != null) {
            sql.append(" AND sm.partner_id = :partnerId");
        }
        if (afterDate != null) {
            sql.append(" AND sm.movement_date > :afterDate");
        }
        Query query = entityManager.createNativeQuery(sql.toString());
        query.setParameter("itemId", itemId);
        query.setParameter("locationId", locationId);
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
        if (afterDate != null) {
            query.setParameter("afterDate", afterDate);
        }
        return ((Number) query.getSingleResult()).longValue();
    }
}
