package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseReceiptStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record PurchaseReceiptView(
        long id,
        String receiptNo,
        long partnerId,
        String partnerName,
        LocalDate receiptDate,
        Long purchaseOrderId,
        PurchaseReceiptStatus status,
        Instant createdAt,
        String createdBy,
        List<PurchaseReceiptLineView> lines
) {
}
