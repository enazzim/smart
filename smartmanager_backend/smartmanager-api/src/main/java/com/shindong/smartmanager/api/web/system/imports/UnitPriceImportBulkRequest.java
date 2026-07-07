package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.system.imports.UnitPriceImportRow;
import com.shindong.smartmanager.domain.pricing.CostType;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record UnitPriceImportBulkRequest(@Valid @NotEmpty List<UnitPriceImportRowRequest> rows) {
    public List<UnitPriceImportRow> toRows() {
        return rows.stream().map(UnitPriceImportRowRequest::toRow).toList();
    }

    public record UnitPriceImportRowRequest(
            @NotNull CostType costType,
            @NotBlank String itemNum,
            @NotBlank String businessRegNo,
            String beginProcessSmallCode,
            String endProcessSmallCode,
            BigDecimal orderRate,
            @NotNull BigDecimal standardUnitCost,
            BigDecimal discountUnitCost,
            @NotNull LocalDate beginDate,
            LocalDate endDate
    ) {
        UnitPriceImportRow toRow() {
            return new UnitPriceImportRow(
                    costType,
                    itemNum,
                    businessRegNo,
                    beginProcessSmallCode,
                    endProcessSmallCode,
                    orderRate,
                    standardUnitCost,
                    discountUnitCost,
                    beginDate,
                    endDate
            );
        }
    }
}
