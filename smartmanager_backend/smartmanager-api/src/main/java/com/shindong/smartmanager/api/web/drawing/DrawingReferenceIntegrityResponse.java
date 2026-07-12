package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.application.drawing.DrawingReferenceIntegrityIssue;

public record DrawingReferenceIntegrityResponse(
        String code,
        String parentPartNo,
        String parentHistoryId,
        String childPartNo,
        String childHistoryId,
        String message
) {
    public static DrawingReferenceIntegrityResponse from(DrawingReferenceIntegrityIssue issue) {
        return new DrawingReferenceIntegrityResponse(
                issue.code(),
                issue.parentPartNo(),
                issue.parentHistoryId(),
                issue.childPartNo(),
                issue.childHistoryId(),
                issue.message()
        );
    }
}
