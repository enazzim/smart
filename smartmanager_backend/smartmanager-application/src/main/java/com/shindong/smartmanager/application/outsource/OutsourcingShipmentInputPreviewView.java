package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingShipmentInputPreviewView(
        long orderLineId,
        String orderNo,
        String itemNo,
        BigDecimal shipmentQty,
        List<OutsourcingShipmentInputPreviewLineView> lines
) {
}
