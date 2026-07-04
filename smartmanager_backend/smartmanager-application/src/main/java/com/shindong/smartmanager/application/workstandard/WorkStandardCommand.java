package com.shindong.smartmanager.application.workstandard;

public record WorkStandardCommand(
        long itemId,
        long processSequenceId,
        long workCenterId,
        Long equipmentId,
        int priorityOrder,
        Long mainWorkerId,
        String toolName,
        int setupTime,
        int standardTime
) {
}
