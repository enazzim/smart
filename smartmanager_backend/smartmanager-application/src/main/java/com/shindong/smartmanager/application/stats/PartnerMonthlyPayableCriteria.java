package com.shindong.smartmanager.application.stats;

public record PartnerMonthlyPayableCriteria(
        Long companyId,
        String companyName,
        int fiscalYear,
        int fiscalMonth
) {
}
