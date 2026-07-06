package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreatePurchaseOrderFromMrpCommand(
        long partnerId,
        LocalDate orderDate,
        List<CreatePurchaseOrderFromMrpLineCommand> lines
) {
}
