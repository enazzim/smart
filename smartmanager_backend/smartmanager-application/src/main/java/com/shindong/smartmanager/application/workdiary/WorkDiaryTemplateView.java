package com.shindong.smartmanager.application.workdiary;

import java.util.Map;

public record WorkDiaryTemplateView(
        String templateCode,
        Long workDiaryGroupId,
        String workDiaryGroupName,
        String templateName,
        Map<String, Object> fieldSchema
) {
}

