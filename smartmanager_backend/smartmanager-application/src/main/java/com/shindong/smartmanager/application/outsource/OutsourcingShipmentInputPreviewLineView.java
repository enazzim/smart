package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;

public record OutsourcingShipmentInputPreviewLineView(
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        Long itemCompositionId,
        BigDecimal unitRatio,
        BigDecimal issueQty,
        String sourceLocationCode,
        Long sourceProcessId,
        long inputProcessId,
        String inputProcessName,
        BigDecimal onHandQty
) {
}
