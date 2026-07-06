package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;
import java.util.List;

public record CreatePurchaseOrderFromMrpRequest(
        @NotNull Long partnerId,
        LocalDate orderDate,
        @NotEmpty @Valid List<CreatePurchaseOrderFromMrpLineRequest> lines
) {
}
