package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesOrderBulkFailure;

public record SalesOrderBulkFailureResponse(
        int rowIndex,
        String orderNo,
        String message
) {
    public static SalesOrderBulkFailureResponse from(SalesOrderBulkFailure failure) {
        return new SalesOrderBulkFailureResponse(failure.rowIndex(), failure.orderNo(), failure.message());
    }
}
