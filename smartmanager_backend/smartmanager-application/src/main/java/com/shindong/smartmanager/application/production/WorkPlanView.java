package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.WorkPlanStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record WorkPlanView(
        long id,
        long productionPlanId,
        String planNo,
        long itemId,
        String itemNo,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        String processCode,
        String processName,
        WorkDistinction workDistinction,
        Long workCenterId,
        String workCenterName,
        BigDecimal plannedQty,
        LocalDate planStartDate,
        LocalDate planEndDate,
        int setupTime,
        int standardTime,
        WorkPlanStatus status,
        boolean cancellable,
        Instant createdAt,
        String createdBy
) {
}
