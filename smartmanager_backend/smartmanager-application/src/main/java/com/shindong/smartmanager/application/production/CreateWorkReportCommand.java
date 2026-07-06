package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreateWorkReportCommand(
        long workOrderId,
        LocalDate reportDate,
        BigDecimal goodQty,
        BigDecimal scrapQty,
        BigDecimal setupTime,
        BigDecimal runTime,
        String workerName,
        List<WorkReportIssueLineCommand> issueLines
) {
}
