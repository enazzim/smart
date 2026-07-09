package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcPurchaseReceiptCandidateView(
        long etcPurchaseOrderId,
        String orderNo,
        String itemName,
        long partnerId,
        String partnerName,
        BigDecimal unitPrice,
        BigDecimal orderQty,
        BigDecimal remainQty,
        BigDecimal amount,
        LocalDate requestedDeliveryDate,
        String categoryName,
        EtcPurchaseOrderStatus status
) {
}
