package com.shindong.smartmanager.application.system.backup;

import java.time.Instant;

public record FullBackupSetView(
        String setName,
        String sqlFileName,
        long totalSizeBytes,
        long drawingPdfFileCount,
        Instant createdAt,
        String reason
) {
}
