package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import com.shindong.smartmanager.domain.purchase.PurchaseReceiptStatus;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record PurchaseOrderLineReceiptContext(
        long purchaseOrderLineId,
        long purchaseOrderId,
        long partnerId,
        String partnerName,
        String orderNo,
        PurchaseOrderStatus orderStatus,
        long itemId,
        String itemNum,
        String itemName,
        String propertyClassification,
        String checkDistinction,
        boolean lotTracked,
        BigDecimal orderQty,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal unitPrice
) {
    public BigDecimal remainQty() {
        return orderQty
                .subtract(receivedQty != null ? receivedQty : BigDecimal.ZERO)
                .subtract(waitingInspectionQty != null ? waitingInspectionQty : BigDecimal.ZERO);
    }
}
