package com.shindong.smartmanager.application.production;

import java.time.LocalDate;

public record WorkOrderListCriteria(
        String itemNo,
        String itemName,
        String processName,
        String workCenterName,
        String orderNum,
        LocalDate planStartDateFrom,
        LocalDate planStartDateTo
) {
}
