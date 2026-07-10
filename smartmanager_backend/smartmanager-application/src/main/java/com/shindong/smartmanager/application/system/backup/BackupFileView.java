package com.shindong.smartmanager.application.system.backup;

import java.time.Instant;

public record BackupFileView(String fileName, long fileSizeBytes, Instant createdAt, String reason) {
}
