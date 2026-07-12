package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingType;
import java.time.Instant;

public record DrawingListView(
        String id,
        String partNo,
        String partName,
        String modelGroup,
        Long itemId,
        String itemNo,
        int majorVersion,
        int minorVersion,
        Instant updatedAt,
        DrawingType drawingType
) {
}
