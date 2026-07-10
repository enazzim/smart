package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record WorkDiaryChecklistItemRequest(
        @NotBlank @Size(max = 50) String id,
        @Size(max = 100) String group,
        @NotBlank @Size(max = 500) String text,
        Integer sortOrder
) {
}
