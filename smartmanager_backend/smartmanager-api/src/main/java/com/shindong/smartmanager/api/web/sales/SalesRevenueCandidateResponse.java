package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesRevenueCandidateView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesRevenueCandidateResponse(
        long shipmentLineId,
        long shipmentId,
        String shipmentNo,
        LocalDate shipmentDate,
        long partnerId,
        String partnerName,
        long orderLineId,
        String orderNo,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal shippedQty,
        BigDecimal invoicedQty,
        BigDecimal remainingQty,
        BigDecimal deliveryOnHandQty,
        BigDecimal unitPrice,
        boolean billable,
        String billableMessage,
        boolean lotTracked,
        Long shipmentLotId
) {
    public static SalesRevenueCandidateResponse from(SalesRevenueCandidateView view) {
        return new SalesRevenueCandidateResponse(
                view.shipmentLineId(),
                view.shipmentId(),
                view.shipmentNo(),
                view.shipmentDate(),
                view.partnerId(),
                view.partnerName(),
                view.orderLineId(),
                view.orderNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.shippedQty(),
                view.invoicedQty(),
                view.remainingQty(),
                view.deliveryOnHandQty(),
                view.unitPrice(),
                view.billable(),
                view.billableMessage(),
                view.lotTracked(),
                view.shipmentLotId()
        );
    }
}
