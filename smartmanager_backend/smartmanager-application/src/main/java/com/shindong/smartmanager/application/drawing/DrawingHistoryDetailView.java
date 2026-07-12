package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingType;
import java.time.Instant;

public record DrawingHistoryDetailView(
        String id,
        String masterId,
        String partNo,
        String partName,
        String modelType,
        DrawingType drawingType,
        int majorVersion,
        int minorVersion,
        String filePath,
        long fileSizeBytes,
        String isLatest,
        int masterRecordingState,
        Instant createdAt
) {
}
