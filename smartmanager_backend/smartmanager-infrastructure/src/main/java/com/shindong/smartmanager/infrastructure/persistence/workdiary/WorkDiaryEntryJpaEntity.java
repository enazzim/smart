package com.shindong.smartmanager.infrastructure.persistence.workdiary;

import com.shindong.smartmanager.application.workdiary.WorkDiaryStatus;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;
import java.time.LocalDate;

@Entity
@Table(name = "work_diary_entry")
public class WorkDiaryEntryJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "work_date", nullable = false)
    private LocalDate workDate;

    @Column(name = "author_user_id", nullable = false)
    private Long authorUserId;

    @Column(name = "work_diary_group_id", nullable = false)
    private Long workDiaryGroupId;

    @Column(name = "template_code", nullable = false, length = 2, columnDefinition = "CHAR(2)")
    private String templateCode;

    @Column(name = "work_date_title", length = 200)
    private String workDateTitle;

    @Column(name = "field_values", nullable = false, columnDefinition = "json")
    private String fieldValues;

    @Column(name = "directive_note", length = 1000)
    private String directiveNote;

    @Column(name = "listed", nullable = false, columnDefinition = "TINYINT")
    private int listed = 1;

    @Column(name = "closing_note", length = 500)
    private String closingNote;

    @Enumerated(EnumType.STRING)
    @Column(name = "status", nullable = false, columnDefinition = "ENUM('DRAFT','SUBMITTED','APPROVED','REJECTED')")
    private WorkDiaryStatus status = WorkDiaryStatus.DRAFT;

    @Column(name = "submitted_at")
    private Instant submittedAt;

    @Column(name = "approved_at")
    private Instant approvedAt;

    @Column(name = "approved_by_user_id")
    private Long approvedByUserId;

    @Column(name = "approval_canceled_at")
    private Instant approvalCanceledAt;

    @Column(name = "approval_canceled_by_user_id")
    private Long approvalCanceledByUserId;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by_id")
    private Long createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_by_id")
    private Long updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected WorkDiaryEntryJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public LocalDate getWorkDate() {
        return workDate;
    }

    public void setWorkDate(LocalDate workDate) {
        this.workDate = workDate;
    }

    public Long getAuthorUserId() {
        return authorUserId;
    }

    public void setAuthorUserId(Long authorUserId) {
        this.authorUserId = authorUserId;
    }

    public Long getWorkDiaryGroupId() {
        return workDiaryGroupId;
    }

    public void setWorkDiaryGroupId(Long workDiaryGroupId) {
        this.workDiaryGroupId = workDiaryGroupId;
    }

    public String getTemplateCode() {
        return templateCode;
    }

    public void setTemplateCode(String templateCode) {
        this.templateCode = templateCode;
    }

    public String getWorkDateTitle() {
        return workDateTitle;
    }

    public void setWorkDateTitle(String workDateTitle) {
        this.workDateTitle = workDateTitle;
    }

    public String getFieldValues() {
        return fieldValues;
    }

    public void setFieldValues(String fieldValues) {
        this.fieldValues = fieldValues;
    }

    public String getDirectiveNote() {
        return directiveNote;
    }

    public void setDirectiveNote(String directiveNote) {
        this.directiveNote = directiveNote;
    }

    public boolean isListed() {
        return listed == 1;
    }

    public void setListed(boolean listed) {
        this.listed = listed ? 1 : 0;
    }

    public String getClosingNote() {
        return closingNote;
    }

    public void setClosingNote(String closingNote) {
        this.closingNote = closingNote;
    }

    public WorkDiaryStatus getStatus() {
        return status;
    }

    public void setStatus(WorkDiaryStatus status) {
        this.status = status;
    }

    public Instant getSubmittedAt() {
        return submittedAt;
    }

    public void setSubmittedAt(Instant submittedAt) {
        this.submittedAt = submittedAt;
    }

    public Instant getApprovedAt() {
        return approvedAt;
    }

    public void setApprovedAt(Instant approvedAt) {
        this.approvedAt = approvedAt;
    }

    public Long getApprovedByUserId() {
        return approvedByUserId;
    }

    public void setApprovedByUserId(Long approvedByUserId) {
        this.approvedByUserId = approvedByUserId;
    }

    public Instant getApprovalCanceledAt() {
        return approvalCanceledAt;
    }

    public void setApprovalCanceledAt(Instant approvalCanceledAt) {
        this.approvalCanceledAt = approvalCanceledAt;
    }

    public Long getApprovalCanceledByUserId() {
        return approvalCanceledByUserId;
    }

    public void setApprovalCanceledByUserId(Long approvalCanceledByUserId) {
        this.approvalCanceledByUserId = approvalCanceledByUserId;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }

    public Long getCreatedById() {
        return createdById;
    }

    public void setCreatedById(Long createdById) {
        this.createdById = createdById;
    }

    public Instant getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }

    public Long getUpdatedById() {
        return updatedById;
    }

    public void setUpdatedById(Long updatedById) {
        this.updatedById = updatedById;
    }

    public Instant getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Instant updatedAt) {
        this.updatedAt = updatedAt;
    }
}

