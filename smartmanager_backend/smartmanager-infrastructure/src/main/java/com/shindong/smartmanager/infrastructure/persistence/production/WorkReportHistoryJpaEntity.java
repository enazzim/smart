package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.production.WorkReportHistorySourceType;
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
@Table(name = "work_report_history")
public class WorkReportHistoryJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "production_plan_id", nullable = false)
    private Long productionPlanId;

    @Column(name = "process_sequence_id", nullable = false)
    private Long processSequenceId;

    @Column(name = "good_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal goodQty;

    @Column(name = "scrap_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal scrapQty;

    @Column(name = "history_date", nullable = false)
    private LocalDate historyDate;

    @Enumerated(EnumType.STRING)
    @Column(name = "source_type", nullable = false, length = 30)
    private WorkReportHistorySourceType sourceType;

    @Column(name = "source_id", nullable = false)
    private Long sourceId;

    @Column(name = "fiscal_year", nullable = false)
    private short fiscalYear;

    @Column(name = "fiscal_month", nullable = false)
    private byte fiscalMonth;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by_id")
    private Long createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    protected WorkReportHistoryJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public void setProductionPlanId(Long productionPlanId) {
        this.productionPlanId = productionPlanId;
    }

    public void setProcessSequenceId(Long processSequenceId) {
        this.processSequenceId = processSequenceId;
    }

    public void setGoodQty(BigDecimal goodQty) {
        this.goodQty = goodQty;
    }

    public void setScrapQty(BigDecimal scrapQty) {
        this.scrapQty = scrapQty;
    }

    public void setHistoryDate(LocalDate historyDate) {
        this.historyDate = historyDate;
    }

    public void setSourceType(WorkReportHistorySourceType sourceType) {
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
}
