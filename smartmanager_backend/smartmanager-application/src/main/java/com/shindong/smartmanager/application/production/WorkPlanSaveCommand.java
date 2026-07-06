package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.math.BigDecimal;
import java.time.LocalDate;

public record WorkPlanSaveCommand(
        long productionPlanId,
        long processSequenceId,
        Long workCenterId,
        WorkDistinction workDistinction,
        BigDecimal plannedQty,
        LocalDate planStartDate,
        LocalDate planEndDate,
        int setupTime,
        int standardTime
) {
}
