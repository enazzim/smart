package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import java.time.LocalDate;

public record ProductionPlanListCriteria(
        Long partnerId,
        Long itemId,
        LocalDate requestedDeliveryDateFrom,
        LocalDate requestedDeliveryDateTo,
        ProductionPlanStatus status,
        ProductionPlanMrpStatus mrpStatus
) {
}
