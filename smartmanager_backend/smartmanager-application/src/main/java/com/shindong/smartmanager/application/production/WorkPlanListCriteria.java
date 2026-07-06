package com.shindong.smartmanager.application.production;

import java.time.LocalDate;

public record WorkPlanListCriteria(
        String itemNo,
        String itemName,
        String processName,
        String workCenterName,
        LocalDate planStartDateFrom,
        LocalDate planStartDateTo
) {
}
