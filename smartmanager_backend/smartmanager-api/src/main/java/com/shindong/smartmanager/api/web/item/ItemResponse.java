package com.shindong.smartmanager.api.web.item;

import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.Instant;

public record ItemResponse(
        long id,
        String itemNo,
        String itemName,
        PropertyClassification propertyClassification,
        String modelType,
        String unit,
        String standard,
        BigDecimal standardUnitCost,
        CheckDistinction checkDistinction,
        Integer leadTime,
        BigDecimal safetyStockQuantity,
        BigDecimal orderIntervalQuantity,
        BigDecimal minOrderQuantity,
        Instant createdAt
) {
    public static ItemResponse from(ItemView view) {
        return new ItemResponse(
                view.id(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.modelType(),
                view.unit(),
                view.standard(),
                view.standardUnitCost(),
                view.checkDistinction(),
                view.leadTime(),
                view.safetyStockQuantity(),
                view.orderIntervalQuantity(),
                view.minOrderQuantity(),
                view.createdAt()
        );
    }
}
