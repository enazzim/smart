package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.Size;

public record ApproveWorkDiaryRequest(
        @Size(max = 1000) String directiveNote
) {
}

