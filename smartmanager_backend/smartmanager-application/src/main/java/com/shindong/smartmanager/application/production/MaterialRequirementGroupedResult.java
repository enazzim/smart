package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.MrpGroupingMode;
import java.util.List;

public record MaterialRequirementGroupedResult(
        MrpGroupingMode groupingMode,
        List<MaterialRequirementLineView> lines,
        List<MaterialRequirementComponentGroupView> groups
) {
}
