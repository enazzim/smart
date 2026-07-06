package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MaterialRequirementLineView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.Instant;

public record MaterialRequirementLineResponse(
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
        String componentPropertyClassificationLabel,
        String unit,
        BigDecimal bomUnitQty,
        BigDecimal plannedQty,
        BigDecimal grossQty,
        Instant createdAt,
        boolean cancellable
) {
    public static MaterialRequirementLineResponse from(MaterialRequirementLineView view) {
        return new MaterialRequirementLineResponse(
                view.id(),
                view.mrpRunId(),
                view.runNo(),
                view.productionPlanId(),
                view.planNo(),
                view.parentItemId(),
                view.parentItemNo(),
                view.parentItemName(),
                view.componentItemId(),
                view.componentItemNo(),
                view.componentItemName(),
                view.componentPropertyClassification(),
                view.componentPropertyClassification().name(),
                view.unit(),
                view.bomUnitQty(),
                view.plannedQty(),
                view.grossQty(),
                view.createdAt(),
                view.cancellable()
        );
    }
}
