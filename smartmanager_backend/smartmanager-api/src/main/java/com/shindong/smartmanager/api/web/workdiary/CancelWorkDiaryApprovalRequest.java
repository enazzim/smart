package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record CancelWorkDiaryApprovalRequest(
        @NotBlank @Size(max = 500) String reason
) {
}

