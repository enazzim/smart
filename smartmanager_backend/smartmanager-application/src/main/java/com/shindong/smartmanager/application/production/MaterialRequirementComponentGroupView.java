package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.util.List;

public record MaterialRequirementComponentGroupView(
        long componentItemId,
        String componentItemNo,
        String componentItemName,
        PropertyClassification componentPropertyClassification,
        String unit,
        BigDecimal totalGrossQty,
        int lineCount,
        List<MaterialRequirementLineView> details
) {
}
