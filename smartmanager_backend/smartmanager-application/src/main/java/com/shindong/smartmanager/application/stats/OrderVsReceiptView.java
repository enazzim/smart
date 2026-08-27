package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;
import java.time.LocalDate;

public record OrderVsReceiptView(
        long orderLineId,
        String division,
        long companyId,
        String companyName,
        long itemId,
        String itemNo,
        String itemName,
        String modelType,
        String processName,
        String unit,
        String standard,
        LocalDate orderDate,
        BigDecimal orderQty,
        BigDecimal orderUnitPrice,
        BigDecimal orderAmount,
        LocalDate requestedDeliveryDate,
        LocalDate lastReceiptDate,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal receiptAmount,
        BigDecimal remainQty,
        BigDecimal remainAmount,
        BigDecimal currentStockQty
) {
}
