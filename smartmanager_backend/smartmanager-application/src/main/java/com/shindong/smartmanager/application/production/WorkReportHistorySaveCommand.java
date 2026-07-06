package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.WorkReportHistorySourceType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record WorkReportHistorySaveCommand(
        long itemId,
        long productionPlanId,
        long processSequenceId,
        BigDecimal goodQty,
        BigDecimal scrapQty,
        LocalDate historyDate,
        WorkReportHistorySourceType sourceType,
        long sourceId,
        int fiscalYear,
        int fiscalMonth
) {
}
