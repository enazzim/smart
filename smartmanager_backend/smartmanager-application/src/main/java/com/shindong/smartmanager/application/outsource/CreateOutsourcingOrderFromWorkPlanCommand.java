package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreateOutsourcingOrderFromWorkPlanCommand(
        long partnerId,
        LocalDate orderDate,
        List<CreateOutsourcingOrderFromWorkPlanLineCommand> lines
) {
}
