package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesCollectionView;
import com.shindong.smartmanager.domain.sales.SalesCollectionStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record SalesCollectionResponse(
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
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable
) {
    public static SalesCollectionResponse from(SalesCollectionView view) {
        return new SalesCollectionResponse(
                view.id(),
                view.collectionNo(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.collectionDate(),
                view.supplyAmount(),
                view.vatAmount(),
                view.totalAmount(),
                view.paymentMethod(),
                view.remark(),
                view.status(),
                statusLabel(view.status()),
                view.createdAt(),
                view.createdBy(),
                view.cancelable()
        );
    }

    private static String statusLabel(SalesCollectionStatus status) {
        return switch (status) {
            case ISSUED -> "수금";
            case CANCELLED -> "취소";
        };
    }
}
