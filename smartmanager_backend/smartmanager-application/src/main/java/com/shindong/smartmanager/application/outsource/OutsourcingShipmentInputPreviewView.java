package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;

public record OutsourcingShipmentInputPreviewView(
        Long orderLineId,
        String orderNo,
        String itemNo,
        BigDecimal shipmentQty,
        List<OutsourcingShipmentInputPreviewLineView> lines
) {
}
