package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.time.LocalDate;

public record WorkReportRegistrationContext(
        long workOrderId,
        long workPlanId,
        long productionPlanId,
        long itemId,
        long processSequenceId,
        short processSequenceNum,
        BigDecimal orderedQty,
        BigDecimal reportedQty,
        LocalDate reportDate
) {
    public BigDecimal remainingQty() {
        return orderedQty.subtract(reportedQty);
    }
}
