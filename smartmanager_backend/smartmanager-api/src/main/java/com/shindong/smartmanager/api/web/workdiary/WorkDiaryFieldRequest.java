package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import java.util.List;

public record WorkDiaryFieldRequest(
        @NotBlank @Size(max = 2) String key,
        @NotBlank @Size(max = 200) String label,
        @NotBlank @Size(max = 20) String type,
        List<@NotBlank @Size(max = 50) String> options,
        List<@Valid WorkDiaryChecklistItemRequest> items
) {
}
