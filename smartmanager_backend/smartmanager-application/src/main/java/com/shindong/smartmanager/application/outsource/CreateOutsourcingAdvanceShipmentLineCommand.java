package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;

public record CreateOutsourcingAdvanceShipmentLineCommand(
        long parentItemId,
        long beginProcessCodeId,
        long endProcessCodeId,
        BigDecimal referenceQty,
        List<OutsourcingShipmentInputSaveCommand> inputLines
) {
    public CreateOutsourcingAdvanceShipmentLineCommand(
            long parentItemId,
            long beginProcessCodeId,
            long endProcessCodeId,
            BigDecimal referenceQty
    ) {
        this(parentItemId, beginProcessCodeId, endProcessCodeId, referenceQty, null);
    }
}
