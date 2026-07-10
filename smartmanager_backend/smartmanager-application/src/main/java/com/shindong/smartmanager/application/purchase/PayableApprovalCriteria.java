package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import java.time.LocalDate;

public record PayableApprovalCriteria(
        String partnerName,
        String itemNo,
        String itemName,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        Integer fiscalYear,
        Integer fiscalMonth
) {
}
