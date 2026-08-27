package com.shindong.smartmanager.api.web.drawing;

import jakarta.validation.constraints.NotNull;

public record DrawingLinkItemRequest(
        @NotNull(message = "품목 ID는 필수입니다.") Long itemId
) {
}
