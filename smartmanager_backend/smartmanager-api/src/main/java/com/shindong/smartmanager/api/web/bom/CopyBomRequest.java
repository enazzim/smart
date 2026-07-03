package com.shindong.smartmanager.api.web.bom;

import jakarta.validation.constraints.NotBlank;

public record CopyBomRequest(
        @NotBlank String sourceItemNum,
        @NotBlank String targetItemNum
) {
}
