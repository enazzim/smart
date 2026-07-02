package com.shindong.smartmanager.application.process;

import com.shindong.smartmanager.domain.process.WorkDistinction;

public record ProcessUpdateCommand(
        long itemId,
        short processSequenceNum,
        long processCodeId,
        WorkDistinction workDistinction,
        Long workCenterId,
        int outsideOrderRate,
        short progressRate
) {
}
