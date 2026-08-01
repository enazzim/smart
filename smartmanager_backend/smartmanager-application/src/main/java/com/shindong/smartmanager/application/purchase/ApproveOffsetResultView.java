package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import java.math.BigDecimal;
import java.util.List;

public record ApproveOffsetResultView(
        int itemCount,
        BigDecimal totalApproveAmount,
        BigDecimal totalOffsetAmount,
        BigDecimal totalUnpaidIncrease,
        List<ApproveOffsetItemView> offsets
) {
    public record ApproveOffsetItemView(
            PayableApprovalLedgerKind ledgerKind,
            long historyId,
            long partnerId,
            String partnerName,
            Long itemId,
            String itemNo,
            String itemName,
            PartnerPaymentCostCategory costCategory,
            BigDecimal approveAmount,
            BigDecimal offsetAmount,
            BigDecimal prepaidAfter,
            BigDecimal unpaidIncrease,
            boolean offsetApplicable
    ) {
    }
}
