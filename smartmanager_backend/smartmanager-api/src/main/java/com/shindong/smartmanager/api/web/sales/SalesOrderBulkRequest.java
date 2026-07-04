package com.shindong.smartmanager.api.web.sales;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import java.util.List;

public record SalesOrderBulkRequest(
        @Valid @NotEmpty List<SalesOrderRequest> orders
) {
}
