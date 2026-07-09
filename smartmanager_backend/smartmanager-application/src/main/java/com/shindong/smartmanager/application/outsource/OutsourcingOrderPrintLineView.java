package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingOrderPrintLineView(
        int lineNo,
        String itemName,
        String itemNo,
        String material,
        String standard,
        String beginProcessName,
        String endProcessName,
        String unit,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate requestedDeliveryDate,
        String remarks
) {
}
