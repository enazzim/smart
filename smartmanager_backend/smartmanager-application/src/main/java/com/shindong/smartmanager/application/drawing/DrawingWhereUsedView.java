package com.shindong.smartmanager.application.drawing;

public record DrawingWhereUsedView(
        String id,
        String parentHistoryId,
        String childHistoryId,
        String refRole,
        int sortOrder,
        String remark,
        DrawingReferencePeerView parent
) {
}
