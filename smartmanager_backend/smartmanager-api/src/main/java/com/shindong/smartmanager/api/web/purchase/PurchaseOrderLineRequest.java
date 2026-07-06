package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;

public record PurchaseOrderLineRequest(
        @NotNull Long itemId,
        @NotNull BigDecimal orderQty,
        BigDecimal unitPrice,
        Long requirementLineId
) {
}
