package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseOrderLineView(
        long id,
        int lineNo,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        BigDecimal orderQty,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long requirementLineId,
        String planNo,
        String runNo,
        LocalDate requestedDeliveryDate
) {
}
