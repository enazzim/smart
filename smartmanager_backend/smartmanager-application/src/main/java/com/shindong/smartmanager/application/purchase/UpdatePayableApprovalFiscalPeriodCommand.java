package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;

public record UpdatePayableApprovalFiscalPeriodCommand(
        PayableApprovalLedgerKind ledgerKind,
        long historyId,
        Integer fiscalYear,
        Integer fiscalMonth
) {
}
