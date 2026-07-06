package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.Instant;

public record MaterialRequirementLineView(
        long id,
        long mrpRunId,
        String runNo,
        long productionPlanId,
        String planNo,
        long parentItemId,
        String parentItemNo,
        String parentItemName,
        long componentItemId,
        String componentItemNo,
        String componentItemName,
        PropertyClassification componentPropertyClassification,
        String unit,
        BigDecimal bomUnitQty,
        BigDecimal plannedQty,
        BigDecimal grossQty,
        Instant createdAt,
        boolean cancellable
) {
}
