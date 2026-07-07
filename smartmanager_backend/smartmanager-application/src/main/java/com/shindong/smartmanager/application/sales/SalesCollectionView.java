package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesCollectionStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record SalesCollectionView(
        long id,
        String collectionNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate collectionDate,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        String paymentMethod,
        String remark,
        SalesCollectionStatus status,
        Instant createdAt,
        String createdBy,
        boolean cancelable
) {
}
