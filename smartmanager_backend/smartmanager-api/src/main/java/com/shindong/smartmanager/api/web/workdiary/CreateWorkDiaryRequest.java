package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import java.time.LocalDate;
import java.util.Map;

public record CreateWorkDiaryRequest(
        @NotNull LocalDate workDate,
        @NotNull Map<String, Object> fieldValues,
        @NotNull Boolean listed,
        @Size(max = 500) String closingNote,
        @NotNull WorkDiaryStatus status
) {
}

