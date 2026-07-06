package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record WorkPlanOutsourceCandidateVendorView(
        long partnerId,
        String partnerName,
        String businessRegNo,
        long beginProcessCodeId,
        String beginProcessCode,
        String beginProcessName,
        long endProcessCodeId,
        String endProcessCode,
        String endProcessName,
        BigDecimal orderRate,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate requestedDeliveryDate
) {
}
