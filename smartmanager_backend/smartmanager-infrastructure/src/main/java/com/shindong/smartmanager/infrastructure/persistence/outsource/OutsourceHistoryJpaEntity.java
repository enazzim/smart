package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
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
@Table(name = "outsource_history")
public class OutsourceHistoryJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "company_id", nullable = false)
    private Long companyId;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "outsource_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal outsourceQty;

    @Column(name = "unit_price", nullable = false, precision = 18, scale = 2)
    private BigDecimal unitPrice;

    @Column(name = "amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal amount;

    @Column(name = "history_date", nullable = false)
    private LocalDate historyDate;

    @Enumerated(EnumType.STRING)
    @Column(name = "source_type", nullable = false, length = 30)
    private OutsourceHistorySourceType sourceType;

    @Column(name = "source_id", nullable = false)
    private Long sourceId;

    @Column(name = "fiscal_year", nullable = false)
    private short fiscalYear;

    @Column(name = "fiscal_month", nullable = false)
    private byte fiscalMonth;

    @Enumerated(EnumType.STRING)
    @Column(name = "approval_status", nullable = false, length = 20)
    private PayableApprovalStatus approvalStatus = PayableApprovalStatus.PENDING;

    @Column(name = "approved_at")
    private Instant approvedAt;

    @Column(name = "approved_by_user_id")
    private Long approvedByUserId;

    @Column(name = "approval_cancelled_at")
    private Instant approvalCancelledAt;

    @Column(name = "approval_cancelled_by_user_id")
    private Long approvalCancelledByUserId;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by_id")
    private Long createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    public void setCompanyId(Long companyId) {
        this.companyId = companyId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public void setOutsourceQty(BigDecimal outsourceQty) {
        this.outsourceQty = outsourceQty;
    }

    public void setUnitPrice(BigDecimal unitPrice) {
        this.unitPrice = unitPrice;
    }

    public void setAmount(BigDecimal amount) {
        this.amount = amount;
    }

    public void setHistoryDate(LocalDate historyDate) {
        this.historyDate = historyDate;
    }

    public void setSourceType(OutsourceHistorySourceType sourceType) {
        this.sourceType = sourceType;
    }

    public void setSourceId(Long sourceId) {
        this.sourceId = sourceId;
    }

    public void setFiscalYear(short fiscalYear) {
        this.fiscalYear = fiscalYear;
    }

    public void setFiscalMonth(byte fiscalMonth) {
        this.fiscalMonth = fiscalMonth;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }

    public void setCreatedById(Long createdById) {
        this.createdById = createdById;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }

    public Long getId() {
        return id;
    }

    public OutsourceHistorySourceType getSourceType() {
        return sourceType;
    }

    public Long getSourceId() {
        return sourceId;
    }

    public Long getCompanyId() {
        return companyId;
    }

    public Long getItemId() {
        return itemId;
    }

    public BigDecimal getAmount() {
        return amount;
    }

    public LocalDate getHistoryDate() {
        return historyDate;
    }

    public short getFiscalYear() {
        return fiscalYear;
    }

    public byte getFiscalMonth() {
        return fiscalMonth;
    }

    public PayableApprovalStatus getApprovalStatus() {
        return approvalStatus;
    }

    public void setApprovalStatus(PayableApprovalStatus approvalStatus) {
        this.approvalStatus = approvalStatus;
    }

    public void setApprovedAt(Instant approvedAt) {
        this.approvedAt = approvedAt;
    }

    public void setApprovedByUserId(Long approvedByUserId) {
        this.approvedByUserId = approvedByUserId;
    }

    public void setApprovalCancelledAt(Instant approvalCancelledAt) {
        this.approvalCancelledAt = approvalCancelledAt;
    }

    public void setApprovalCancelledByUserId(Long approvalCancelledByUserId) {
        this.approvalCancelledByUserId = approvalCancelledByUserId;
    }
}
