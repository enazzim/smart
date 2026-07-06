package com.shindong.smartmanager.infrastructure.persistence.production;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;

@Entity
@Table(name = "material_requirement_line")
public class MaterialRequirementLineJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "mrp_run_id", nullable = false)
    private Long mrpRunId;

    @Column(name = "production_plan_id", nullable = false)
    private Long productionPlanId;

    @Column(name = "parent_item_id", nullable = false)
    private Long parentItemId;

    @Column(name = "component_item_id", nullable = false)
    private Long componentItemId;

    @Column(name = "bom_unit_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal bomUnitQty;

    @Column(name = "planned_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal plannedQty;

    @Column(name = "gross_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal grossQty;

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

    public Long getId() {
        return id;
    }

    public Long getMrpRunId() {
        return mrpRunId;
    }

    public void setMrpRunId(Long mrpRunId) {
        this.mrpRunId = mrpRunId;
    }

    public Long getProductionPlanId() {
        return productionPlanId;
    }

    public void setProductionPlanId(Long productionPlanId) {
        this.productionPlanId = productionPlanId;
    }

    public Long getParentItemId() {
        return parentItemId;
    }

    public void setParentItemId(Long parentItemId) {
        this.parentItemId = parentItemId;
    }

    public Long getComponentItemId() {
        return componentItemId;
    }

    public void setComponentItemId(Long componentItemId) {
        this.componentItemId = componentItemId;
    }

    public BigDecimal getBomUnitQty() {
        return bomUnitQty;
    }

    public void setBomUnitQty(BigDecimal bomUnitQty) {
        this.bomUnitQty = bomUnitQty;
    }

    public BigDecimal getPlannedQty() {
        return plannedQty;
    }

    public void setPlannedQty(BigDecimal plannedQty) {
        this.plannedQty = plannedQty;
    }

    public BigDecimal getGrossQty() {
        return grossQty;
    }

    public void setGrossQty(BigDecimal grossQty) {
        this.grossQty = grossQty;
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
