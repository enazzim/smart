package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDateTime;

public record WorkDiarySubmitResult(
        Long id,
        WorkDiaryStatus status,
        LocalDateTime submittedAt
) {
}

