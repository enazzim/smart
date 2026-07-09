package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderView;
import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcPurchaseOrderResponse(
        long id,
        String orderNo,
        String itemName,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        BigDecimal unitPrice,
        BigDecimal orderQty,
        BigDecimal remainQty,
        BigDecimal amount,
        LocalDate requestedDeliveryDate,
        Long categoryCodeId,
        String categoryName,
        EtcPurchaseOrderStatus status,
        String statusLabel,
        LocalDate orderDate,
        boolean editable
) {
    public static EtcPurchaseOrderResponse from(EtcPurchaseOrderView view) {
        return new EtcPurchaseOrderResponse(
                view.id(),
                view.orderNo(),
                view.itemName(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.unitPrice(),
                view.orderQty(),
                view.remainQty(),
                view.amount(),
                view.requestedDeliveryDate(),
                view.categoryCodeId(),
                view.categoryName(),
                view.status(),
                statusLabel(view.status()),
                view.orderDate(),
                view.editable()
        );
    }

    private static String statusLabel(EtcPurchaseOrderStatus status) {
        return switch (status) {
            case WAITING -> "대기";
            case IN_PROGRESS -> "진행";
            case COMPLETED -> "완료";
        };
    }
}
