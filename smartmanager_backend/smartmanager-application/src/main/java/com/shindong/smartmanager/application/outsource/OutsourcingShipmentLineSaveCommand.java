package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;

public record OutsourcingShipmentLineSaveCommand(
        long orderLineId,
        BigDecimal shipmentQty,
        List<OutsourcingShipmentInputSaveCommand> inputLines
) {
}
