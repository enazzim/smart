package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;

public record CreateSalesCollectionCommand(
        long partnerId,
        LocalDate collectionDate,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        String paymentMethod,
        String remark
) {
}
