package com.shindong.smartmanager.api.web.workstandard;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotNull;

public record UpdateWorkStandardRequest(
        @NotNull Long workCenterId,
        Long equipmentId,
        @NotNull @Min(1) Integer priorityOrder,
        Long mainWorkerId,
        String toolName,
        @NotNull @Min(0) Integer setupTime,
        @NotNull @Min(0) Integer standardTime
) {
}
