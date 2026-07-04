package com.shindong.smartmanager.application.closing;

import java.time.Instant;

public record MonthClosingView(
        long id,
        int fiscalYear,
        int fiscalMonth,
        Instant closedAt,
        String closedBy,
        String closedById
) {
}
