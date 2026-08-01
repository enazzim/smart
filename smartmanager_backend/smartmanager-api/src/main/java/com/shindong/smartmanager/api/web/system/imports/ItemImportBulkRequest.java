package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.system.imports.ItemImportRow;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import java.math.BigDecimal;
import java.util.List;

public record ItemImportBulkRequest(@Valid @NotEmpty List<ItemImportRowRequest> rows) {
    public List<ItemImportRow> toRows() {
        return rows.stream().map(ItemImportRowRequest::toRow).toList();
    }

    public record ItemImportRowRequest(
            @NotBlank String itemNo,
            @NotBlank String itemName,
            @NotBlank String propertyClassification,
            @NotBlank(message = "기종은 필수입니다.") String modelType,
            @NotBlank String unit,
            String standard,
            BigDecimal standardUnitCost,
            CheckDistinction checkDistinction,
            Integer leadTime,
            BigDecimal safetyStockQuantity,
            BigDecimal orderIntervalQuantity,
            BigDecimal minOrderQuantity,
            Boolean lotTracked
    ) {
        ItemImportRow toRow() {
            return new ItemImportRow(
                    itemNo,
                    itemName,
                    PropertyClassification.fromImportLabel(propertyClassification),
                    modelType,
                    unit,
                    standard,
                    standardUnitCost,
                    checkDistinction,
                    leadTime,
                    safetyStockQuantity,
                    orderIntervalQuantity,
                    minOrderQuantity,
                    lotTracked != null && lotTracked
            );
        }
    }
}
