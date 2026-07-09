package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptCandidateView;
import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcPurchaseReceiptCandidateResponse(
        long etcPurchaseOrderId,
        String orderNo,
        String itemName,
        long partnerId,
        String partnerName,
        BigDecimal unitPrice,
        BigDecimal orderQty,
        BigDecimal remainQty,
        BigDecimal amount,
        LocalDate requestedDeliveryDate,
        String categoryName,
        EtcPurchaseOrderStatus status,
        String statusLabel
) {
    public static EtcPurchaseReceiptCandidateResponse from(EtcPurchaseReceiptCandidateView view) {
        return new EtcPurchaseReceiptCandidateResponse(
                view.etcPurchaseOrderId(),
                view.orderNo(),
                view.itemName(),
                view.partnerId(),
                view.partnerName(),
                view.unitPrice(),
                view.orderQty(),
                view.remainQty(),
                view.amount(),
                view.requestedDeliveryDate(),
                view.categoryName(),
                view.status(),
                toStatusLabel(view.status())
        );
    }

    private static String toStatusLabel(EtcPurchaseOrderStatus status) {
        return switch (status) {
            case WAITING -> "대기";
            case IN_PROGRESS -> "진행";
            case COMPLETED -> "완료";
        };
    }
}
