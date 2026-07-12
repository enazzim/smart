package com.shindong.smartmanager.api.web.item;

import jakarta.validation.constraints.NotNull;

public record UpdateLotTrackedRequest(
        @NotNull Boolean lotTracked
) {
}
