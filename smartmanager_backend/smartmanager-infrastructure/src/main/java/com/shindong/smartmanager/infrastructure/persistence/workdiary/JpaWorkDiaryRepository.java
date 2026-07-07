package com.shindong.smartmanager.infrastructure.persistence.workdiary;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.shindong.smartmanager.application.workdiary.WorkDiaryListCriteria;
import com.shindong.smartmanager.application.workdiary.WorkDiaryRepository;
import com.shindong.smartmanager.application.workdiary.WorkDiaryStatus;
import java.io.IOException;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaWorkDiaryRepository implements WorkDiaryRepository {

    private final SpringDataWorkDiaryTemplateRepository templateRepository;
    private final SpringDataWorkDiaryEntryRepository entryRepository;
    private final ObjectMapper objectMapper;

    public JpaWorkDiaryRepository(
            SpringDataWorkDiaryTemplateRepository templateRepository,
            SpringDataWorkDiaryEntryRepository entryRepository,
            ObjectMapper objectMapper
    ) {
        this.templateRepository = templateRepository;
        this.entryRepository = entryRepository;
        this.objectMapper = objectMapper;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<WorkDiaryTemplateRecord> findActiveTemplateByGroupId(long workDiaryGroupId) {
        return templateRepository.findActiveByGroupId(workDiaryGroupId).map(this::toTemplateRecord);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<WorkDiaryEntryRecord> findActiveEntryById(long id) {
        return entryRepository.findActiveById(id).map(this::toEntryRecord);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<WorkDiaryEntryRecord> findActiveEntryByAuthorAndDate(long authorUserId, LocalDate workDate) {
        return entryRepository.findActiveByAuthorAndDate(authorUserId, workDate).map(this::toEntryRecord);
    }

    @Override
    @Transactional(readOnly = true)
    public List<WorkDiaryEntryRecord> findActiveEntries(WorkDiaryListCriteria criteria) {
        Long authorUserId = criteria.approver() ? criteria.authorUserId() : Long.valueOf(criteria.actorUserId());
        return entryRepository.findActiveEntries(
                        authorUserId,
                        criteria.fromDate(),
                        criteria.toDate(),
                        criteria.status(),
                        PageRequest.of(Math.max(criteria.page(), 0), Math.max(criteria.size(), 1))
                ).stream()
                .map(this::toEntryRecord)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public long countActiveEntries(WorkDiaryListCriteria criteria) {
        Long authorUserId = criteria.approver() ? criteria.authorUserId() : Long.valueOf(criteria.actorUserId());
        return entryRepository.countActiveEntries(
                authorUserId,
                criteria.fromDate(),
                criteria.toDate(),
                criteria.status()
        );
    }

    @Override
    @Transactional
    public long saveEntry(
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
    ) {
        Instant now = Instant.now();
        WorkDiaryEntryJpaEntity entity = new WorkDiaryEntryJpaEntity();
        entity.setWorkDate(workDate);
        entity.setAuthorUserId(authorUserId);
        entity.setWorkDiaryGroupId(workDiaryGroupId);
        entity.setTemplateCode(templateCode);
        entity.setWorkDateTitle(workDateTitle);
        entity.setFieldValues(writeJson(fieldValues));
        entity.setListed(listed);
        entity.setClosingNote(closingNote);
        entity.setStatus(status);
        entity.setRecordingState(1);
        entity.setCreatedBy(actorLoginId);
        entity.setCreatedById(actorUserIdText);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorLoginId);
        entity.setUpdatedById(actorUserIdText);
        entity.setUpdatedAt(now);
        return entryRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void updateEntry(
            long id,
            Map<String, String> fieldValues,
            boolean listed,
            String closingNote,
            String actorLoginId,
            String actorUserIdText
    ) {
        WorkDiaryEntryJpaEntity entity = entryRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + id));
        entity.setFieldValues(writeJson(fieldValues));
        entity.setListed(listed);
        entity.setClosingNote(closingNote);
        entity.setUpdatedBy(actorLoginId);
        entity.setUpdatedById(actorUserIdText);
        entity.setUpdatedAt(Instant.now());
        entryRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDeleteEntry(long id, String actorLoginId, String actorUserIdText) {
        entryRepository.findActiveById(id).ifPresent(entity -> {
            entity.setRecordingState(0);
            entity.setUpdatedBy(actorLoginId);
            entity.setUpdatedById(actorUserIdText);
            entity.setUpdatedAt(Instant.now());
            entryRepository.save(entity);
        });
    }

    @Override
    @Transactional
    public void markSubmitted(long id, WorkDiaryStatus status, Instant submittedAt, String actorLoginId, String actorUserIdText) {
        WorkDiaryEntryJpaEntity entity = entryRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + id));
        entity.setStatus(status);
        entity.setSubmittedAt(submittedAt);
        entity.setUpdatedBy(actorLoginId);
        entity.setUpdatedById(actorUserIdText);
        entity.setUpdatedAt(submittedAt);
        entryRepository.save(entity);
    }

    @Override
    @Transactional
    public void markApproved(
            long id,
            String directiveNote,
            WorkDiaryStatus status,
            Instant approvedAt,
            long approvedByUserId,
            String actorLoginId,
            String actorUserIdText
    ) {
        WorkDiaryEntryJpaEntity entity = entryRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + id));
        entity.setDirectiveNote(directiveNote);
        entity.setStatus(status);
        entity.setApprovedAt(approvedAt);
        entity.setApprovedByUserId(approvedByUserId);
        entity.setUpdatedBy(actorLoginId);
        entity.setUpdatedById(actorUserIdText);
        entity.setUpdatedAt(approvedAt);
        entryRepository.save(entity);
    }

    @Override
    @Transactional
    public void cancelApproval(
            long id,
            WorkDiaryStatus status,
            Instant canceledAt,
            long canceledByUserId,
            String actorLoginId,
            String actorUserIdText
    ) {
        WorkDiaryEntryJpaEntity entity = entryRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + id));
        entity.setStatus(status);
        entity.setApprovalCanceledAt(canceledAt);
        entity.setApprovalCanceledByUserId(canceledByUserId);
        entity.setUpdatedBy(actorLoginId);
        entity.setUpdatedById(actorUserIdText);
        entity.setUpdatedAt(canceledAt);
        entryRepository.save(entity);
    }

    private WorkDiaryTemplateRecord toTemplateRecord(WorkDiaryTemplateJpaEntity entity) {
        return new WorkDiaryTemplateRecord(
                entity.getId(),
                entity.getTemplateCode(),
                entity.getWorkDiaryGroupId(),
                entity.getTemplateName(),
                readObjectJson(entity.getFieldSchema())
        );
    }

    private WorkDiaryEntryRecord toEntryRecord(WorkDiaryEntryJpaEntity entity) {
        return new WorkDiaryEntryRecord(
                entity.getId(),
                entity.getWorkDate(),
                entity.getAuthorUserId(),
                entity.getWorkDiaryGroupId(),
                entity.getTemplateCode(),
                entity.getWorkDateTitle(),
                readStringMapJson(entity.getFieldValues()),
                entity.getDirectiveNote(),
                entity.isListed(),
                entity.getClosingNote(),
                entity.getStatus(),
                entity.getRecordingState(),
                entity.getSubmittedAt(),
                entity.getApprovedAt(),
                entity.getApprovedByUserId(),
                entity.getApprovalCanceledAt(),
                entity.getApprovalCanceledByUserId(),
                entity.getCreatedAt(),
                entity.getUpdatedAt()
        );
    }

    private String writeJson(Object value) {
        try {
            return objectMapper.writeValueAsString(value);
        } catch (IOException ex) {
            throw new IllegalStateException("JSON 직렬화에 실패했습니다.", ex);
        }
    }

    private Map<String, Object> readObjectJson(String json) {
        if (json == null || json.isBlank()) {
            return Map.of();
        }
        try {
            return objectMapper.readValue(json, new TypeReference<>() {});
        } catch (IOException ex) {
            throw new IllegalStateException("JSON 파싱에 실패했습니다.", ex);
        }
    }

    private Map<String, String> readStringMapJson(String json) {
        if (json == null || json.isBlank()) {
            return Map.of();
        }
        try {
            return objectMapper.readValue(json, new TypeReference<>() {});
        } catch (IOException ex) {
            throw new IllegalStateException("JSON 파싱에 실패했습니다.", ex);
        }
    }
}

