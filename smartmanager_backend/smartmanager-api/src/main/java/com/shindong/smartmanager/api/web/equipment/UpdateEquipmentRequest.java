package com.shindong.smartmanager.api.web.equipment;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;

public record UpdateEquipmentRequest(
        @NotBlank String equipmentName,
        @NotNull Long equipmentCategoryId,
        Long workCenterId,
        @NotNull @Min(0) Integer designShot,
        @NotNull @Min(0) Integer initialShot
) {
}
