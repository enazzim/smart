package com.shindong.smartmanager.api.web.unitprice;

import com.shindong.smartmanager.application.unitprice.UnitPriceChangeLogView;
import com.shindong.smartmanager.domain.pricing.CostType;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record UnitPriceHistoryResponse(
        long id,
        long unitPriceId,
        CostType type,
        long itemId,
        long companyId,
        Long beginProcessCodeId,
        Long endProcessCodeId,
        BigDecimal orderRate,
        BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        LocalDate beginDate,
        LocalDate endDate,
        String updateReason,
        String changedBy,
        Instant changedAt
) {

    public static UnitPriceHistoryResponse from(UnitPriceChangeLogView view) {
        return new UnitPriceHistoryResponse(
                view.id(),
                view.unitPriceId(),
                view.costType(),
                view.itemId(),
                view.companyId(),
                view.beginProcessCodeId(),
                view.endProcessCodeId(),
                view.orderRate(),
                view.standardUnitCost(),
                view.discountUnitCost(),
                view.beginDate(),
                view.endDate(),
                view.updateReason(),
                view.changedBy(),
                view.changedAt()
        );
    }
}
