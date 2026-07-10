package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PayableApprovalView;
import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record PayableApprovalResponse(
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
        String categoryLabel,
        int fiscalYear,
        int fiscalMonth,
        PayableApprovalStatus approvalStatus,
        Instant approvedAt,
        String approvedByName,
        Instant approvalCancelledAt,
        String approvalCancelledByName
) {
    public static PayableApprovalResponse from(PayableApprovalView view) {
        return new PayableApprovalResponse(
                view.ledgerKind(),
                view.historyId(),
                view.partnerId(),
                view.partnerName(),
                view.receiptDate(),
                view.itemNo(),
                view.itemName(),
                view.drawingNo(),
                view.processName(),
                view.qty(),
                view.standardUnitPrice(),
                view.unitPrice(),
                view.amount(),
                view.categoryLabel(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.approvalStatus(),
                view.approvedAt(),
                view.approvedByName(),
                view.approvalCancelledAt(),
                view.approvalCancelledByName()
        );
    }
}
