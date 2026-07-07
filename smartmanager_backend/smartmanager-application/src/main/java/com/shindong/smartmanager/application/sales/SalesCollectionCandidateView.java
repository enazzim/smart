package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesCollectionCandidateView(
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        BigDecimal revenueAmount,
        BigDecimal collectedAmount,
        BigDecimal uncollectedAmount,
        boolean collectable
) {
}
