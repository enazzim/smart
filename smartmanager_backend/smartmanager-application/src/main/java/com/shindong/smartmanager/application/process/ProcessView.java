package com.shindong.smartmanager.application.process;

import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.time.Instant;

public record ProcessView(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        short processSequenceNum,
        long processCodeId,
        String processCode,
        String processName,
        WorkDistinction workDistinction,
        Long workCenterId,
        String wcName,
        int outsideOrderRate,
        short progressRate,
        Instant createdAt
) {
}
