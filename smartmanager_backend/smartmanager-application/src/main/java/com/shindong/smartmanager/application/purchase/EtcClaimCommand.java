package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcClaimCommand(
        long partnerId,
        LocalDate receiptDate,
        String reason,
        BigDecimal amount
) {
}
