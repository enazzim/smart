package com.shindong.smartmanager.application.workstandard;

import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.time.Instant;

public record WorkStandardView(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        long processCodeId,
        String processCode,
        String processName,
        WorkDistinction workDistinction,
        long workCenterId,
        String wcName,
        Long equipmentId,
        int priorityOrder,
        Long mainWorkerId,
        String toolName,
        int setupTime,
        int standardTime,
        Instant createdAt
) {
}
