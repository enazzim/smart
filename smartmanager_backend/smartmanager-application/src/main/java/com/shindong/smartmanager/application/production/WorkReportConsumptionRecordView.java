package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record WorkReportConsumptionRecordView(
        long id,
        long workReportId,
        short lineNo,
        long itemId,
        Long itemCompositionId,
        BigDecimal issueQty,
        String locationCode,
        Long sourceProcessId,
        Long lotId
) {
}
