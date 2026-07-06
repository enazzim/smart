package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PurchaseReceiptView;
import com.shindong.smartmanager.domain.purchase.PurchaseReceiptStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record PurchaseReceiptResponse(
        long id,
        String receiptNo,
        long partnerId,
        String partnerName,
        LocalDate receiptDate,
        Long purchaseOrderId,
        PurchaseReceiptStatus status,
        Instant createdAt,
        String createdBy,
        List<PurchaseReceiptLineResponse> lines
) {
    public static PurchaseReceiptResponse from(PurchaseReceiptView view) {
        return new PurchaseReceiptResponse(
                view.id(),
                view.receiptNo(),
                view.partnerId(),
                view.partnerName(),
                view.receiptDate(),
                view.purchaseOrderId(),
                view.status(),
                view.createdAt(),
                view.createdBy(),
                view.lines().stream().map(PurchaseReceiptLineResponse::from).toList()
        );
    }
}
