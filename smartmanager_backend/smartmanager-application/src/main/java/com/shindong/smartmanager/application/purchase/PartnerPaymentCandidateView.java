package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;

public record PartnerPaymentCandidateView(
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        BigDecimal purchasePayableAmount,
        BigDecimal outsourcePayableAmount,
        BigDecimal totalPayableAmount,
        BigDecimal paidAmount,
        BigDecimal unpaidAmount,
        boolean payable
) {
}
