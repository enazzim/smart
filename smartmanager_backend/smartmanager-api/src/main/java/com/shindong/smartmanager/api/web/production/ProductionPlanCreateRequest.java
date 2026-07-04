package com.shindong.smartmanager.api.web.production;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import java.util.List;

@JsonIgnoreProperties(ignoreUnknown = true)
public record ProductionPlanCreateRequest(
        List<ProductionPlanCreateLineRequest> lines,
        List<Long> salesOrderLineIds
) {
}
