package com.shindong.smartmanager.api.web.sales;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;
import java.util.List;

public record SalesOrderRequest(
        String orderNo,
        @NotNull Long partnerId,
        @NotNull LocalDate orderDate,
        @NotNull LocalDate requestedDeliveryDate,
        String remark,
        @Valid @NotNull List<SalesOrderLineRequest> lines
) {
}
