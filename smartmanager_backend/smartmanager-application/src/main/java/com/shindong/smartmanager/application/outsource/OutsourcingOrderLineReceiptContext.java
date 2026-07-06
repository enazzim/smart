package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import java.math.BigDecimal;

public record OutsourcingOrderLineReceiptContext(
        long outsourcingOrderLineId,
        long outsourcingOrderId,
        long partnerId,
        long itemId,
        String itemNo,
        String checkDistinction,
        long endProcessCodeId,
        OutsourcingOrderStatus orderStatus,
        BigDecimal shippedQty,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal remainQty,
        BigDecimal unitPrice
) {
}
