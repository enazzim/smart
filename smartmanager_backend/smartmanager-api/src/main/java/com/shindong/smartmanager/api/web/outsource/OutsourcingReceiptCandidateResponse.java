package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingReceiptCandidateView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingReceiptCandidateResponse(
        long outsourcingOrderLineId,
        long outsourcingOrderId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        String processName,
        String checkDistinction,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal remainQty,
        BigDecimal unitPrice,
        LocalDate requestedDeliveryDate
) {
    public static OutsourcingReceiptCandidateResponse from(OutsourcingReceiptCandidateView view) {
        return new OutsourcingReceiptCandidateResponse(
                view.outsourcingOrderLineId(),
                view.outsourcingOrderId(),
                view.orderNo(),
                view.orderDate(),
                view.partnerId(),
                view.partnerName(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processName(),
                view.checkDistinction(),
                view.orderQty(),
                view.shippedQty(),
                view.receivedQty(),
                view.waitingInspectionQty(),
                view.remainQty(),
                view.unitPrice(),
                view.requestedDeliveryDate()
        );
    }
}
