package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import com.shindong.smartmanager.domain.drawing.DrawingType;
import java.time.Instant;

public record DrawingListView(
        String id,
        String partNo,
        String partName,
        String modelType,
        Long itemId,
        String itemNo,
        DrawingLifecycleStage lifecycleStage,
        Long sourcePartnerId,
        String sourcePartnerName,
        Instant itemLinkedAt,
        int majorVersion,
        int minorVersion,
        Instant updatedAt,
        DrawingType drawingType
) {
}
