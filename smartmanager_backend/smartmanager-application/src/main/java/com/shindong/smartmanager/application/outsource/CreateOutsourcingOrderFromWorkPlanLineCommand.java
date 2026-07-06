package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record CreateOutsourcingOrderFromWorkPlanLineCommand(
        long workPlanId,
        long beginProcessCodeId,
        long endProcessCodeId,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        LocalDate requestedDeliveryDate
) {
}
