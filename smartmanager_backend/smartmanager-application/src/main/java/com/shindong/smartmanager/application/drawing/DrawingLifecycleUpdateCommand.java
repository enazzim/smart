package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;

public record DrawingLifecycleUpdateCommand(
        DrawingLifecycleStage lifecycleStage
) {
}
