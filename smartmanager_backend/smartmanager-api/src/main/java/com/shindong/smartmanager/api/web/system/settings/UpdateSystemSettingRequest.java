package com.shindong.smartmanager.api.web.system.settings;

import jakarta.validation.constraints.NotNull;

public record UpdateSystemSettingRequest(
        @NotNull String value
) {
}
