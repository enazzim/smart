package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;

public record PartnerMonthlyPayableView(
        long companyId,
        String companyName,
        int fiscalYear,
        int fiscalMonth,
        BigDecimal approvedAmount,
        BigDecimal offsetAmount,
        BigDecimal payableAmount
) {
}
