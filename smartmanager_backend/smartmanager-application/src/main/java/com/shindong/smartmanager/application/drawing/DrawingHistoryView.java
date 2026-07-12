package com.shindong.smartmanager.application.drawing;

import java.time.Instant;

public record DrawingHistoryView(
        String id,
        int majorVersion,
        int minorVersion,
        String isLatest,
        String changeType,
        String changeReason,
        Instant createdAt
) {
}
