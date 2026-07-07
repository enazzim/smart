package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesCollectionSaveCommand(
        String collectionNo,
        long partnerId,
        LocalDate collectionDate,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        String paymentMethod,
        String remark
) {
}
