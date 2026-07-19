package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingType;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;

public record DrawingRegisterRequest(
        @NotBlank(message = "품번은 필수입니다.") String partNo,
        @NotBlank(message = "품명은 필수입니다.") String partName,
        @NotBlank(message = "기종은 필수입니다.") String modelType,
        Long sourcePartnerId,
        @NotNull(message = "도면 구분(DEV/PROD)은 필수입니다.") DrawingType drawingType
) {
}
