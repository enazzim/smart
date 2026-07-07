package com.shindong.smartmanager.api.web.workdiary;

import java.time.LocalDate;
import java.time.LocalDateTime;

public record WorkDiaryListItemResponse(
        Long id,
        LocalDate workDate,
        String workDateTitle,
        Long authorUserId,
        String authorName,
        Long workDiaryGroupId,
        String workDiaryGroupName,
        String templateCode,
        WorkDiaryStatus status,
        boolean listed,
        LocalDateTime approvedAt,
        String directiveNotePreview
) {
}

