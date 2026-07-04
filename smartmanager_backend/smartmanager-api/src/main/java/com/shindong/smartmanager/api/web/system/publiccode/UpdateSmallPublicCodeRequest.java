package com.shindong.smartmanager.api.web.system.publiccode;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record UpdateSmallPublicCodeRequest(
        @NotBlank @Size(max = 100) String smallName
) {
}
