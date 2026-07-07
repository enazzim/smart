package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.Size;
import java.util.Map;

public record UpdateWorkDiaryTemplateRequest(
        @NotBlank @Size(max = 100) String templateName,
        @NotEmpty Map<String, @NotBlank @Size(max = 200) String> legacyFields
) {
}
