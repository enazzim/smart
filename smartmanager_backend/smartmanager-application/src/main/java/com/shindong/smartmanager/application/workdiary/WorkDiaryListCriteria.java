package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDate;

public record WorkDiaryListCriteria(
        long actorUserId,
        boolean approver,
        LocalDate fromDate,
        LocalDate toDate,
        WorkDiaryStatus status,
        Long authorUserId,
        int page,
        int size
) {
}

