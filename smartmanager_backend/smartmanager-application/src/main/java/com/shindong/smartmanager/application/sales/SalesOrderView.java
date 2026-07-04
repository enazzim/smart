package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record SalesOrderView(
        long id,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        LocalDate requestedDeliveryDate,
        SalesOrderStatus status,
        String remark,
        Instant confirmedAt,
        String confirmedBy,
        List<SalesOrderLineView> lines
) {
}
