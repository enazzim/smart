package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;

public record OutsourcingShipmentInputLineView(
        long itemId,
        String itemNo,
        String itemName,
        Long itemCompositionId,
        BigDecimal issueQty,
        String sourceLocationCode,
        Long sourceProcessId,
        long inputProcessId,
        Long lotId
) {
}
