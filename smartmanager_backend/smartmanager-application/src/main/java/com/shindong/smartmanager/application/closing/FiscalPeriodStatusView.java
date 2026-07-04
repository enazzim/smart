package com.shindong.smartmanager.application.closing;

import java.time.LocalDate;

public record FiscalPeriodStatusView(
        LocalDate referenceDate,
        int fiscalYear,
        int fiscalMonth,
        boolean closed
) {
}
