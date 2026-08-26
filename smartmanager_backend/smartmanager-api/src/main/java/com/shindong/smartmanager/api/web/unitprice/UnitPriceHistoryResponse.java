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
        String itemNo,
        String itemName,
        long companyId,
        String companyName,
        Long beginProcessCodeId,
        String beginProcessCode,
        String beginProcessName,
        Long endProcessCodeId,
        String endProcessCode,
        String endProcessName,
        BigDecimal orderRate,
        BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        LocalDate beginDate,
        LocalDate endDate,
        String updateReason,
        String changedBy,
        String changedById,
        Instant changedAt
) {

    public static UnitPriceHistoryResponse from(UnitPriceChangeLogView view) {
        return new UnitPriceHistoryResponse(
                view.id(),
                view.unitPriceId(),
                view.costType(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.companyId(),
                view.companyName(),
                view.beginProcessCodeId(),
                view.beginProcessCode(),
                view.beginProcessName(),
                view.endProcessCodeId(),
                view.endProcessCode(),
                view.endProcessName(),
                view.orderRate(),
                view.standardUnitCost(),
                view.discountUnitCost(),
                view.beginDate(),
                view.endDate(),
                view.updateReason(),
                view.changedBy(),
                view.changedById(),
                view.changedAt()
        );
    }
}
