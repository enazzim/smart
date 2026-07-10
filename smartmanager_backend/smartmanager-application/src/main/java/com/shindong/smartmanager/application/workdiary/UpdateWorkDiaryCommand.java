package com.shindong.smartmanager.application.workdiary;

import java.util.Map;

public record UpdateWorkDiaryCommand(
        long id,
        Map<String, Object> fieldValues,
        boolean listed,
        String closingNote,
        long actorUserId,
        String actorLoginId,
        String actorUserIdText
) {
}

