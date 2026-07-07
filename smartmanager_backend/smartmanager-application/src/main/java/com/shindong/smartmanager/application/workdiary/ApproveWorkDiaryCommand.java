package com.shindong.smartmanager.application.workdiary;

public record ApproveWorkDiaryCommand(
        long id,
        String directiveNote,
        long actorUserId,
        String actorLoginId,
        String actorUserIdText
) {
}

