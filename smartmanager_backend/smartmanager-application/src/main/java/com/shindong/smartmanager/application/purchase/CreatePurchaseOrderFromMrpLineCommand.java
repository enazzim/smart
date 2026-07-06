package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record CreatePurchaseOrderFromMrpLineCommand(
        long requirementLineId,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        LocalDate requestedDeliveryDate
) {
}
