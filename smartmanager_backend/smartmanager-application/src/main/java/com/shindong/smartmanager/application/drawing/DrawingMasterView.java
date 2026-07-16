package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import java.time.Instant;

public record DrawingMasterView(
        String id,
        String partNo,
        String partName,
        String modelType,
        Long itemId,
        DrawingLifecycleStage lifecycleStage,
        Long sourcePartnerId,
        Instant itemLinkedAt,
        int recordingState
) {
}
