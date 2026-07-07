package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.common.BulkImportResult;
import java.util.List;

public record BulkImportResponse(int successCount, int failureCount, List<BulkImportFailureResponse> failures) {
    public static BulkImportResponse from(BulkImportResult result) {
        return new BulkImportResponse(
                result.successCount(),
                result.failureCount(),
                result.failures().stream().map(BulkImportFailureResponse::from).toList()
        );
    }
}
