package com.shindong.smartmanager.api.web.system.settings;

import com.shindong.smartmanager.application.system.SystemSettingItemView;
import java.time.Instant;
import java.util.List;

public record SystemSettingResponse(
        String settingKey,
        String label,
        String description,
        String value,
        List<String> allowedValues,
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
                view.updatedAt(),
                view.updatedBy()
        );
    }
}
