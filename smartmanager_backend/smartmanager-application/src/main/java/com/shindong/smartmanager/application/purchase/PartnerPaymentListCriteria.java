package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import java.time.LocalDate;

public record PartnerPaymentListCriteria(
        LocalDate paymentDateFrom,
        LocalDate paymentDateTo,
        String paymentNo,
        String partnerName,
        PartnerPaymentStatus status,
        boolean excludeCancelled
) {
}
