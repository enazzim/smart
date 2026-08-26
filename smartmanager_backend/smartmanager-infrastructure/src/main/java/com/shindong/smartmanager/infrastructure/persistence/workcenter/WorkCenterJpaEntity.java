package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "work_center")
public class WorkCenterJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "wc_name", nullable = false, length = 100)
    private String wcName;

    @Column(name = "main_process_code_id", nullable = false)
    private Long mainProcessCodeId;

    @Column(name = "operation_time", nullable = false)
    private int operationTime = 480;

    @Column(name = "retention_staff", nullable = false)
    private int retentionStaff = 1;

    @Column(name = "capacity_distinction", nullable = false, length = 20)
    private String capacityDistinction = "TIME";

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

    protected WorkCenterJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getWcName() {
        return wcName;
    }

    public void setWcName(String wcName) {
        this.wcName = wcName;
    }

    public Long getMainProcessCodeId() {
        return mainProcessCodeId;
    }

    public void setMainProcessCodeId(Long mainProcessCodeId) {
        this.mainProcessCodeId = mainProcessCodeId;
    }

    public int getOperationTime() {
        return operationTime;
    }

    public void setOperationTime(int operationTime) {
        this.operationTime = operationTime;
    }

    public int getRetentionStaff() {
        return retentionStaff;
    }

    public String getCapacityDistinction() {
        return capacityDistinction;
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
