package com.shindong.smartmanager.infrastructure.persistence.company;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;

@Entity
@Table(name = "partner_ledger_monthly")
public class PartnerLedgerMonthlyJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "ledger_account_id", nullable = false)
    private Long ledgerAccountId;

    @Column(name = "month_num", nullable = false)
    private byte monthNum;

    @Column(name = "sale_amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal saleAmount = BigDecimal.ZERO;

    @Column(name = "purchase_amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal purchaseAmount = BigDecimal.ZERO;

    @Column(name = "collected_amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal collectedAmount = BigDecimal.ZERO;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected PartnerLedgerMonthlyJpaEntity() {
    }

    public static PartnerLedgerMonthlyJpaEntity createNew(long ledgerAccountId, byte monthNum) {
        Instant now = Instant.now();
        PartnerLedgerMonthlyJpaEntity entity = new PartnerLedgerMonthlyJpaEntity();
        entity.setLedgerAccountId(ledgerAccountId);
        entity.setMonthNum(monthNum);
        entity.setRecordingState(1);
        entity.setCreatedAt(now);
        entity.setUpdatedAt(now);
        return entity;
    }

    public Long getId() {
        return id;
    }

    public Long getLedgerAccountId() {
        return ledgerAccountId;
    }

    public void setLedgerAccountId(Long ledgerAccountId) {
        this.ledgerAccountId = ledgerAccountId;
    }

    public byte getMonthNum() {
        return monthNum;
    }

    public void setMonthNum(byte monthNum) {
        this.monthNum = monthNum;
    }

    public BigDecimal getSaleAmount() {
        return saleAmount;
    }

    public void setSaleAmount(BigDecimal saleAmount) {
        this.saleAmount = saleAmount;
    }

    public BigDecimal getPurchaseAmount() {
        return purchaseAmount;
    }

    public void setPurchaseAmount(BigDecimal purchaseAmount) {
        this.purchaseAmount = purchaseAmount;
    }

    public BigDecimal getCollectedAmount() {
        return collectedAmount;
    }

    public void setCollectedAmount(BigDecimal collectedAmount) {
        this.collectedAmount = collectedAmount;
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
