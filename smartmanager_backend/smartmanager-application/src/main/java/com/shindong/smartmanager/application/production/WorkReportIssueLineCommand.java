package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkReportIssueLineCommand(
        Long itemCompositionId,
        long itemId,
        BigDecimal issueQty,
        Long lotId
) {
    public WorkReportIssueLineCommand(Long itemCompositionId, long itemId, BigDecimal issueQty) {
        this(itemCompositionId, itemId, issueQty, null);
    }
}
