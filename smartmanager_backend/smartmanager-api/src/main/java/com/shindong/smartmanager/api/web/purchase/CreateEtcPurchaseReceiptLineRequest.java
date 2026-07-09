package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;

public record CreateEtcPurchaseReceiptLineRequest(
        @NotNull Long etcPurchaseOrderId,
        @NotNull @DecimalMin("0.0001") BigDecimal receiptQty
) {
}
