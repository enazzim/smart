package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;

public record CreateOutsourcingReceiptLineCommand(
        long outsourcingOrderLineId,
        BigDecimal receiptQty,
        String lotNo,
        boolean autoGenerateLot,
        Long lotId,
        List<OutsourcingReceiptInputLotCommand> inputLots
) {
    public CreateOutsourcingReceiptLineCommand(long outsourcingOrderLineId, BigDecimal receiptQty) {
        this(outsourcingOrderLineId, receiptQty, null, false, null, List.of());
    }
}
