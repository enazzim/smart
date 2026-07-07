package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.system.imports.ItemCompositionImportRow;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import java.math.BigDecimal;
import java.util.List;

public record ItemCompositionImportBulkRequest(@Valid @NotEmpty List<ItemCompositionImportRowRequest> rows) {
    public List<ItemCompositionImportRow> toRows() {
        return rows.stream().map(ItemCompositionImportRowRequest::toRow).toList();
    }

    public record ItemCompositionImportRowRequest(
            @NotBlank String parentItemNum,
            @NotBlank String childItemNum,
            @NotNull @Positive BigDecimal parentQuantity,
            @NotNull @Positive BigDecimal childQuantity
    ) {
        ItemCompositionImportRow toRow() {
            return new ItemCompositionImportRow(parentItemNum, childItemNum, parentQuantity, childQuantity);
        }
    }
}
