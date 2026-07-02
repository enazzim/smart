package com.shindong.smartmanager.infrastructure.persistence.process;

import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "process_sequence")
public class ProcessSequenceJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "process_sequence", nullable = false)
    private short processSequenceNum;

    @Column(name = "public_code_id", nullable = false)
    private Long publicCodeId;

    @Enumerated(EnumType.STRING)
    @Column(name = "work_distinction", nullable = false, length = 20)
    private WorkDistinction workDistinction;

    @Column(name = "work_center_id")
    private Long workCenterId;

    @Column(name = "outside_order_rate", nullable = false, columnDefinition = "TINYINT")
    private int outsideOrderRate;

    @Column(name = "progress_rate", nullable = false)
    private short progressRate;

    @Enumerated(EnumType.STRING)
    @Column(name = "variant", nullable = false, length = 10)
    private ProcessVariant variant = ProcessVariant.plan;

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

    protected ProcessSequenceJpaEntity() {
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

    public short getProcessSequenceNum() {
        return processSequenceNum;
    }

    public void setProcessSequenceNum(short processSequenceNum) {
        this.processSequenceNum = processSequenceNum;
    }

    public Long getPublicCodeId() {
        return publicCodeId;
    }

    public void setPublicCodeId(Long publicCodeId) {
        this.publicCodeId = publicCodeId;
    }

    public WorkDistinction getWorkDistinction() {
        return workDistinction;
    }

    public void setWorkDistinction(WorkDistinction workDistinction) {
        this.workDistinction = workDistinction;
    }

    public Long getWorkCenterId() {
        return workCenterId;
    }

    public void setWorkCenterId(Long workCenterId) {
        this.workCenterId = workCenterId;
    }

    public int getOutsideOrderRate() {
        return outsideOrderRate;
    }

    public void setOutsideOrderRate(int outsideOrderRate) {
        this.outsideOrderRate = outsideOrderRate;
    }

    public short getProgressRate() {
        return progressRate;
    }

    public void setProgressRate(short progressRate) {
        this.progressRate = progressRate;
    }

    public ProcessVariant getVariant() {
        return variant;
    }

    public void setVariant(ProcessVariant variant) {
        this.variant = variant;
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
