package com.shindong.smartmanager.application.stats;

import java.time.LocalDate;

public record PurchaseDailyReportCriteria(
        Long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        LocalDate inputDateFrom,
        LocalDate inputDateTo,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        Integer fiscalYear,
        Integer fiscalMonth,
        String division,
        String approvalStatus
) {
}
