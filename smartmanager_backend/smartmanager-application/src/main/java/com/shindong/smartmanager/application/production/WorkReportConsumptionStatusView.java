package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.util.List;

public record WorkReportConsumptionStatusView(
        long workOrderId,
        String parentItemNo,
        String parentItemName,
        BigDecimal cumulativeGoodQty,
        BigDecimal pendingGoodQty,
        List<WorkReportConsumptionLineView> lines,
        boolean allSatisfied,
        boolean materialIssueEnabled
) {
}
