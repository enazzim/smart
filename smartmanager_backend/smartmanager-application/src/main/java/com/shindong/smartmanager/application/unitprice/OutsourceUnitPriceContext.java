package com.shindong.smartmanager.application.unitprice;

public record OutsourceUnitPriceContext(
        long itemId,
        long companyId,
        long beginProcessCodeId,
        long endProcessCodeId
) {
}
