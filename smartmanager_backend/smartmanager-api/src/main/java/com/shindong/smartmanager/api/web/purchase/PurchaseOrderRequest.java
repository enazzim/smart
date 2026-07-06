package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderSourceType;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;
import java.util.List;

public record PurchaseOrderRequest(
        String orderNo,
        @NotNull Long partnerId,
        @NotNull LocalDate orderDate,
        PurchaseOrderSourceType sourceType,
        @NotEmpty @Valid List<PurchaseOrderLineRequest> lines
) {
}
