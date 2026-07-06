package com.shindong.smartmanager.api.web.system.settings;

import jakarta.validation.constraints.NotBlank;

public record UpdateSystemSettingRequest(
        @NotBlank String value
) {
}
