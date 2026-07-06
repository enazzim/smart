package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.production.WorkReportStatus;
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
@Table(name = "work_report")
public class WorkReportJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "report_num", nullable = false, length = 50)
    private String reportNum;

    @Column(name = "work_order_id", nullable = false)
    private Long workOrderId;

    @Column(name = "report_date", nullable = false)
    private LocalDate reportDate;

    @Column(name = "good_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal goodQty;

    @Column(name = "scrap_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal scrapQty;

    @Column(name = "setup_time", precision = 10, scale = 2)
    private BigDecimal setupTime;

    @Column(name = "run_time", precision = 10, scale = 2)
    private BigDecimal runTime;

    @Column(name = "worker_id", length = 50)
    private String workerId;

    @Column(name = "worker_name", length = 100)
    private String workerName;

    @Enumerated(EnumType.STRING)
    @Column(name = "status", nullable = false, length = 20)
    private WorkReportStatus status = WorkReportStatus.REGISTERED;

    @Column(name = "stock_applied", nullable = false, columnDefinition = "TINYINT")
    private int stockApplied = 1;

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

    protected WorkReportJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getReportNum() {
        return reportNum;
    }

    public void setReportNum(String reportNum) {
        this.reportNum = reportNum;
    }

    public Long getWorkOrderId() {
        return workOrderId;
    }

    public void setWorkOrderId(Long workOrderId) {
        this.workOrderId = workOrderId;
    }

    public LocalDate getReportDate() {
        return reportDate;
    }

    public void setReportDate(LocalDate reportDate) {
        this.reportDate = reportDate;
    }

    public BigDecimal getGoodQty() {
        return goodQty;
    }

    public void setGoodQty(BigDecimal goodQty) {
        this.goodQty = goodQty;
    }

    public BigDecimal getScrapQty() {
        return scrapQty;
    }

    public void setScrapQty(BigDecimal scrapQty) {
        this.scrapQty = scrapQty;
    }

    public BigDecimal getSetupTime() {
        return setupTime;
    }

    public void setSetupTime(BigDecimal setupTime) {
        this.setupTime = setupTime;
    }

    public BigDecimal getRunTime() {
        return runTime;
    }

    public void setRunTime(BigDecimal runTime) {
        this.runTime = runTime;
    }

    public String getWorkerId() {
        return workerId;
    }

    public void setWorkerId(String workerId) {
        this.workerId = workerId;
    }

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
    }

    public WorkReportStatus getStatus() {
        return status;
    }

    public void setStatus(WorkReportStatus status) {
        this.status = status;
    }

    public int getStockApplied() {
        return stockApplied;
    }

    public void setStockApplied(int stockApplied) {
        this.stockApplied = stockApplied;
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
