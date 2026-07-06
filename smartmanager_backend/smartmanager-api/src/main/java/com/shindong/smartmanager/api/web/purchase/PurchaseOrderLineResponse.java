package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PurchaseOrderLineView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseOrderLineResponse(
        long id,
        int lineNo,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        BigDecimal orderQty,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long requirementLineId,
        String planNo,
        String runNo,
        LocalDate requestedDeliveryDate
) {
    public static PurchaseOrderLineResponse from(PurchaseOrderLineView view) {
        return new PurchaseOrderLineResponse(
                view.id(),
                view.lineNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.orderQty(),
                view.receivedQty(),
                view.waitingInspectionQty(),
                view.unitPrice(),
                view.amount(),
                view.requirementLineId(),
                view.planNo(),
                view.runNo(),
                view.requestedDeliveryDate()
        );
    }
}
