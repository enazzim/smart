package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkReportConsumptionSaveCommand(
        long itemId,
        Long itemCompositionId,
        BigDecimal issueQty,
        String locationCode,
        Long sourceProcessId
) {
}
