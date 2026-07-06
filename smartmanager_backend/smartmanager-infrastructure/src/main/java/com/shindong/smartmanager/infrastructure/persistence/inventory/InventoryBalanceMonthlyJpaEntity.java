package com.shindong.smartmanager.infrastructure.persistence.inventory;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;

@Entity
@Table(name = "inventory_balance_monthly")
public class InventoryBalanceMonthlyJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "inventory_balance_id", nullable = false)
    private Long inventoryBalanceId;

    @Column(name = "month_num", nullable = false)
    private byte monthNum;

    @Column(name = "in_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal inQty = BigDecimal.ZERO;

    @Column(name = "in_amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal inAmount = BigDecimal.ZERO;

    @Column(name = "out_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal outQty = BigDecimal.ZERO;

    @Column(name = "out_amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal outAmount = BigDecimal.ZERO;

    @Column(name = "stock_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal stockQty = BigDecimal.ZERO;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected InventoryBalanceMonthlyJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getInventoryBalanceId() {
        return inventoryBalanceId;
    }

    public void setInventoryBalanceId(Long inventoryBalanceId) {
        this.inventoryBalanceId = inventoryBalanceId;
    }

    public byte getMonthNum() {
        return monthNum;
    }

    public void setMonthNum(byte monthNum) {
        this.monthNum = monthNum;
    }

    public BigDecimal getInQty() {
        return inQty;
    }

    public void setInQty(BigDecimal inQty) {
        this.inQty = inQty;
    }

    public BigDecimal getInAmount() {
        return inAmount;
    }

    public void setInAmount(BigDecimal inAmount) {
        this.inAmount = inAmount;
    }

    public BigDecimal getOutQty() {
        return outQty;
    }

    public void setOutQty(BigDecimal outQty) {
        this.outQty = outQty;
    }

    public BigDecimal getOutAmount() {
        return outAmount;
    }

    public void setOutAmount(BigDecimal outAmount) {
        this.outAmount = outAmount;
    }

    public BigDecimal getStockQty() {
        return stockQty;
    }

    public void setStockQty(BigDecimal stockQty) {
        this.stockQty = stockQty;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }

    public Instant getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }

    public Instant getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Instant updatedAt) {
        this.updatedAt = updatedAt;
    }
}
