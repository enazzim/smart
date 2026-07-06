package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingOrderSourceType;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingOrderCommand(
        String orderNo,
        long partnerId,
        LocalDate orderDate,
        OutsourcingOrderSourceType sourceType,
        List<OutsourcingOrderLineCommand> lines
) {
}
