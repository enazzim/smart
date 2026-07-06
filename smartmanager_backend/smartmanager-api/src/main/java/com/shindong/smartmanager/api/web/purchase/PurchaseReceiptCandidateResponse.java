package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PurchaseReceiptCandidateView;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseReceiptCandidateResponse(
        long purchaseOrderId,
        long purchaseOrderLineId,
        String orderNo,
        LocalDate orderDate,
        long partnerId,
        String partnerName,
        long itemId,
        String itemNum,
        String itemName,
        CheckDistinction checkDistinction,
        BigDecimal orderQty,
        BigDecimal receivedQty,
        BigDecimal remainQty,
        BigDecimal waitingInspectionQty,
        BigDecimal unitPrice,
        LocalDate requestedDeliveryDate
) {
    public static PurchaseReceiptCandidateResponse from(PurchaseReceiptCandidateView view) {
        return new PurchaseReceiptCandidateResponse(
                view.purchaseOrderId(),
                view.purchaseOrderLineId(),
                view.orderNo(),
                view.orderDate(),
                view.partnerId(),
                view.partnerName(),
                view.itemId(),
                view.itemNum(),
                view.itemName(),
                view.checkDistinction(),
                view.orderQty(),
                view.receivedQty(),
                view.remainQty(),
                view.waitingInspectionQty(),
                view.unitPrice(),
                view.requestedDeliveryDate()
        );
    }
}
