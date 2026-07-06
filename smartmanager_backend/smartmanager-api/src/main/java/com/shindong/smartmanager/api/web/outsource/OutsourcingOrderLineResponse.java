package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record OutsourcingOrderLineResponse(
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
    public static OutsourcingOrderLineResponse from(OutsourcingOrderLineView view) {
        return new OutsourcingOrderLineResponse(
                view.id(),
                view.lineNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.processSequenceId(),
                view.processSequenceNum(),
                view.processCode(),
                view.processName(),
                view.beginProcessCodeId(),
                view.beginProcessCode(),
                view.beginProcessName(),
                view.endProcessCodeId(),
                view.endProcessCode(),
                view.endProcessName(),
                view.workPlanId(),
                view.planNo(),
                view.orderQty(),
                view.shippedQty(),
                view.receivedQty(),
                view.unitPrice(),
                view.amount(),
                view.requestedDeliveryDate()
        );
    }
}
