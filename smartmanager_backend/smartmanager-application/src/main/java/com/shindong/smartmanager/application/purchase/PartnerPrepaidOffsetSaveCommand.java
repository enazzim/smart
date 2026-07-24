package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPrepaidOffsetLedgerKind;
import java.math.BigDecimal;

public record PartnerPrepaidOffsetSaveCommand(
        long paymentLineId,
        PartnerPrepaidOffsetLedgerKind ledgerKind,
        long historyId,
        BigDecimal amount
) {
}
