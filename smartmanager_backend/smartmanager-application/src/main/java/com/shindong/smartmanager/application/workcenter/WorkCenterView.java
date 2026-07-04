package com.shindong.smartmanager.application.workcenter;

import java.time.Instant;

public record WorkCenterView(
        long id,
        String wcName,
        long mainProcessCodeId,
        String mainProcessCode,
        String mainProcessName,
        int operationTime,
        Instant createdAt
) {
}
