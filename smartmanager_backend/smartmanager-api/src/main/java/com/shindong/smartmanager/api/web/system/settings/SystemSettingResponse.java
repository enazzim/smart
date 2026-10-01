package com.shindong.smartmanager.api.web.system.settings;

import com.shindong.smartmanager.application.system.SystemSettingItemView;
import java.time.Instant;
import java.util.List;
import java.util.Map;

public record SystemSettingResponse(
        String settingKey,
        String label,
        String description,
        String value,
        List<String> allowedValues,
        Map<String, String> allowedValueLabels,
        Instant updatedAt,
        String updatedBy
) {
    public static SystemSettingResponse from(SystemSettingItemView view) {
        return new SystemSettingResponse(
                view.settingKey(),
                view.label(),
                view.description(),
                view.value(),
                view.allowedValues(),
                view.allowedValueLabels(),
                view.updatedAt(),
                view.updatedBy()
        );
    }
}
