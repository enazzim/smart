package com.shindong.smartmanager.application.drawing;

public record DrawingReferenceIntegrityIssue(
        String code,
        String parentPartNo,
        String parentHistoryId,
        String childPartNo,
        String childHistoryId,
        String message
) {
}
