package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.Size;
import java.util.List;

public record UpdateWorkDiaryTemplateRequest(
        @NotBlank @Size(max = 100) String templateName,
        @NotEmpty List<@Valid WorkDiaryFieldRequest> fields
) {
}
