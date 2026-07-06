package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.WorkReportStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record WorkReportSaveCommand(
        String reportNum,
        long workOrderId,
        LocalDate reportDate,
        BigDecimal goodQty,
        BigDecimal scrapQty,
        BigDecimal setupTime,
        BigDecimal runTime,
        String workerName,
        WorkReportStatus status,
        boolean stockApplied
) {
}
