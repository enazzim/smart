package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.ProductionPlanSourceType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record ProductionPlanSaveCommand(
        ProductionPlanSourceType sourceType,
        Long salesOrderId,
        Long salesOrderLineId,
        long itemId,
        BigDecimal plannedQty,
        LocalDate requestedDeliveryDate
) {
}
