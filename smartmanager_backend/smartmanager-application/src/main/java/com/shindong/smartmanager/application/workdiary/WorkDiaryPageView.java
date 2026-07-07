package com.shindong.smartmanager.application.workdiary;

import java.util.List;

public record WorkDiaryPageView(
        List<WorkDiaryListItemView> items,
        long totalElements,
        int page,
        int size
) {
}

