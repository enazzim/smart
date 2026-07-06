package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record MaterialIssueLineCommand(
        long itemCompositionId,
        BigDecimal issueQty
) {
}
