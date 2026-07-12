package com.shindong.smartmanager.application.drawing;

public record DrawingReferenceChildCommand(
        String childHistoryId,
        String refRole,
        int sortOrder,
        String remark
) {
}
