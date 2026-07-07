package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.Size;

public record SubmitWorkDiaryRequest(
        @Size(max = 500) String comment
) {
}

