package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingType;

public record DrawingReferenceCandidateView(
        String masterId,
        String historyId,
        String partNo,
        String partName,
        DrawingType drawingType,
        int majorVersion,
        int minorVersion,
        Long itemId,
        String itemNo,
        int bomLevel
) {
}
