package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record MaterialIssueLineSaveCommand(
        long itemId,
        long itemCompositionId,
        BigDecimal issueQty,
        String locationCode,
        Long sourceProcessId
) {
}
