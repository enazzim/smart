package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentKind;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record PartnerPaymentSaveCommand(
        String paymentNo,
        long partnerId,
        LocalDate paymentDate,
        PartnerPaymentCostCategory costCategory,
        PartnerPaymentKind paymentKind,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        String paymentMethod,
        String remark,
        List<PartnerPaymentLineCommand> lines
) {
}
