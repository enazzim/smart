package com.shindong.smartmanager.infrastructure.persistence.bom;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;

@Entity
@Table(name = "bom_change_log")
public class BomChangeLogJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "item_composition_id", nullable = false)
    private Long itemCompositionId;

    @Column(name = "parent_item_id", nullable = false)
    private Long parentItemId;

    @Column(name = "child_item_id", nullable = false)
    private Long childItemId;

    @Column(name = "need_quantity_denominator", nullable = false, precision = 18, scale = 4)
    private BigDecimal needQuantityDenominator;

    @Column(name = "need_quantity_numerator", nullable = false, precision = 18, scale = 4)
    private BigDecimal needQuantityNumerator;

    @Column(name = "change_reason", nullable = false, length = 50)
    private String changeReason;

    @Column(name = "changed_by")
    private String changedBy;

    @Column(name = "changed_by_id")
    private String changedById;

    @Column(name = "changed_at", nullable = false)
    private Instant changedAt;

    public Long getId() {
        return id;
    }

    public Long getItemCompositionId() {
        return itemCompositionId;
    }

    public void setItemCompositionId(Long itemCompositionId) {
        this.itemCompositionId = itemCompositionId;
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

    public String getChangeReason() {
        return changeReason;
    }

    public void setChangeReason(String changeReason) {
        this.changeReason = changeReason;
    }

    public String getChangedBy() {
        return changedBy;
    }

    public void setChangedBy(String changedBy) {
        this.changedBy = changedBy;
    }

    public String getChangedById() {
        return changedById;
    }

    public void setChangedById(String changedById) {
        this.changedById = changedById;
    }

    public Instant getChangedAt() {
        return changedAt;
    }

    public void setChangedAt(Instant changedAt) {
        this.changedAt = changedAt;
    }
}
