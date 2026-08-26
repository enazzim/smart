package com.shindong.smartmanager.infrastructure.persistence.workdiary;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "work_diary_template")
public class WorkDiaryTemplateJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "template_code", nullable = false, length = 2, columnDefinition = "CHAR(2)")
    private String templateCode;

    @Column(name = "work_diary_group_id", nullable = false)
    private Long workDiaryGroupId;

    @Column(name = "template_name", nullable = false, length = 100)
    private String templateName;

    @Column(name = "field_schema", nullable = false, columnDefinition = "json")
    private String fieldSchema;

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

    protected WorkDiaryTemplateJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getTemplateCode() {
        return templateCode;
    }

    public void setTemplateCode(String templateCode) {
        this.templateCode = templateCode;
    }

    public Long getWorkDiaryGroupId() {
        return workDiaryGroupId;
    }

    public void setWorkDiaryGroupId(Long workDiaryGroupId) {
        this.workDiaryGroupId = workDiaryGroupId;
    }

    public String getTemplateName() {
        return templateName;
    }

    public void setTemplateName(String templateName) {
        this.templateName = templateName;
    }

    public String getFieldSchema() {
        return fieldSchema;
    }

    public void setFieldSchema(String fieldSchema) {
        this.fieldSchema = fieldSchema;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }

    public Instant getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }

    public Long getCreatedById() {
        return createdById;
    }

    public void setCreatedById(Long createdById) {
        this.createdById = createdById;
    }

    public Instant getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Instant updatedAt) {
        this.updatedAt = updatedAt;
    }

    public Long getUpdatedById() {
        return updatedById;
    }

    public void setUpdatedById(Long updatedById) {
        this.updatedById = updatedById;
    }
}

