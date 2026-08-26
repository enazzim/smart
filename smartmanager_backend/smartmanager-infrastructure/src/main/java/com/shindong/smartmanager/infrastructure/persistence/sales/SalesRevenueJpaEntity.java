package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;
import java.time.LocalDate;

@Entity
@Table(name = "sales_revenue")
public class SalesRevenueJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "revenue_no", nullable = false, length = 30)
    private String revenueNo;

    @Column(name = "partner_id", nullable = false)
    private Long partnerId;

    @Column(name = "revenue_date", nullable = false)
    private LocalDate revenueDate;

    @Column(name = "sales_shipment_id")
    private Long salesShipmentId;

    @Enumerated(EnumType.STRING)
    @Column(name = "status", nullable = false)
    private SalesRevenueStatus status = SalesRevenueStatus.ISSUED;

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

    protected SalesRevenueJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getRevenueNo() {
        return revenueNo;
    }

    public void setRevenueNo(String revenueNo) {
        this.revenueNo = revenueNo;
    }

    public Long getPartnerId() {
        return partnerId;
    }

    public void setPartnerId(Long partnerId) {
        this.partnerId = partnerId;
    }

    public LocalDate getRevenueDate() {
        return revenueDate;
    }

    public void setRevenueDate(LocalDate revenueDate) {
        this.revenueDate = revenueDate;
    }

    public Long getSalesShipmentId() {
        return salesShipmentId;
    }

    public void setSalesShipmentId(Long salesShipmentId) {
        this.salesShipmentId = salesShipmentId;
    }

    public SalesRevenueStatus getStatus() {
        return status;
    }

    public void setStatus(SalesRevenueStatus status) {
        this.status = status;
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
