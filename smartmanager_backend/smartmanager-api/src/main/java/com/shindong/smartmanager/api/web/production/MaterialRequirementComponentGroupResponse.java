package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MaterialRequirementComponentGroupView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.util.List;

public record MaterialRequirementComponentGroupResponse(
        long componentItemId,
        String componentItemNo,
        String componentItemName,
        PropertyClassification componentPropertyClassification,
        String componentPropertyClassificationLabel,
        String unit,
        BigDecimal totalGrossQty,
        int lineCount,
        List<MaterialRequirementLineResponse> details
) {
    public static MaterialRequirementComponentGroupResponse from(MaterialRequirementComponentGroupView view) {
        return new MaterialRequirementComponentGroupResponse(
                view.componentItemId(),
                view.componentItemNo(),
                view.componentItemName(),
                view.componentPropertyClassification(),
                view.componentPropertyClassification().name(),
                view.unit(),
                view.totalGrossQty(),
                view.lineCount(),
                view.details().stream().map(MaterialRequirementLineResponse::from).toList()
        );
    }
}
