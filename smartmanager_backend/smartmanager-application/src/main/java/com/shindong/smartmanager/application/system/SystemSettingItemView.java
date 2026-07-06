package com.shindong.smartmanager.application.system;

import java.time.Instant;
import java.util.List;

public record SystemSettingItemView(
        String settingKey,
        String label,
        String description,
        String value,
        List<String> allowedValues,
        Instant updatedAt,
        String updatedBy
) {
}
