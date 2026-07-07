package com.shindong.smartmanager.api.web.workdiary;

import java.util.List;

public record WorkDiaryPageResponse(
        List<WorkDiaryListItemResponse> items,
        long totalElements,
        int page,
        int size
) {
}

