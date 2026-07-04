package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record ProductionPlanView(
        long id,
        String planNo,
        long salesOrderId,
        long salesOrderLineId,
        String orderNo,
        long partnerId,
        String partnerName,
        LocalDate orderDate,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal plannedQty,
        BigDecimal producedQty,
        LocalDate requestedDeliveryDate,
        ProductionPlanStatus status,
        ProductionPlanMrpStatus mrpStatus,
        ProductionPlanWorkPlanStatus workPlanStatus
) {
}
