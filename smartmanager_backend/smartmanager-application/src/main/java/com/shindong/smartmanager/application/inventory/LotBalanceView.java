package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public record LotBalanceView(
        long id,
        long lotId,
        long inventoryBalanceId,
        String locationCode,
        String locationLabel,
        Long outputProcessId,
        Short outputProcessSequence,
        String outputProcessName,
        BigDecimal qtyOnHand
) {
}
