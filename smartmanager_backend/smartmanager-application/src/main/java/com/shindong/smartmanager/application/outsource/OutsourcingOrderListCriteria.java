package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import java.time.LocalDate;

public record OutsourcingOrderListCriteria(
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        String partnerName,
        String orderNo,
        OutsourcingOrderStatus status,
        boolean excludeCancelled
) {
}
