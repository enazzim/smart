package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import java.time.LocalDate;

public record PayableApprovalCriteria(
        String partnerName,
        String itemNo,
        String drawingNo,
        String itemName,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo
) {
}
