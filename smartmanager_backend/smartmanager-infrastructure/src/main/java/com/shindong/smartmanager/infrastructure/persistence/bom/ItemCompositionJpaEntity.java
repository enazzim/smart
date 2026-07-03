package com.shindong.smartmanager.infrastructure.persistence.bom;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

@Entity
@Table(name = "item_composition")
public class ItemCompositionJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "parent_item_id", nullable = false)
    private Long parentItemId;

    @Column(name = "child_item_id", nullable = false)
    private Long childItemId;

    @Column(name = "need_quantity_denominator", nullable = false, precision = 18, scale = 4)
    private BigDecimal needQuantityDenominator;

    @Column(name = "need_quantity_numerator", nullable = false, precision = 18, scale = 4)
    private BigDecimal needQuantityNumerator;

    @Column(name = "process_management", nullable = false, columnDefinition = "TINYINT")
    private int processManagement;

    @Column(name = "sub_division")
    private String subDivision;

    @Column(name = "supply_division")
    private String supplyDivision;

    @Column(name = "bom_unit")
    private String bomUnit;

    @Column(name = "begin_date", nullable = false)
    private LocalDate beginDate;

    @Column(name = "end_date")
    private LocalDate endDate;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState;

    @Column(name = "created_by")
    private String createdBy;

    @Column(name = "created_by_id")
    private String createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_by")
    private String updatedBy;

    @Column(name = "updated_by_id")
    private String updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    public Long getId() {
        return id;
    }

    public Long getParentItemId() {
        return parentItemId;
    }

    public void setParentItemId(Long parentItemId) {
        this.parentItemId = parentItemId;
    }

    public Long getChildItemId() {
        return childItemId;
    }

    public void setChildItemId(Long childItemId) {
        this.childItemId = childItemId;
    }

    public BigDecimal getNeedQuantityDenominator() {
        return needQuantityDenominator;
    }

    public void setNeedQuantityDenominator(BigDecimal needQuantityDenominator) {
        this.needQuantityDenominator = needQuantityDenominator;
    }

    public BigDecimal getNeedQuantityNumerator() {
        return needQuantityNumerator;
    }

    public void setNeedQuantityNumerator(BigDecimal needQuantityNumerator) {
        this.needQuantityNumerator = needQuantityNumerator;
    }

    public int getProcessManagement() {
        return processManagement;
    }

    public void setProcessManagement(int processManagement) {
        this.processManagement = processManagement;
    }

    public String getSubDivision() {
        return subDivision;
    }

    public void setSubDivision(String subDivision) {
        this.subDivision = subDivision;
    }

    public String getSupplyDivision() {
        return supplyDivision;
    }

    public void setSupplyDivision(String supplyDivision) {
        this.supplyDivision = supplyDivision;
    }

    public String getBomUnit() {
        return bomUnit;
    }

    public void setBomUnit(String bomUnit) {
        this.bomUnit = bomUnit;
    }

    public LocalDate getBeginDate() {
        return beginDate;
    }

    public void setBeginDate(LocalDate beginDate) {
        this.beginDate = beginDate;
    }

    public LocalDate getEndDate() {
        return endDate;
    }

    public void setEndDate(LocalDate endDate) {
        this.endDate = endDate;
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
