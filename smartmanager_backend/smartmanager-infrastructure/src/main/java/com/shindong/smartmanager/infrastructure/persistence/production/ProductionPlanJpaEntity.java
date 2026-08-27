package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanSourceType;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

@Entity
@Table(name = "production_plan")
public class ProductionPlanJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "plan_no", nullable = false, length = 30)
    private String planNo;

    @Enumerated(EnumType.STRING)
    @Column(name = "source_type", nullable = false, columnDefinition = "ENUM('SALES_ORDER','MANUAL')")
    private ProductionPlanSourceType sourceType = ProductionPlanSourceType.SALES_ORDER;

    @Column(name = "sales_order_id")
    private Long salesOrderId;

    @Column(name = "sales_order_line_id")
    private Long salesOrderLineId;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "planned_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal plannedQty;

    @Column(name = "produced_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal producedQty = BigDecimal.ZERO;

    @Column(name = "requested_delivery_date")
    private LocalDate requestedDeliveryDate;

    @Enumerated(EnumType.STRING)
    @Column(name = "status", nullable = false, columnDefinition = "ENUM('PLANNED','IN_PROGRESS','COMPLETED','CANCELLED')")
    private ProductionPlanStatus status = ProductionPlanStatus.PLANNED;

    @Enumerated(EnumType.STRING)
    @Column(name = "mrp_status", nullable = false, columnDefinition = "ENUM('NOT_CALCULATED','CALCULATED')")
    private ProductionPlanMrpStatus mrpStatus = ProductionPlanMrpStatus.NOT_CALCULATED;

    @Enumerated(EnumType.STRING)
    @Column(name = "work_plan_status", nullable = false, columnDefinition = "ENUM('NOT_PLANNED','PLANNED')")
    private ProductionPlanWorkPlanStatus workPlanStatus = ProductionPlanWorkPlanStatus.NOT_PLANNED;

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

    public Long getId() {
        return id;
    }

    public String getPlanNo() {
        return planNo;
    }

    public void setPlanNo(String planNo) {
        this.planNo = planNo;
    }

    public ProductionPlanSourceType getSourceType() {
        return sourceType;
    }

    public void setSourceType(ProductionPlanSourceType sourceType) {
        this.sourceType = sourceType;
    }

    public Long getSalesOrderId() {
        return salesOrderId;
    }

    public void setSalesOrderId(Long salesOrderId) {
        this.salesOrderId = salesOrderId;
    }

    public Long getSalesOrderLineId() {
        return salesOrderLineId;
    }

    public void setSalesOrderLineId(Long salesOrderLineId) {
        this.salesOrderLineId = salesOrderLineId;
    }

    public Long getItemId() {
        return itemId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public BigDecimal getPlannedQty() {
        return plannedQty;
    }

    public void setPlannedQty(BigDecimal plannedQty) {
        this.plannedQty = plannedQty;
    }

    public BigDecimal getProducedQty() {
        return producedQty;
    }

    public void setProducedQty(BigDecimal producedQty) {
        this.producedQty = producedQty;
    }

    public LocalDate getRequestedDeliveryDate() {
        return requestedDeliveryDate;
    }

    public void setRequestedDeliveryDate(LocalDate requestedDeliveryDate) {
        this.requestedDeliveryDate = requestedDeliveryDate;
    }

    public ProductionPlanStatus getStatus() {
        return status;
    }

    public void setStatus(ProductionPlanStatus status) {
        this.status = status;
    }

    public ProductionPlanMrpStatus getMrpStatus() {
        return mrpStatus;
    }

    public void setMrpStatus(ProductionPlanMrpStatus mrpStatus) {
        this.mrpStatus = mrpStatus;
    }

    public ProductionPlanWorkPlanStatus getWorkPlanStatus() {
        return workPlanStatus;
    }

    public void setWorkPlanStatus(ProductionPlanWorkPlanStatus workPlanStatus) {
        this.workPlanStatus = workPlanStatus;
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
