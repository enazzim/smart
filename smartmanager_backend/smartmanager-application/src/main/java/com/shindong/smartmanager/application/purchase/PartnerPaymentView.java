package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentKind;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record PartnerPaymentView(
        long id,
        String paymentNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate paymentDate,
        PartnerPaymentCostCategory costCategory,
        PartnerPaymentKind paymentKind,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        String paymentMethod,
        String remark,
        PartnerPaymentStatus status,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<PartnerPaymentLineView> lines,
        String lineSummary
) {
}
