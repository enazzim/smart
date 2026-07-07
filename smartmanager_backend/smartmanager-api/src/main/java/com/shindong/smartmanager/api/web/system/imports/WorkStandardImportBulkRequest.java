package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.system.imports.WorkStandardImportRow;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.util.List;

public record WorkStandardImportBulkRequest(@Valid @NotEmpty List<WorkStandardImportRowRequest> rows) {
    public List<WorkStandardImportRow> toRows() {
        return rows.stream().map(WorkStandardImportRowRequest::toRow).toList();
    }

    public record WorkStandardImportRowRequest(
            @NotBlank String itemNum,
            @NotNull Short processSequenceNum,
            @NotBlank String processSmallCode,
            @NotBlank String workCenterName,
            String equipmentNum,
            @NotNull Integer priorityOrder,
            String mainWorkerLoginId,
            String toolName,
            @NotNull Integer setupTime,
            @NotNull Integer standardTime
    ) {
        WorkStandardImportRow toRow() {
            return new WorkStandardImportRow(
                    itemNum,
                    processSequenceNum,
                    processSmallCode,
                    workCenterName,
                    equipmentNum,
                    priorityOrder,
                    mainWorkerLoginId,
                    toolName,
                    setupTime,
                    standardTime
            );
        }
    }
}
