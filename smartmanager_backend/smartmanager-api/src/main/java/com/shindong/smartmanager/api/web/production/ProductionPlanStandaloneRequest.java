package com.shindong.smartmanager.api.web.production;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import java.util.List;

public record ProductionPlanStandaloneRequest(
        @NotEmpty @Valid List<ProductionPlanStandaloneLineRequest> lines
) {
}
