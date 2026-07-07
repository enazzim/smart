package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;

public record OutsourcingShipmentLineSaveCommand(
        Long orderLineId,
        BigDecimal shipmentQty,
        Long parentItemId,
        Long beginProcessCodeId,
        Long endProcessCodeId,
        List<OutsourcingShipmentInputSaveCommand> inputLines
) {
}
