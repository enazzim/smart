package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDateTime;

public record WorkDiaryApproveResult(
        Long id,
        WorkDiaryStatus status,
        String directiveNote,
        LocalDateTime approvedAt,
        Long approvedByUserId,
        String approvedByName
) {
}

