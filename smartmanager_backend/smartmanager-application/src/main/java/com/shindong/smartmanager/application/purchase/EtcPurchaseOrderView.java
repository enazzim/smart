package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcPurchaseOrderView(
        long id,
        String orderNo,
        String itemName,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        BigDecimal unitPrice,
        BigDecimal orderQty,
        BigDecimal remainQty,
        BigDecimal amount,
        LocalDate requestedDeliveryDate,
        Long categoryCodeId,
        String categoryName,
        EtcPurchaseOrderStatus status,
        LocalDate orderDate,
        boolean editable
) {
}
