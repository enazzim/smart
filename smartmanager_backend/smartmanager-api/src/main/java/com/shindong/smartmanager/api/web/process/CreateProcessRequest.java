package com.shindong.smartmanager.api.web.process;

import com.shindong.smartmanager.domain.process.WorkDistinction;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotNull;

public record CreateProcessRequest(
        @NotNull Long itemId,
        @NotNull @Min(1) @Max(98) Short processSequenceNum,
        @NotNull Long processCodeId,
        @NotNull WorkDistinction workDistinction,
        Long workCenterId,
        @Min(0) @Max(99) Integer outsideOrderRate,
        @NotNull @Min(0) @Max(100) Short progressRate
) {
}
