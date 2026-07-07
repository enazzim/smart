package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record MaterialIssueLineCommand(
        Long itemCompositionId,
        Long itemId,
        BigDecimal issueQty
) {
}
