package com.shindong.smartmanager.application.workstandard;

public record WorkStandardUpdateCommand(
        long workCenterId,
        Long equipmentId,
        int priorityOrder,
        Long mainWorkerId,
        String toolName,
        int setupTime,
        int standardTime
) {
}
