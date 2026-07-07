package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDateTime;

public record WorkDiaryCancelApprovalResult(
        Long id,
        WorkDiaryStatus status,
        LocalDateTime approvalCanceledAt,
        Long approvalCanceledByUserId,
        String approvalCanceledByName
) {
}

