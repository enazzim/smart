package com.shindong.smartmanager.api.web.item;

import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;

public record CreateItemRequest(
        @NotBlank String itemNo,
        @NotBlank String itemName,
        @NotNull PropertyClassification propertyClassification,
        @NotBlank String unit,
        String standard,
        BigDecimal standardUnitCost,
        CheckDistinction checkDistinction,
        Integer leadTime,
        BigDecimal safetyStockQuantity,
        BigDecimal orderIntervalQuantity,
        BigDecimal minOrderQuantity
) {
}
