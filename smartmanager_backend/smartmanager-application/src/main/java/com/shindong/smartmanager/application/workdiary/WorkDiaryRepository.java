package com.shindong.smartmanager.application.workdiary;

import java.time.Instant;
import java.time.LocalDate;
import java.util.List;
import java.util.List;
import java.util.Map;
import java.util.Optional;

public interface WorkDiaryRepository {

    record WorkDiaryTemplateRecord(
            long id,
            String templateCode,
            long workDiaryGroupId,
            String templateName,
            Map<String, Object> fieldSchema
    ) {
    }

    record WorkDiaryEntryRecord(
            long id,
            LocalDate workDate,
            long authorUserId,
            long workDiaryGroupId,
            String templateCode,
            String workDateTitle,
            Map<String, String> fieldValues,
            String directiveNote,
            boolean listed,
            String closingNote,
            WorkDiaryStatus status,
            int recordingState,
            Instant submittedAt,
            Instant approvedAt,
            Long approvedByUserId,
            Instant approvalCanceledAt,
            Long approvalCanceledByUserId,
            Instant createdAt,
            Instant updatedAt
    ) {
    }

    Optional<WorkDiaryTemplateRecord> findActiveTemplateByGroupId(long workDiaryGroupId);

    List<WorkDiaryTemplateRecord> findAllActiveTemplates();

    void updateTemplate(
            long workDiaryGroupId,
            String templateName,
            Map<String, Object> fieldSchema,
            String actorLoginId,
            String actorUserIdText
    );

    Optional<WorkDiaryEntryRecord> findActiveEntryById(long id);

    Optional<WorkDiaryEntryRecord> findActiveEntryByAuthorAndDate(long authorUserId, LocalDate workDate);

    List<WorkDiaryEntryRecord> findActiveEntries(WorkDiaryListCriteria criteria);

    long countActiveEntries(WorkDiaryListCriteria criteria);

    long saveEntry(
            LocalDate workDate,
            long authorUserId,
            long workDiaryGroupId,
            String templateCode,
            String workDateTitle,
            Map<String, String> fieldValues,
            boolean listed,
            String closingNote,
            WorkDiaryStatus status,
            String actorLoginId,
            String actorUserIdText
    );

    void updateEntry(
            long id,
            Map<String, String> fieldValues,
            boolean listed,
            String closingNote,
            String actorLoginId,
            String actorUserIdText
    );

    void softDeleteEntry(long id, String actorLoginId, String actorUserIdText);

    void markSubmitted(long id, WorkDiaryStatus status, Instant submittedAt, String actorLoginId, String actorUserIdText);

    void markApproved(
            long id,
            String directiveNote,
            WorkDiaryStatus status,
            Instant approvedAt,
            long approvedByUserId,
            String actorLoginId,
            String actorUserIdText
    );

    void cancelApproval(
            long id,
            WorkDiaryStatus status,
            Instant canceledAt,
            long canceledByUserId,
            String actorLoginId,
            String actorUserIdText
    );
}

