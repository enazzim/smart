package com.shindong.smartmanager.application.production;

import java.time.LocalDate;

public record WorkReportListCriteria(
        String itemNo,
        String itemName,
        String processName,
        String orderNum,
        String reportNum,
        LocalDate reportDateFrom,
        LocalDate reportDateTo
) {
}
