package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;

public record EtcClaimListCriteria(
        Long partnerId,
        String partnerName,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        String reason,
        LocalDate registeredOn
) {
}
