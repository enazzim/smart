package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
import java.time.LocalDate;

public record SalesRevenueListCriteria(
        LocalDate revenueDateFrom,
        LocalDate revenueDateTo,
        String revenueNo,
        String partnerName,
        Long itemId,
        SalesRevenueStatus status,
        boolean excludeCancelled
) {
}
