package com.shindong.smartmanager.api.web.workdiary;

import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;

public record GetWorkDiaryByDateQuery(
        @NotNull LocalDate workDate
) {
}

