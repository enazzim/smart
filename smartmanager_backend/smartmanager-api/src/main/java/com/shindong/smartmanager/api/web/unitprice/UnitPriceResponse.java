package com.shindong.smartmanager.api.web.unitprice;

import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.pricing.CostType;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record UnitPriceResponse(
        long id,
        CostType type,
        long itemId,
        String itemNum,
        String itemName,
        long companyId,
        String companyName,
        String businessRegistrationNum,
        Long beginProcessCodeId,
        String processCode,
        String processName,
        Long endProcessCodeId,
        String endProcessCode,
        String endProcessName,
        BigDecimal orderRate,
        BigDecimal standardUnitCost,
        BigDecimal discountUnitCost,
        LocalDate beginDate,
        LocalDate endDate,
        Instant createdAt
) {

    public static UnitPriceResponse from(UnitPriceView view) {
        return new UnitPriceResponse(
                view.id(),
                view.costType(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.companyId(),
                view.companyName(),
                view.businessRegNo(),
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
                view.createdAt()
        );
    }
}
