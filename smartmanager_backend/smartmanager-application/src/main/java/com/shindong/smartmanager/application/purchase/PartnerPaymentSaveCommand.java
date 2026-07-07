package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import java.math.BigDecimal;
import java.time.LocalDate;

public record PartnerPaymentSaveCommand(
        String paymentNo,
        long partnerId,
        LocalDate paymentDate,
        PartnerPaymentCostCategory costCategory,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        String paymentMethod,
        String remark
) {
}
