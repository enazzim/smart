package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourceHistoryRecord(
        long id,
        long companyId,
        BigDecimal amount,
        LocalDate historyDate,
        int fiscalYear,
        int fiscalMonth,
        PayableApprovalStatus approvalStatus
) {
}
