package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesShipmentCandidateView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesShipmentCandidateResponse(
        long orderLineId,
        long orderId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal remainingQty,
        BigDecimal salesOnHandQty,
        boolean shippable,
        String shippableMessage
) {
    public static SalesShipmentCandidateResponse from(SalesShipmentCandidateView view) {
        return new SalesShipmentCandidateResponse(
                view.orderLineId(),
                view.orderId(),
                view.orderNo(),
                view.orderDate(),
                view.partnerId(),
                view.partnerName(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.orderQty(),
                view.shippedQty(),
                view.remainingQty(),
                view.salesOnHandQty(),
                view.shippable(),
                view.shippableMessage()
        );
    }
}
