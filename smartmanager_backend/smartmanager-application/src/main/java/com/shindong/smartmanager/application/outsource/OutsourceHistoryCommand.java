package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourceHistoryCommand(
        long companyId,
        long itemId,
        BigDecimal outsourceQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate historyDate,
        OutsourceHistorySourceType sourceType,
        long sourceId,
        int fiscalYear,
        int fiscalMonth,
        String actorUserId
) {
}
