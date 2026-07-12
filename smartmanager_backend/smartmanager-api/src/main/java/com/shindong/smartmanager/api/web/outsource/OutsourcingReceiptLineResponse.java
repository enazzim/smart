package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingReceiptLineView;
import java.math.BigDecimal;

public record OutsourcingReceiptLineResponse(
        long id,
        int lineNo,
        long outsourcingOrderLineId,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal receiptQty,
        BigDecimal postedQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long qualityInspectionId,
        boolean stockPosted,
        Long lotId
) {
    public static OutsourcingReceiptLineResponse from(OutsourcingReceiptLineView view) {
        return new OutsourcingReceiptLineResponse(
                view.id(),
                view.lineNo(),
                view.outsourcingOrderLineId(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.receiptQty(),
                view.postedQty(),
                view.unitPrice(),
                view.amount(),
                view.qualityInspectionId(),
                view.stockPosted(),
                view.lotId()
        );
    }
}
