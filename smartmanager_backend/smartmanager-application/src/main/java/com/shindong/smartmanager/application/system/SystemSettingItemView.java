package com.shindong.smartmanager.application.system;

import java.time.Instant;
import java.util.List;
import java.util.Map;

public record SystemSettingItemView(
        String settingKey,
        String label,
        String description,
        String value,
        List<String> allowedValues,
        Map<String, String> allowedValueLabels,
        Instant updatedAt,
        String updatedBy
) {
}
