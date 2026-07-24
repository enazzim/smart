package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record DefectClaimListCriteria(
        Long partnerId,
        String partnerName,
        Long itemId,
        String itemNo,
        String itemName,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo
) {
}
