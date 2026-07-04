package com.shindong.smartmanager.application.sales;

public record SalesOrderBulkFailure(
        int rowIndex,
        String orderNo,
        String message
) {
}
