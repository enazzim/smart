package com.shindong.smartmanager.api.web.drawing;

import jakarta.validation.constraints.NotBlank;

public record DrawingInfoUpdateRequest(
        @NotBlank(message = "품번은 필수입니다.") String partNo,
        @NotBlank(message = "품명은 필수입니다.") String partName,
        @NotBlank(message = "기종은 필수입니다.") String modelType
) {
}
