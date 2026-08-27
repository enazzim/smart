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
@Table(name = "unit_price_change_log")
public class UnitPriceChangeLogJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "unit_price_id", nullable = false)
    private Long unitPriceId;

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
    private BigDecimal orderRate;

    @Column(name = "standard_unit_cost", nullable = false, precision = 18, scale = 4)
    private BigDecimal standardUnitCost;

    @Column(name = "discount_unit_cost", precision = 18, scale = 4)
    private BigDecimal discountUnitCost;

    @Column(name = "begin_date", nullable = false)
    private LocalDate beginDate;

    @Column(name = "end_date")
    private LocalDate endDate;

    @Column(name = "update_reason", nullable = false, length = 500)
    private String updateReason;

    @Column(name = "changed_by_id")
    private Long changedById;

    @Column(name = "changed_at", nullable = false)
    private Instant changedAt;

    protected UnitPriceChangeLogJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getUnitPriceId() {
        return unitPriceId;
    }

    public void setUnitPriceId(Long unitPriceId) {
        this.unitPriceId = unitPriceId;
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

    public String getUpdateReason() {
        return updateReason;
    }

    public void setUpdateReason(String updateReason) {
        this.updateReason = updateReason;
    }

    public Long getChangedById() {
        return changedById;
    }

    public void setChangedById(Long changedById) {
        this.changedById = changedById;
    }

    public Instant getChangedAt() {
        return changedAt;
    }

    public void setChangedAt(Instant changedAt) {
        this.changedAt = changedAt;
    }
}
