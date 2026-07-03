package com.shindong.smartmanager.infrastructure.persistence.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
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
@Table(name = "unit_price")
public class UnitPriceJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Enumerated(EnumType.STRING)
    @Column(name = "cost_type", nullable = false, columnDefinition = "ENUM('SALE','PURCHASE','OUTSOURCE')")
    private CostType costType;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "company_id", nullable = false)
    private Long companyId;

    @Column(name = "begin_process_code_id")
    private Long beginProcessCodeId;

    @Column(name = "end_process_code_id")
    private Long endProcessCodeId;

    @Column(name = "order_rate", nullable = false, precision = 5, scale = 2)
    private BigDecimal orderRate = BigDecimal.ZERO;

    @Column(name = "standard_unit_cost", nullable = false, precision = 18, scale = 4)
    private BigDecimal standardUnitCost;

    @Column(name = "discount_unit_cost", precision = 18, scale = 4)
    private BigDecimal discountUnitCost;

    @Column(name = "begin_date", nullable = false)
    private LocalDate beginDate;

    @Column(name = "end_date")
    private LocalDate endDate;

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

    protected UnitPriceJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public CostType getCostType() {
        return costType;
    }

    public void setCostType(CostType costType) {
        this.costType = costType;
    }

    public Long getItemId() {
        return itemId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public Long getCompanyId() {
        return companyId;
    }

    public void setCompanyId(Long companyId) {
        this.companyId = companyId;
    }

    public Long getBeginProcessCodeId() {
        return beginProcessCodeId;
    }

    public void setBeginProcessCodeId(Long beginProcessCodeId) {
        this.beginProcessCodeId = beginProcessCodeId;
    }

    public Long getEndProcessCodeId() {
        return endProcessCodeId;
    }

    public void setEndProcessCodeId(Long endProcessCodeId) {
        this.endProcessCodeId = endProcessCodeId;
    }

    public BigDecimal getOrderRate() {
        return orderRate;
    }

    public void setOrderRate(BigDecimal orderRate) {
        this.orderRate = orderRate;
    }

    public BigDecimal getStandardUnitCost() {
        return standardUnitCost;
    }

    public void setStandardUnitCost(BigDecimal standardUnitCost) {
        this.standardUnitCost = standardUnitCost;
    }

    public BigDecimal getDiscountUnitCost() {
        return discountUnitCost;
    }

    public void setDiscountUnitCost(BigDecimal discountUnitCost) {
        this.discountUnitCost = discountUnitCost;
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
