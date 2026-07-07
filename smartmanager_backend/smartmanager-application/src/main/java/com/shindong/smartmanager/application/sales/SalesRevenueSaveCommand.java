package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
import java.time.LocalDate;
import java.util.List;

public record SalesRevenueSaveCommand(
        String revenueNo,
        long partnerId,
        LocalDate revenueDate,
        Long salesShipmentId,
        List<SalesRevenueLineSaveCommand> lines
) {
}
