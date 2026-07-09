package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcPurchaseReceiptResponse(
        long id,
        String receiptNo,
        long etcPurchaseOrderId,
        String orderNo,
        long partnerId,
        String partnerName,
        String itemName,
        BigDecimal receiptQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate receiptDate,
        int fiscalYear,
        int fiscalMonth
) {
    public static EtcPurchaseReceiptResponse from(EtcPurchaseReceiptView view) {
        return new EtcPurchaseReceiptResponse(
                view.id(),
                view.receiptNo(),
                view.etcPurchaseOrderId(),
                view.orderNo(),
                view.partnerId(),
                view.partnerName(),
                view.itemName(),
                view.receiptQty(),
                view.unitPrice(),
                view.amount(),
                view.receiptDate(),
                view.fiscalYear(),
                view.fiscalMonth()
        );
    }
}
