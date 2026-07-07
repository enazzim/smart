package com.shindong.smartmanager.application.common;

import java.util.List;

public record BulkImportResult(int successCount, int failureCount, List<BulkFailure> failures) {
}
