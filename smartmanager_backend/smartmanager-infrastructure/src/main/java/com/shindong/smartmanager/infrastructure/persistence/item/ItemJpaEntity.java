package com.shindong.smartmanager.infrastructure.persistence.item;

import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
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

@Entity
@Table(name = "item")
public class ItemJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "item_no", nullable = false, length = 50)
    private String itemNo;

    @Column(name = "item_name", nullable = false, length = 200)
    private String itemName;

    @Enumerated(EnumType.STRING)
    @Column(name = "property_classification", nullable = false, length = 20)
    private PropertyClassification propertyClassification;

    @Column(name = "model_type", length = 100)
    private String modelType;

    @Column(name = "unit", nullable = false, length = 20)
    private String unit;

    @Column(name = "standard", length = 200)
    private String standard;

    @Column(name = "standard_unit_cost")
    private BigDecimal standardUnitCost;

    @Enumerated(EnumType.STRING)
    @Column(name = "check_distinction", length = 20)
    private CheckDistinction checkDistinction;

    @Column(name = "lead_time")
    private Integer leadTime;

    @Column(name = "safety_stock_quantity")
    private BigDecimal safetyStockQuantity;

    @Column(name = "order_interval_quantity")
    private BigDecimal orderIntervalQuantity;

    @Column(name = "min_order_quantity")
    private BigDecimal minOrderQuantity;

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

    protected ItemJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getItemNo() {
        return itemNo;
    }

    public void setItemNo(String itemNo) {
        this.itemNo = itemNo;
    }

    public String getItemName() {
        return itemName;
    }

    public void setItemName(String itemName) {
        this.itemName = itemName;
    }

    public PropertyClassification getPropertyClassification() {
        return propertyClassification;
    }

    public void setPropertyClassification(PropertyClassification propertyClassification) {
        this.propertyClassification = propertyClassification;
    }

    public String getModelType() {
        return modelType;
    }

    public void setModelType(String modelType) {
        this.modelType = modelType;
    }

    public String getUnit() {
        return unit;
    }

    public void setUnit(String unit) {
        this.unit = unit;
    }

    public String getStandard() {
        return standard;
    }

    public void setStandard(String standard) {
        this.standard = standard;
    }

    public BigDecimal getStandardUnitCost() {
        return standardUnitCost;
    }

    public void setStandardUnitCost(BigDecimal standardUnitCost) {
        this.standardUnitCost = standardUnitCost;
    }

    public CheckDistinction getCheckDistinction() {
        return checkDistinction;
    }

    public void setCheckDistinction(CheckDistinction checkDistinction) {
        this.checkDistinction = checkDistinction;
    }

    public Integer getLeadTime() {
        return leadTime;
    }

    public void setLeadTime(Integer leadTime) {
        this.leadTime = leadTime;
    }

    public BigDecimal getSafetyStockQuantity() {
        return safetyStockQuantity;
    }

    public void setSafetyStockQuantity(BigDecimal safetyStockQuantity) {
        this.safetyStockQuantity = safetyStockQuantity;
    }

    public BigDecimal getOrderIntervalQuantity() {
        return orderIntervalQuantity;
    }

    public void setOrderIntervalQuantity(BigDecimal orderIntervalQuantity) {
        this.orderIntervalQuantity = orderIntervalQuantity;
    }

    public BigDecimal getMinOrderQuantity() {
        return minOrderQuantity;
    }

    public void setMinOrderQuantity(BigDecimal minOrderQuantity) {
        this.minOrderQuantity = minOrderQuantity;
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
