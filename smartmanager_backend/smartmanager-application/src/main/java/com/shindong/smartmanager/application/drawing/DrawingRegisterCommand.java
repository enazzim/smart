package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingType;

public record DrawingRegisterCommand(
        String partNo,
        String partName,
        String modelType,
        Long itemId,
        DrawingType drawingType,
        String filePath,
        long fileSizeBytes
) {
}
