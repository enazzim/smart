package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
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
@Table(name = "etc_claim")
public class EtcClaimJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "partner_id", nullable = false)
    private Long partnerId;

    @Column(name = "receipt_date", nullable = false)
    private LocalDate receiptDate;

    @Column(name = "reason", nullable = false, length = 500)
    private String reason;

    @Column(name = "amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal amount;

    @Column(name = "fiscal_year", nullable = false)
    private short fiscalYear;

    @Column(name = "fiscal_month", nullable = false)
    private byte fiscalMonth;

    @Enumerated(EnumType.STRING)
    @Column(name = "recognition", nullable = false)
    private EtcClaimRecognition recognition = EtcClaimRecognition.PENDING;

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

    @Column(name = "updated_by_id")
    private Long updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected EtcClaimJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getPartnerId() {
        return partnerId;
    }

    public void setPartnerId(Long partnerId) {
        this.partnerId = partnerId;
    }

    public LocalDate getReceiptDate() {
        return receiptDate;
    }

    public void setReceiptDate(LocalDate receiptDate) {
        this.receiptDate = receiptDate;
    }

    public String getReason() {
        return reason;
    }

    public void setReason(String reason) {
        this.reason = reason;
    }

    public BigDecimal getAmount() {
        return amount;
    }

    public void setAmount(BigDecimal amount) {
        this.amount = amount;
    }

    public short getFiscalYear() {
        return fiscalYear;
    }

    public void setFiscalYear(short fiscalYear) {
        this.fiscalYear = fiscalYear;
    }

    public byte getFiscalMonth() {
        return fiscalMonth;
    }

    public void setFiscalMonth(byte fiscalMonth) {
        this.fiscalMonth = fiscalMonth;
    }

    public EtcClaimRecognition getRecognition() {
        return recognition;
    }

    public void setRecognition(EtcClaimRecognition recognition) {
        this.recognition = recognition;
    }

    public Instant getApprovedAt() {
        return approvedAt;
    }

    public void setApprovedAt(Instant approvedAt) {
        this.approvedAt = approvedAt;
    }

    public Long getApprovedByUserId() {
        return approvedByUserId;
    }

    public void setApprovedByUserId(Long approvedByUserId) {
        this.approvedByUserId = approvedByUserId;
    }

    public Instant getApprovalCancelledAt() {
        return approvalCancelledAt;
    }

    public void setApprovalCancelledAt(Instant approvalCancelledAt) {
        this.approvalCancelledAt = approvalCancelledAt;
    }

    public Long getApprovalCancelledByUserId() {
        return approvalCancelledByUserId;
    }

    public void setApprovalCancelledByUserId(Long approvalCancelledByUserId) {
        this.approvalCancelledByUserId = approvalCancelledByUserId;
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
