package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record MrpPurchaseCandidateVendorView(
        long partnerId,
        String partnerName,
        String businessRegNo,
        BigDecimal orderRate,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Integer leadTimeDays,
        LocalDate requestedDeliveryDate
) {
}