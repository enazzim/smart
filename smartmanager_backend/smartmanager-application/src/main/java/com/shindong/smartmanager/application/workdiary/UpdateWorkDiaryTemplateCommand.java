package com.shindong.smartmanager.application.workdiary;

import java.util.List;

public record UpdateWorkDiaryTemplateCommand(
        long workDiaryGroupId,
        String templateName,
        List<WorkDiaryFieldDefinition> fields,
        long actorUserId,
        String actorLoginId,
        String actorUserIdText
) {
}
