package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.Map;

public record WorkDiaryDetailView(
        Long id,
        LocalDate workDate,
        String workDateTitle,
        Long authorUserId,
        String authorName,
        Long workDiaryGroupId,
        String workDiaryGroupName,
        String templateCode,
        String templateName,
        Map<String, Object> fieldSchema,
        Map<String, Object> fieldValues,
        String directiveNote,
        WorkDiaryStatus status,
        boolean listed,
        String closingNote,
        LocalDateTime submittedAt,
        LocalDateTime approvedAt,
        Long approvedByUserId,
        String approvedByName,
        LocalDateTime approvalCanceledAt,
        Long approvalCanceledByUserId,
        String approvalCanceledByName,
        boolean canEdit,
        boolean canDelete,
        boolean canApprove,
        boolean canCancelApproval,
        LocalDateTime createdAt,
        LocalDateTime updatedAt
) {
}

