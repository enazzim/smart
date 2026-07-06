package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.WorkReportStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record WorkReportView(
        long id,
        String reportNum,
        long workOrderId,
        String orderNum,
        long productionPlanId,
        String planNo,
        long itemId,
        String itemNo,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        String processCode,
        String processName,
        Long workCenterId,
        String workCenterName,
        LocalDate reportDate,
        BigDecimal goodQty,
        BigDecimal scrapQty,
        BigDecimal setupTime,
        BigDecimal runTime,
        String workerName,
        WorkReportStatus status,
        boolean cancellable,
        Instant createdAt,
        String createdBy
) {
}
