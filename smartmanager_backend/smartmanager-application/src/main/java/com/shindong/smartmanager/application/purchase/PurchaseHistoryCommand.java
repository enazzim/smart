package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseHistoryCommand(
        long companyId,
        long itemId,
        BigDecimal purchaseQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate historyDate,
        PurchaseHistorySourceType sourceType,
        long sourceId,
        int fiscalYear,
        int fiscalMonth,
        String actorUserId
) {
}
