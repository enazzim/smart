package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;

public record OutsourcingShipmentInputSaveCommand(
        long itemId,
        Long itemCompositionId,
        BigDecimal issueQty,
        String sourceLocationCode,
        Long sourceProcessId,
        long inputProcessId,
        Long lotId
) {
    public OutsourcingShipmentInputSaveCommand(
            long itemId,
            Long itemCompositionId,
            BigDecimal issueQty,
            String sourceLocationCode,
            Long sourceProcessId,
            long inputProcessId
    ) {
        this(itemId, itemCompositionId, issueQty, sourceLocationCode, sourceProcessId, inputProcessId, null);
    }
}
