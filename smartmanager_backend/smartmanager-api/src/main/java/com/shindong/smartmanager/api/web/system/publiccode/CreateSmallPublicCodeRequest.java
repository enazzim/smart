package com.shindong.smartmanager.api.web.system.publiccode;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record CreateSmallPublicCodeRequest(
        @NotBlank @Size(max = 20) String largeCode,
        @NotBlank @Size(max = 20) String smallCode,
        @NotBlank @Size(max = 100) String smallName
) {
}
