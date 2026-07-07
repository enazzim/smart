package com.shindong.smartmanager.application.workdiary;

public record CancelWorkDiaryApprovalCommand(
        long id,
        String reason,
        long actorUserId,
        String actorLoginId,
        String actorUserIdText
) {
}

