package com.shindong.smartmanager.application.production;

import java.time.LocalDate;

public record WorkTargetSearchCriteria(
        String itemNo,
        String processName,
        LocalDate dateFrom,
        LocalDate dateTo
) {
}
