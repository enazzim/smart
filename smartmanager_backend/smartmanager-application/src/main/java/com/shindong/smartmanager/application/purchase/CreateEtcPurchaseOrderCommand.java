package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record CreateEtcPurchaseOrderCommand(
        String itemName,
        long partnerId,
        BigDecimal unitPrice,
        BigDecimal orderQty,
        LocalDate requestedDeliveryDate,
        Long categoryCodeId,
        LocalDate orderDate
) {
}
