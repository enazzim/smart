package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseHistoryRecord(
        long id,
        long companyId,
        BigDecimal amount,
        LocalDate historyDate,
        int fiscalYear,
        int fiscalMonth,
        PayableApprovalStatus approvalStatus
) {
}
