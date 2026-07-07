package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDate;
import java.time.LocalDateTime;

public record WorkDiaryListItemView(
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

