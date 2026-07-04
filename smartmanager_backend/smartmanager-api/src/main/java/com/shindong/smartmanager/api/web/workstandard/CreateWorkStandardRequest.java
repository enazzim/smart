package com.shindong.smartmanager.api.web.workstandard;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;

public record CreateWorkStandardRequest(
        @NotBlank String itemNum,
        @NotNull Long processSequenceId,
        @NotNull Long workCenterId,
        Long equipmentId,
        @NotNull @Min(1) Integer priorityOrder,
        Long mainWorkerId,
        String toolName,
        @NotNull @Min(0) Integer setupTime,
        @NotNull @Min(0) Integer standardTime
) {
}
