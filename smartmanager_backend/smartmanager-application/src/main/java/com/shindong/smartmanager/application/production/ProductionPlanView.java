package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanSourceType;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record ProductionPlanView(
        long id,
        String planNo,
        ProductionPlanSourceType sourceType,
        Long salesOrderId,
        Long salesOrderLineId,
        String orderNo,
        Long partnerId,
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
