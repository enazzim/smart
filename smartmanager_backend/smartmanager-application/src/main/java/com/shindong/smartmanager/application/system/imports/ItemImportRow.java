package com.shindong.smartmanager.application.system.imports;

import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;

public record ItemImportRow(
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
        BigDecimal minOrderQuantity
) {
}
