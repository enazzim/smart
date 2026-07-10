package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;

public record PayableApprovalItemCommand(
        PayableApprovalLedgerKind ledgerKind,
        long historyId
) {
}
