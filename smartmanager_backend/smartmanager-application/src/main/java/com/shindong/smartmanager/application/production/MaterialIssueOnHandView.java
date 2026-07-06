package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;

public record MaterialIssueOnHandView(
        long itemId,
        BigDecimal onHandQty
) {
}
