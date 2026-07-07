package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesHistorySourceType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesHistoryCommand(
        long companyId,
        long itemId,
        BigDecimal salesQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate historyDate,
        SalesHistorySourceType sourceType,
        long sourceId,
        int fiscalYear,
        int fiscalMonth,
        String actorUserId
) {
}
