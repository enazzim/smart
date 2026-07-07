package com.shindong.smartmanager.api.web.workdiary;

import java.time.LocalDateTime;

public record WorkDiaryApproveResponse(
        Long id,
        WorkDiaryStatus status,
        String directiveNote,
        LocalDateTime approvedAt,
        Long approvedByUserId,
        String approvedByName
) {
}

