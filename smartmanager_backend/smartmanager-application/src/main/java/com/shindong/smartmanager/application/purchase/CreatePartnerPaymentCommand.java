package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import java.math.BigDecimal;
import java.time.LocalDate;

public record CreatePartnerPaymentCommand(
        long partnerId,
        LocalDate paymentDate,
        PartnerPaymentCostCategory costCategory,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        String paymentMethod,
        String remark
) {
}
