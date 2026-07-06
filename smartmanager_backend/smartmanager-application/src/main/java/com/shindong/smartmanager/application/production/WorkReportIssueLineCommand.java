package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkReportIssueLineCommand(
        long itemCompositionId,
        long itemId,
        BigDecimal issueQty
) {
}
