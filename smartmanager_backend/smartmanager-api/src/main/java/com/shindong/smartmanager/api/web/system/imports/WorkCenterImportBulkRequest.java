package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.system.imports.WorkCenterImportRow;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.util.List;

public record WorkCenterImportBulkRequest(@Valid @NotEmpty List<WorkCenterImportRowRequest> rows) {
    public List<WorkCenterImportRow> toRows() {
        return rows.stream().map(WorkCenterImportRowRequest::toRow).toList();
    }

    public record WorkCenterImportRowRequest(
            @NotBlank String wcName,
            @NotBlank String mainProcessSmallCode,
            @NotNull Integer operationTime
    ) {
        WorkCenterImportRow toRow() {
            return new WorkCenterImportRow(wcName, mainProcessSmallCode, operationTime);
        }
    }
}
