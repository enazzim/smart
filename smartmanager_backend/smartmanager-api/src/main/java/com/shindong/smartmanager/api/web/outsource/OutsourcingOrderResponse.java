package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderView;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderSourceType;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingOrderResponse(
        long id,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        OutsourcingOrderSourceType sourceType,
        String sourceTypeLabel,
        OutsourcingOrderStatus status,
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<OutsourcingOrderLineResponse> lines
) {
    public static OutsourcingOrderResponse from(OutsourcingOrderView view) {
        return new OutsourcingOrderResponse(
                view.id(),
                view.orderNo(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.orderDate(),
                view.sourceType(),
                sourceTypeLabel(view.sourceType()),
                view.status(),
                statusLabel(view.status()),
                view.createdAt(),
                view.createdBy(),
                view.cancelable(),
                view.lines().stream().map(OutsourcingOrderLineResponse::from).toList()
        );
    }

    private static String sourceTypeLabel(OutsourcingOrderSourceType sourceType) {
        return switch (sourceType) {
            case WORK_PLAN -> "작업계획";
            case MANUAL -> "수동";
        };
    }

    private static String statusLabel(OutsourcingOrderStatus status) {
        return switch (status) {
            case CONFIRMED -> "확정";
            case IN_PROGRESS -> "출고중";
            case RECEIVED -> "입고완료";
            case CANCELLED -> "취소";
        };
    }
}
