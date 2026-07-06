package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingOrderLineView(
        long id,
        int lineNo,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        long processSequenceId,
        short processSequenceNum,
        String processCode,
        String processName,
        long beginProcessCodeId,
        String beginProcessCode,
        String beginProcessName,
        long endProcessCodeId,
        String endProcessCode,
        String endProcessName,
        Long workPlanId,
        String planNo,
        BigDecimal orderQty,
        BigDecimal shippedQty,
        BigDecimal receivedQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate requestedDeliveryDate
) {
}
