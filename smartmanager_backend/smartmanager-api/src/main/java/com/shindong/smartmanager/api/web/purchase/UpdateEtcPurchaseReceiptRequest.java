package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;

public record UpdateEtcPurchaseReceiptRequest(
        @NotNull LocalDate receiptDate,
        @NotNull @DecimalMin("0.0001") BigDecimal receiptQty
) {
}
