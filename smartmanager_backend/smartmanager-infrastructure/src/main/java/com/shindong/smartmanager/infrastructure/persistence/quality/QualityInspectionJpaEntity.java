package com.shindong.smartmanager.infrastructure.persistence.quality;

import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
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
@Table(name = "quality_inspection")
public class QualityInspectionJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "inspection_no", length = 30)
    private String inspectionNo;

    @Enumerated(EnumType.STRING)
    @Column(name = "source_type", nullable = false, length = 20)
    private QualityInspectionSourceType sourceType;

    @Column(name = "source_receipt_line_id", nullable = false)
    private Long sourceReceiptLineId;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "company_id", nullable = false)
    private Long companyId;

    @Column(name = "request_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal requestQty;

    @Column(name = "passed_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal passedQty = BigDecimal.ZERO;

    @Column(name = "failed_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal failedQty = BigDecimal.ZERO;

    @Enumerated(EnumType.STRING)
    @Column(name = "status", nullable = false, length = 20)
    private QualityInspectionStatus status;

    @Column(name = "inspection_decision_code_id")
    private Long inspectionDecisionCodeId;

    @Column(name = "unsuitability_cause_code_id")
    private Long unsuitabilityCauseCodeId;

    @Column(name = "unsuitability_status_code_id")
    private Long unsuitabilityStatusCodeId;

    @Column(name = "completed_at")
    private Instant completedAt;

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

    protected QualityInspectionJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public void setInspectionNo(String inspectionNo) {
        this.inspectionNo = inspectionNo;
    }

    public String getInspectionNo() {
        return inspectionNo;
    }

    public QualityInspectionSourceType getSourceType() {
        return sourceType;
    }

    public void setSourceType(QualityInspectionSourceType sourceType) {
        this.sourceType = sourceType;
    }

    public Long getSourceReceiptLineId() {
        return sourceReceiptLineId;
    }

    public void setSourceReceiptLineId(Long sourceReceiptLineId) {
        this.sourceReceiptLineId = sourceReceiptLineId;
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

    public BigDecimal getRequestQty() {
        return requestQty;
    }

    public void setRequestQty(BigDecimal requestQty) {
        this.requestQty = requestQty;
    }

    public BigDecimal getPassedQty() {
        return passedQty;
    }

    public void setPassedQty(BigDecimal passedQty) {
        this.passedQty = passedQty;
    }

    public BigDecimal getFailedQty() {
        return failedQty;
    }

    public void setFailedQty(BigDecimal failedQty) {
        this.failedQty = failedQty;
    }

    public QualityInspectionStatus getStatus() {
        return status;
    }

    public void setStatus(QualityInspectionStatus status) {
        this.status = status;
    }

    public Long getInspectionDecisionCodeId() {
        return inspectionDecisionCodeId;
    }

    public void setInspectionDecisionCodeId(Long inspectionDecisionCodeId) {
        this.inspectionDecisionCodeId = inspectionDecisionCodeId;
    }

    public Long getUnsuitabilityCauseCodeId() {
        return unsuitabilityCauseCodeId;
    }

    public void setUnsuitabilityCauseCodeId(Long unsuitabilityCauseCodeId) {
        this.unsuitabilityCauseCodeId = unsuitabilityCauseCodeId;
    }

    public Long getUnsuitabilityStatusCodeId() {
        return unsuitabilityStatusCodeId;
    }

    public void setUnsuitabilityStatusCodeId(Long unsuitabilityStatusCodeId) {
        this.unsuitabilityStatusCodeId = unsuitabilityStatusCodeId;
    }

    public Instant getCompletedAt() {
        return completedAt;
    }

    public void setCompletedAt(Instant completedAt) {
        this.completedAt = completedAt;
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
