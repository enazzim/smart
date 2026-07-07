package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateView;
import java.math.BigDecimal;

public record PartnerPaymentCandidateResponse(
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
    public static PartnerPaymentCandidateResponse from(PartnerPaymentCandidateView view) {
        return new PartnerPaymentCandidateResponse(
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.purchasePayableAmount(),
                view.outsourcePayableAmount(),
                view.totalPayableAmount(),
                view.paidAmount(),
                view.unpaidAmount(),
                view.payable()
        );
    }
}
