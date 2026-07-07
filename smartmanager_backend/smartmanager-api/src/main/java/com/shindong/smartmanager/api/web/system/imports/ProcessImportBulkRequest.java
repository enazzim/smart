package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.system.imports.ProcessImportRow;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.util.List;

public record ProcessImportBulkRequest(@Valid @NotEmpty List<ProcessImportRowRequest> rows) {
    public List<ProcessImportRow> toRows() {
        return rows.stream().map(ProcessImportRowRequest::toRow).toList();
    }

    public record ProcessImportRowRequest(
            @NotBlank String itemNo,
            @NotNull Short processSequenceNum,
            @NotBlank String processSmallCode,
            @NotNull WorkDistinction workDistinction,
            String workCenterName,
            Integer outsideOrderRate,
            Short progressRate
    ) {
        ProcessImportRow toRow() {
            return new ProcessImportRow(
                    itemNo,
                    processSequenceNum,
                    processSmallCode,
                    workDistinction,
                    workCenterName,
                    outsideOrderRate,
                    progressRate
            );
        }
    }
}
