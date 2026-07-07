package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import java.time.LocalDate;

public record WorkDiaryListQuery(
        LocalDate fromDate,
        LocalDate toDate,
        WorkDiaryStatus status,
        Long authorUserId,
        @Min(0) Integer page,
        @Min(1) @Max(200) Integer size
) {
}

