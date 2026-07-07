package com.shindong.smartmanager.api.web.workdiary;

import java.time.LocalDateTime;

public record WorkDiarySubmitResponse(
        Long id,
        WorkDiaryStatus status,
        LocalDateTime submittedAt
) {
}

