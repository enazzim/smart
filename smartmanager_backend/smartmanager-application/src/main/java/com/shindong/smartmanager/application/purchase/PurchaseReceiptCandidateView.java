package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.item.CheckDistinction;
import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseReceiptCandidateView(
        long purchaseOrderId,
        long purchaseOrderLineId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNum,
        String itemName,
        CheckDistinction checkDistinction,
        boolean lotTracked,
        BigDecimal orderQty,
        BigDecimal receivedQty,
        BigDecimal remainQty,
        BigDecimal waitingInspectionQty,
        BigDecimal unitPrice,
        LocalDate requestedDeliveryDate
) {
}
