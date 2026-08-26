package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingOrderSourceType;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
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
@Table(name = "outsourcing_order")
public class OutsourcingOrderJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "order_no", nullable = false, length = 30)
    private String orderNo;

    @Column(name = "partner_id", nullable = false)
    private Long partnerId;

    @Column(name = "order_date", nullable = false)
    private LocalDate orderDate;

    @Enumerated(EnumType.STRING)
    @Column(name = "source_type", nullable = false, columnDefinition = "ENUM('WORK_PLAN','MANUAL')")
    private OutsourcingOrderSourceType sourceType = OutsourcingOrderSourceType.MANUAL;

    @Enumerated(EnumType.STRING)
    @Column(name = "status", nullable = false, columnDefinition = "ENUM('CONFIRMED','IN_PROGRESS','RECEIVED','CANCELLED')")
    private OutsourcingOrderStatus status = OutsourcingOrderStatus.CONFIRMED;

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

    protected OutsourcingOrderJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getOrderNo() {
        return orderNo;
    }

    public void setOrderNo(String orderNo) {
        this.orderNo = orderNo;
    }

    public Long getPartnerId() {
        return partnerId;
    }

    public void setPartnerId(Long partnerId) {
        this.partnerId = partnerId;
    }

    public LocalDate getOrderDate() {
        return orderDate;
    }

    public void setOrderDate(LocalDate orderDate) {
        this.orderDate = orderDate;
    }

    public OutsourcingOrderSourceType getSourceType() {
        return sourceType;
    }

    public void setSourceType(OutsourcingOrderSourceType sourceType) {
        this.sourceType = sourceType;
    }

    public OutsourcingOrderStatus getStatus() {
        return status;
    }

    public void setStatus(OutsourcingOrderStatus status) {
        this.status = status;
    }

    public int getRecordingState() {
        return recordingState;
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
