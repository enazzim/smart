package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record SalesRevenueView(
        long id,
        String revenueNo,
        long partnerId,
        String partnerName,
        LocalDate revenueDate,
        Long salesShipmentId,
        SalesRevenueStatus status,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<SalesRevenueLineView> lines
) {
}
