package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentKind;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreatePartnerPaymentCommand(
        long partnerId,
        LocalDate paymentDate,
        PartnerPaymentCostCategory costCategory,
        PartnerPaymentKind paymentKind,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        String paymentMethod,
        String remark,
        List<PartnerPaymentLineCommand> lines
) {
}
