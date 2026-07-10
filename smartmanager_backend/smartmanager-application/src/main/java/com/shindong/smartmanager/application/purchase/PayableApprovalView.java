package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record PayableApprovalView(
        PayableApprovalLedgerKind ledgerKind,
        long historyId,
        long partnerId,
        String partnerName,
        LocalDate receiptDate,
        String itemNo,
        String itemName,
        String drawingNo,
        String processName,
        BigDecimal qty,
        BigDecimal standardUnitPrice,
        BigDecimal unitPrice,
        BigDecimal amount,
        int fiscalYear,
        int fiscalMonth,
        String categoryLabel,
        PayableApprovalStatus approvalStatus,
        Instant approvedAt,
        String approvedByName,
        Instant approvalCancelledAt,
        String approvalCancelledByName
) {
}
