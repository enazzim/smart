package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderSourceType;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record PurchaseOrderCommand(
        String orderNo,
        long partnerId,
        LocalDate orderDate,
        PurchaseOrderSourceType sourceType,
        List<PurchaseOrderLineCommand> lines
) {
}
