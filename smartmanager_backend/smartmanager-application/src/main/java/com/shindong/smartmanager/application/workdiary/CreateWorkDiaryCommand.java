package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDate;
import java.util.Map;

public record CreateWorkDiaryCommand(
        LocalDate workDate,
        Map<String, Object> fieldValues,
        boolean listed,
        String closingNote,
        WorkDiaryStatus status,
        long actorUserId,
        String actorLoginId,
        String actorUserIdText
) {
}

