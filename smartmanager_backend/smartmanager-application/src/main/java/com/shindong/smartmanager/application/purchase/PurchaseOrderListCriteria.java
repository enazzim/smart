package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.time.LocalDate;

public record PurchaseOrderListCriteria(
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        String partnerName,
        String orderNo,
        PurchaseOrderStatus status,
        boolean excludeCancelled
) {
}
