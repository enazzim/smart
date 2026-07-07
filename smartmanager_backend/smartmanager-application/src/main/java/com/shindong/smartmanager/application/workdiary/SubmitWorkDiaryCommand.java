package com.shindong.smartmanager.application.workdiary;

public record SubmitWorkDiaryCommand(
        long id,
        String comment,
        long actorUserId,
        String actorLoginId,
        String actorUserIdText
) {
}

