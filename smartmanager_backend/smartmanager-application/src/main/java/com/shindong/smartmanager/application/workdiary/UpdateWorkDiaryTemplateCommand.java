package com.shindong.smartmanager.application.workdiary;

import java.util.Map;

public record UpdateWorkDiaryTemplateCommand(
        long workDiaryGroupId,
        String templateName,
        Map<String, String> legacyFields,
        long actorUserId,
        String actorLoginId,
        String actorUserIdText
) {
}
