package com.shindong.smartmanager.application.drawing;

public record DrawingInfoUpdateCommand(
        String partNo,
        String partName,
        String modelType
) {
}
