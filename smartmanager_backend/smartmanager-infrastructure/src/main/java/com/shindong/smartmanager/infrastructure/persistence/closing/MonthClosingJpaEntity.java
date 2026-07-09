package com.shindong.smartmanager.infrastructure.persistence.closing;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "month_closing")
public class MonthClosingJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "fiscal_year", nullable = false, columnDefinition = "SMALLINT")
    private int fiscalYear;

    @Column(name = "fiscal_month", nullable = false, columnDefinition = "TINYINT")
    private int fiscalMonth;

    @Column(name = "closed_at", nullable = false)
    private Instant closedAt;

    @Column(name = "closed_by", nullable = false, length = 100)
    private String closedBy;

    @Column(name = "closed_by_id", length = 100)
    private String closedById;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    protected MonthClosingJpaEntity() {
    }

    public static MonthClosingJpaEntity create(
            int fiscalYear,
            int fiscalMonth,
            String closedBy,
            String closedById,
            Instant closedAt
    ) {
        MonthClosingJpaEntity entity = new MonthClosingJpaEntity();
        entity.fiscalYear = fiscalYear;
        entity.fiscalMonth = fiscalMonth;
        entity.closedBy = closedBy;
        entity.closedById = closedById;
        entity.closedAt = closedAt;
        entity.recordingState = 1;
        return entity;
    }

    public Long getId() {
        return id;
    }

    public int getFiscalYear() {
        return fiscalYear;
    }

    public int getFiscalMonth() {
        return fiscalMonth;
    }

    public Instant getClosedAt() {
        return closedAt;
    }

    public String getClosedBy() {
        return closedBy;
    }

    public String getClosedById() {
        return closedById;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void deactivate() {
        this.recordingState = 0;
    }

    public void reactivate(String closedBy, String closedById, Instant closedAt) {
        this.recordingState = 1;
        this.closedBy = closedBy;
        this.closedById = closedById;
        this.closedAt = closedAt;
    }
}
