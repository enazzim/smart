package com.shindong.smartmanager.infrastructure.persistence.workstandard;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "work_standard")
public class WorkStandardJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "process_sequence_id", nullable = false)
    private Long processSequenceId;

    @Column(name = "work_center_id", nullable = false)
    private Long workCenterId;

    @Column(name = "equipment_id")
    private Long equipmentId;

    @Column(name = "priority_order", nullable = false)
    private int priorityOrder = 1;

    @Column(name = "main_worker_user_id")
    private Long mainWorkerUserId;

    @Column(name = "tool_name", length = 200)
    private String toolName;

    @Column(name = "setup_time", nullable = false)
    private int setupTime;

    @Column(name = "standard_time", nullable = false)
    private int standardTime;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by", length = 100)
    private String createdBy;

    @Column(name = "created_by_id", length = 100)
    private String createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_by", length = 100)
    private String updatedBy;

    @Column(name = "updated_by_id", length = 100)
    private String updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected WorkStandardJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getItemId() {
        return itemId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public Long getProcessSequenceId() {
        return processSequenceId;
    }

    public void setProcessSequenceId(Long processSequenceId) {
        this.processSequenceId = processSequenceId;
    }

    public Long getWorkCenterId() {
        return workCenterId;
    }

    public void setWorkCenterId(Long workCenterId) {
        this.workCenterId = workCenterId;
    }

    public Long getEquipmentId() {
        return equipmentId;
    }

    public void setEquipmentId(Long equipmentId) {
        this.equipmentId = equipmentId;
    }

    public int getPriorityOrder() {
        return priorityOrder;
    }

    public void setPriorityOrder(int priorityOrder) {
        this.priorityOrder = priorityOrder;
    }

    public Long getMainWorkerUserId() {
        return mainWorkerUserId;
    }

    public void setMainWorkerUserId(Long mainWorkerUserId) {
        this.mainWorkerUserId = mainWorkerUserId;
    }

    public String getToolName() {
        return toolName;
    }

    public void setToolName(String toolName) {
        this.toolName = toolName;
    }

    public int getSetupTime() {
        return setupTime;
    }

    public void setSetupTime(int setupTime) {
        this.setupTime = setupTime;
    }

    public int getStandardTime() {
        return standardTime;
    }

    public void setStandardTime(int standardTime) {
        this.standardTime = standardTime;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }

    public String getCreatedBy() {
        return createdBy;
    }

    public void setCreatedBy(String createdBy) {
        this.createdBy = createdBy;
    }

    public String getCreatedById() {
        return createdById;
    }

    public void setCreatedById(String createdById) {
        this.createdById = createdById;
    }

    public Instant getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }

    public String getUpdatedBy() {
        return updatedBy;
    }

    public void setUpdatedBy(String updatedBy) {
        this.updatedBy = updatedBy;
    }

    public String getUpdatedById() {
        return updatedById;
    }

    public void setUpdatedById(String updatedById) {
        this.updatedById = updatedById;
    }

    public Instant getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Instant updatedAt) {
        this.updatedAt = updatedAt;
    }
}
