package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PurchaseOrderView;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderSourceType;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record PurchaseOrderResponse(
        long id,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        PurchaseOrderSourceType sourceType,
        String sourceTypeLabel,
        PurchaseOrderStatus status,
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<PurchaseOrderLineResponse> lines
) {
    public static PurchaseOrderResponse from(PurchaseOrderView view) {
        return new PurchaseOrderResponse(
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
                view.lines().stream().map(PurchaseOrderLineResponse::from).toList()
        );
    }

    private static String sourceTypeLabel(PurchaseOrderSourceType sourceType) {
        return switch (sourceType) {
            case MRP -> "자재소요";
            case SALES_ORDER -> "수주";
            case MANUAL -> "수동";
        };
    }

    private static String statusLabel(PurchaseOrderStatus status) {
        return switch (status) {
            case DRAFT -> "작성중";
            case CONFIRMED -> "확정";
            case IN_PROGRESS -> "입고중";
            case RECEIVED -> "입고완료";
            case CANCELLED -> "취소";
        };
    }
}
