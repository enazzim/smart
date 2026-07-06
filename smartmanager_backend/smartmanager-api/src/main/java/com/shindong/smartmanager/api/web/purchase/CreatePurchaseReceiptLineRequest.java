package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import java.math.BigDecimal;

public record CreatePurchaseReceiptLineRequest(
        @NotNull Long purchaseOrderLineId,
        @NotNull @Positive BigDecimal receiptQty
) {
}
