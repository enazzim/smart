package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesOrderBulkResult;
import java.util.List;

public record SalesOrderBulkResponse(
        int successCount,
        int failureCount,
        List<SalesOrderResponse> created,
        List<SalesOrderBulkFailureResponse> failures
) {
    public static SalesOrderBulkResponse from(SalesOrderBulkResult result) {
        return new SalesOrderBulkResponse(
                result.successCount(),
                result.failureCount(),
                result.created().stream().map(SalesOrderResponse::from).toList(),
                result.failures().stream().map(SalesOrderBulkFailureResponse::from).toList()
        );
    }
}
