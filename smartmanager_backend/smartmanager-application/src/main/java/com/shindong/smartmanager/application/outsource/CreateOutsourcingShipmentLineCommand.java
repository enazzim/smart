package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;

public record CreateOutsourcingShipmentLineCommand(
        long orderLineId,
        BigDecimal shipmentQty,
        List<OutsourcingShipmentInputLotCommand> inputLots
) {
    public CreateOutsourcingShipmentLineCommand(long orderLineId, BigDecimal shipmentQty) {
        this(orderLineId, shipmentQty, List.of());
    }
}
