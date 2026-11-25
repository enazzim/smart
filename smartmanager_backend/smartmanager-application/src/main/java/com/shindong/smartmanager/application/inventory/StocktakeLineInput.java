package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public record StocktakeLineInput(
        int rowNo,
        String itemNo,
        Long processSequenceId,
        String partnerNo,
        String lotNo,
        BigDecimal countedQty,
        BigDecimal expectedBookQty
) {
}
