package com.shindong.smartmanager.api.web.drawing;

import jakarta.validation.constraints.NotBlank;

public record DrawingReviseRequest(
        @NotBlank(message = "변경 유형은 필수입니다.") String changeType,
        String changeReason
) {
}
