package com.shindong.smartmanager.application.drawing;

public record DrawingMasterView(
        String id,
        String partNo,
        String partName,
        String modelType,
        Long itemId,
        int recordingState
) {
}
