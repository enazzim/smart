package com.shindong.smartmanager.api.web.bom;

import jakarta.validation.constraints.NotEmpty;
import java.util.List;

public record LotTrackedEnableRequest(
        @NotEmpty List<Long> itemIds
) {
}
