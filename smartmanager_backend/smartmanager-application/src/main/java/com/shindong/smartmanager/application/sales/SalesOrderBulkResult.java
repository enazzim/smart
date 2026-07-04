package com.shindong.smartmanager.application.sales;

import java.util.List;

public record SalesOrderBulkResult(
        int successCount,
        int failureCount,
        List<SalesOrderView> created,
        List<SalesOrderBulkFailure> failures
) {
}
