package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseReceiptStatus;
import java.time.LocalDate;
import java.util.List;

public record PurchaseReceiptSaveCommand(
        String receiptNo,
        long partnerId,
        LocalDate receiptDate,
        Long purchaseOrderId,
        PurchaseReceiptStatus status,
        List<PurchaseReceiptLineSaveCommand> lines
) {
}
