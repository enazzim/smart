package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PurchaseReceiptLineView;
import java.math.BigDecimal;

public record PurchaseReceiptLineResponse(
        long id,
        int lineNo,
        long purchaseOrderLineId,
        long itemId,
        String itemNum,
        String itemName,
        BigDecimal receiptQty,
        BigDecimal postedQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long qualityInspectionId,
        boolean postedImmediately
) {
    public static PurchaseReceiptLineResponse from(PurchaseReceiptLineView view) {
        return new PurchaseReceiptLineResponse(
                view.id(),
                view.lineNo(),
                view.purchaseOrderLineId(),
                view.itemId(),
                view.itemNum(),
                view.itemName(),
                view.receiptQty(),
                view.postedQty(),
                view.unitPrice(),
                view.amount(),
                view.qualityInspectionId(),
                view.postedImmediately()
        );
    }
}
