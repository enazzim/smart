package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record PurchaseReceiptCandidateCriteria(
        String partnerName,
        String orderNo,
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        String itemNum,
        String itemName,
        /** GENERAL: 원자재·상품 / SUB_MATERIAL: 부자재 / null·빈값: 전체 */
        String itemPropertyScope
) {
}
