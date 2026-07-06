package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingOrderLineCommand(
        long itemId,
        long processSequenceId,
        long beginProcessCodeId,
        long endProcessCodeId,
        Long workPlanId,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        LocalDate requestedDeliveryDate
) {
}
