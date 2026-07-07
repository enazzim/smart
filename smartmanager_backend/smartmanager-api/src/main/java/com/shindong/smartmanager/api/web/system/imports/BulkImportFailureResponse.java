package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.common.BulkFailure;
import com.shindong.smartmanager.application.common.BulkImportResult;

public record BulkImportFailureResponse(int rowIndex, String key, String message) {
    public static BulkImportFailureResponse from(BulkFailure failure) {
        return new BulkImportFailureResponse(failure.rowIndex(), failure.key(), failure.message());
    }
}
