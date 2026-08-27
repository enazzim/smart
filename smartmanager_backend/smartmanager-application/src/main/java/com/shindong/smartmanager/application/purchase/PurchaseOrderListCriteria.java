package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.time.LocalDate;

public record PurchaseOrderListCriteria(
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        String partnerName,
        String orderNo,
        PurchaseOrderStatus status,
        boolean excludeCancelled,
        /** GENERAL: 원자재·상품 / SUB_MATERIAL: 부자재 / null·빈값: 전체 */
        String itemPropertyScope
) {
}
