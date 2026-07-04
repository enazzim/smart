package com.shindong.smartmanager.api.web.workstandard;

import jakarta.validation.constraints.NotBlank;

public record CopyWorkStandardRequest(
        @NotBlank String sourceItemNum,
        @NotBlank String targetItemNum
) {
}
