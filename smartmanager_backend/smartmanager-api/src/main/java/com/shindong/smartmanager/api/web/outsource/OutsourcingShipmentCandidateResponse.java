package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingShipmentCandidateView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingShipmentCandidateResponse(
        long orderLineId,
        long orderId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        String processName,
        String beginProcessName,
        String endProcessName,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal remainingQty,
        boolean shippable,
        String shippableMessage
) {
    public static OutsourcingShipmentCandidateResponse from(OutsourcingShipmentCandidateView view) {
        return new OutsourcingShipmentCandidateResponse(
                view.orderLineId(),
                view.orderId(),
                view.orderNo(),
                view.orderDate(),
                view.partnerId(),
                view.partnerName(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processName(),
                view.beginProcessName(),
                view.endProcessName(),
                view.orderQty(),
                view.shippedQty(),
                view.remainingQty(),
                view.shippable(),
                view.shippableMessage()
        );
    }
}
