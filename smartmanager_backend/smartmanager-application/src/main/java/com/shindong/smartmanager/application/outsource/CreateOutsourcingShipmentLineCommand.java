package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;

public record CreateOutsourcingShipmentLineCommand(
        long orderLineId,
        BigDecimal shipmentQty
) {
}
