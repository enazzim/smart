package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;
import org.springframework.format.annotation.DateTimeFormat;

public record CreatePurchaseOrderFromMrpLineRequest(
        @NotNull Long requirementLineId,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate requestedDeliveryDate
) {
}
