package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import java.math.BigDecimal;
import java.time.LocalDate;

/** FIFO 소진 대상 선지급 라인 (잔여 > 0). */
public record FifoPrepaidLineView(
        long paymentLineId,
        long paymentId,
        String paymentNo,
        LocalDate paymentDate,
        long partnerId,
        long itemId,
        PartnerPaymentCostCategory costCategory,
        BigDecimal lineTotalAmount,
        BigDecimal offsetAmount,
        BigDecimal remainingAmount
) {
}
