package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkReportIssueOnHandView(
        long itemId,
        BigDecimal onHandQty
) {
}
