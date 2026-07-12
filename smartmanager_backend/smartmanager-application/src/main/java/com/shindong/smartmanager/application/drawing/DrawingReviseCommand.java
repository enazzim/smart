package com.shindong.smartmanager.application.drawing;

public record DrawingReviseCommand(
        String changeType,
        String changeReason,
        String filePath,
        long fileSizeBytes
) {
}
