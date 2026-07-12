package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.application.drawing.DrawingReferenceCandidateView;

public record DrawingReferenceCandidateResponse(
        String masterId,
        String historyId,
        String partNo,
        String partName,
        String drawingType,
        int majorVersion,
        int minorVersion,
        Long itemId,
        String itemNo,
        int bomLevel
) {
    public static DrawingReferenceCandidateResponse from(DrawingReferenceCandidateView view) {
        return new DrawingReferenceCandidateResponse(
                view.masterId(),
                view.historyId(),
                view.partNo(),
                view.partName(),
                view.drawingType().name(),
                view.majorVersion(),
                view.minorVersion(),
                view.itemId(),
                view.itemNo(),
                view.bomLevel()
        );
    }
}
