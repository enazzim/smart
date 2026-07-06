package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MaterialRequirementGroupedResult;
import com.shindong.smartmanager.domain.production.MrpGroupingMode;
import java.util.List;

public record MaterialRequirementGroupedResponse(
        MrpGroupingMode groupingMode,
        String groupingModeLabel,
        List<MaterialRequirementLineResponse> lines,
        List<MaterialRequirementComponentGroupResponse> groups
) {
    public static MaterialRequirementGroupedResponse from(MaterialRequirementGroupedResult result) {
        return new MaterialRequirementGroupedResponse(
                result.groupingMode(),
                result.groupingMode().label(),
                result.lines().stream().map(MaterialRequirementLineResponse::from).toList(),
                result.groups().stream().map(MaterialRequirementComponentGroupResponse::from).toList()
        );
    }
}
