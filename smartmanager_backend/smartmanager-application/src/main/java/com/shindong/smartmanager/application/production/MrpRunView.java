package com.shindong.smartmanager.application.production;

import java.time.Instant;

public record MrpRunView(
        long id,
        String runNo,
        int planCount,
        int lineCount,
        Instant createdAt,
        String createdBy,
        boolean cancellable
) {
}
