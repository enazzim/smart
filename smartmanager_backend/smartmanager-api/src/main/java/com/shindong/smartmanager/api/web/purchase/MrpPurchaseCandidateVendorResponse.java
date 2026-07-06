package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.MrpPurchaseCandidateVendorView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record MrpPurchaseCandidateVendorResponse(
        long partnerId,
        String partnerName,
        String businessRegNo,
        BigDecimal orderRate,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Integer leadTimeDays,
        LocalDate requestedDeliveryDate
) {
    public static MrpPurchaseCandidateVendorResponse from(MrpPurchaseCandidateVendorView view) {
        return new MrpPurchaseCandidateVendorResponse(
                view.partnerId(),
                view.partnerName(),
                view.businessRegNo(),
                view.orderRate(),
                view.orderQty(),
                view.unitPrice(),
                view.amount(),
                view.leadTimeDays(),
                view.requestedDeliveryDate()
        );
    }
}