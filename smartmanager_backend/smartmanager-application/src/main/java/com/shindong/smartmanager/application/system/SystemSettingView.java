package com.shindong.smartmanager.application.system;

import java.time.Instant;

public record SystemSettingView(
        String settingKey,
        String valueJson,
        Instant updatedAt,
        String updatedBy
) {
}
