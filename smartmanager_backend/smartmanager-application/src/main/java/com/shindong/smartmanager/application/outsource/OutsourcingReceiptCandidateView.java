package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingReceiptCandidateView(
        long outsourcingOrderLineId,
        long outsourcingOrderId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        String processName,
        String checkDistinction,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal remainQty,
        BigDecimal unitPrice,
        LocalDate requestedDeliveryDate,
        boolean lotTracked
) {
}
