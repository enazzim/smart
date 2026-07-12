package com.shindong.smartmanager.application.drawing;

public record DrawingMasterView(
        String id,
        String partNo,
        String partName,
        String modelGroup,
        Long itemId,
        int recordingState
) {
}
