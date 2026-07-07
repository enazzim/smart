package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record ApproveWorkDiaryRequest(
        @NotBlank @Size(max = 1000) String directiveNote
) {
}

