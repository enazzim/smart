package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import jakarta.validation.constraints.NotNull;

public record DrawingLifecycleUpdateRequest(
        @NotNull(message = "lifecycle 단계는 필수입니다.") DrawingLifecycleStage lifecycleStage
) {
}
