package com.shindong.smartmanager.infrastructure.persistence.equipment;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "equipment")
public class EquipmentJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "equipment_num", nullable = false, length = 50)
    private String equipmentNum;

    @Column(name = "equipment_name", nullable = false, length = 200)
    private String equipmentName;

    @Column(name = "equipment_category_id", nullable = false)
    private Long equipmentCategoryId;

    @Column(name = "work_center_id")
    private Long workCenterId;

    @Column(name = "design_shot", nullable = false)
    private int designShot;

    @Column(name = "initial_shot", nullable = false)
    private int initialShot;

    @Column(name = "work_shot", nullable = false)
    private int workShot;

    @Column(name = "accumulated_shot", nullable = false)
    private int accumulatedShot;

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

    protected EquipmentJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getEquipmentNum() {
        return equipmentNum;
    }

    public void setEquipmentNum(String equipmentNum) {
        this.equipmentNum = equipmentNum;
    }

    public String getEquipmentName() {
        return equipmentName;
    }

    public void setEquipmentName(String equipmentName) {
        this.equipmentName = equipmentName;
    }

    public Long getEquipmentCategoryId() {
        return equipmentCategoryId;
    }

    public void setEquipmentCategoryId(Long equipmentCategoryId) {
        this.equipmentCategoryId = equipmentCategoryId;
    }

    public Long getWorkCenterId() {
        return workCenterId;
    }

    public void setWorkCenterId(Long workCenterId) {
        this.workCenterId = workCenterId;
    }

    public int getDesignShot() {
        return designShot;
    }

    public void setDesignShot(int designShot) {
        this.designShot = designShot;
    }

    public int getInitialShot() {
        return initialShot;
    }

    public void setInitialShot(int initialShot) {
        this.initialShot = initialShot;
    }

    public int getWorkShot() {
        return workShot;
    }

    public void setWorkShot(int workShot) {
        this.workShot = workShot;
    }

    public int getAccumulatedShot() {
        return accumulatedShot;
    }

    public void setAccumulatedShot(int accumulatedShot) {
        this.accumulatedShot = accumulatedShot;
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
