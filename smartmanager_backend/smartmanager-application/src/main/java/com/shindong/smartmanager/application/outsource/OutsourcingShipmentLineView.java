package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;

public record OutsourcingShipmentLineView(
        long id,
        int lineNo,
        long orderLineId,
        String orderNo,
        long partnerId,
        String partnerName,
        String itemNo,
        String itemName,
        String processName,
        BigDecimal shipmentQty,
        List<OutsourcingShipmentInputLineView> inputLines
) {
}
