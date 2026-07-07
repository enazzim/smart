package com.shindong.smartmanager.api.web.workdiary;

public record WorkDiaryErrorResponse(
        String code,
        String message,
        String detail
) {
}

