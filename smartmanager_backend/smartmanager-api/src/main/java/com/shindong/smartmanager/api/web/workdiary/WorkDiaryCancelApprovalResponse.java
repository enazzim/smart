package com.shindong.smartmanager.api.web.workdiary;

import java.time.LocalDateTime;

public record WorkDiaryCancelApprovalResponse(
        Long id,
        WorkDiaryStatus status,
        LocalDateTime approvalCanceledAt,
        Long approvalCanceledByUserId,
        String approvalCanceledByName
) {
}

