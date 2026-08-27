package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record DefectClaimCommand(
        long partnerId,
        long itemId,
        LocalDate receiptDate,
        BigDecimal claimQty,
        BigDecimal amount,
        String reason
) {
}
