package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.Size;

public record CancelWorkDiaryApprovalRequest(
        @Size(max = 500) String reason
) {
}

