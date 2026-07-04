package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record SalesOrderCommand(
        String orderNo,
        long partnerId,
        LocalDate orderDate,
        LocalDate requestedDeliveryDate,
        String remark,
        List<SalesOrderLineCommand> lines
) {
}
