package com.shindong.smartmanager.api.web.purchase;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;
import java.util.List;

public record CreatePurchaseReceiptRequest(
        @NotNull LocalDate receiptDate,
        @NotEmpty List<@Valid CreatePurchaseReceiptLineRequest> lines
) {
}
