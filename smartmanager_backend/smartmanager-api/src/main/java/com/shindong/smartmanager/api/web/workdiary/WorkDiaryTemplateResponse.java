package com.shindong.smartmanager.api.web.workdiary;

import java.util.Map;

public record WorkDiaryTemplateResponse(
        String templateCode,
        Long workDiaryGroupId,
        String workDiaryGroupName,
        String templateName,
        Map<String, Object> fieldSchema
) {
}

