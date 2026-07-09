package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;

public record UpdateEtcPurchaseOrderRequest(
        @NotBlank String itemName,
        @NotNull Long partnerId,
        @NotNull @DecimalMin("0") BigDecimal unitPrice,
        @NotNull @DecimalMin("0.0001") BigDecimal orderQty,
        @NotNull LocalDate requestedDeliveryDate,
        Long categoryCodeId
) {
}
