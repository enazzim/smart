package com.shindong.smartmanager.infrastructure.persistence.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.FetchType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "drawing_master")
public class DrawingMasterJpaEntity {

    @Id
    @Column(length = 36)
    private String id;

    @Column(name = "part_no", nullable = false, length = 50)
    private String partNo;

    @Column(name = "part_name", nullable = false, length = 100)
    private String partName;

    @Column(name = "model_type", nullable = false, length = 50)
    private String modelType;

    @Enumerated(EnumType.STRING)
    @Column(name = "lifecycle_stage", nullable = false, length = 30)
    private DrawingLifecycleStage lifecycleStage = DrawingLifecycleStage.RECEIVED;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "source_partner_id")
    private CompanyJpaEntity sourcePartner;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "item_id")
    private ItemJpaEntity item;

    @Column(name = "item_linked_at")
    private Instant itemLinkedAt;

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

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getPartNo() {
        return partNo;
    }

    public void setPartNo(String partNo) {
        this.partNo = partNo;
    }

    public String getPartName() {
        return partName;
    }

    public void setPartName(String partName) {
        this.partName = partName;
    }

    public String getModelType() {
        return modelType;
    }

    public void setModelType(String modelType) {
        this.modelType = modelType;
    }

    public DrawingLifecycleStage getLifecycleStage() {
        return lifecycleStage;
    }

    public void setLifecycleStage(DrawingLifecycleStage lifecycleStage) {
        this.lifecycleStage = lifecycleStage;
    }

    public CompanyJpaEntity getSourcePartner() {
        return sourcePartner;
    }

    public void setSourcePartner(CompanyJpaEntity sourcePartner) {
        this.sourcePartner = sourcePartner;
    }

    public ItemJpaEntity getItem() {
        return item;
    }

    public void setItem(ItemJpaEntity item) {
        this.item = item;
    }

    public Instant getItemLinkedAt() {
        return itemLinkedAt;
    }

    public void setItemLinkedAt(Instant itemLinkedAt) {
        this.itemLinkedAt = itemLinkedAt;
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
