package com.shindong.smartmanager.application.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.Collection;
import java.util.List;
import java.util.Optional;

public interface UnitPriceRepository {

    long save(UnitPriceCommand command, String actorUserId);

    void update(long id, UnitPriceUpdateCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);

    Optional<UnitPriceView> findActiveById(long id);

    List<UnitPriceView> findAllActiveByCostType(CostType costType, String query);

    List<UnitPriceView> findAllActiveByCostTypeAndItemIds(CostType costType, Collection<Long> itemIds);

    Optional<UnitPriceView> findActiveByItemNoAndCostType(String itemNo, CostType costType);

    boolean existsActiveUk(
            CostType costType,
            long itemId,
            long companyId,
            LocalDate beginDate,
            Long beginProcessCodeId,
            Long endProcessCodeId,
            Long excludeId
    );

    void lockActiveRowsForOrderRateValidation(CostType costType, long itemId);

    BigDecimal sumActiveOrderRate(CostType costType, long itemId, Long excludeId);

    BigDecimal sumActiveOrderRateForOutsourceSegment(
            long itemId,
            long beginProcessCodeId,
            long endProcessCodeId,
            Long excludeId
    );

    void appendChangeLog(long unitPriceId, String updateReason, String actorUserId);

    List<UnitPriceChangeLogView> findChangeLogs(long unitPriceId);

    List<UnitPriceChangeLogView> findChangeLogs(UnitPriceHistorySearchQuery query);
}
