package com.shindong.smartmanager.infrastructure.persistence.outsource;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;

@Entity
@Table(name = "outsourcing_shipment_line")
public class OutsourcingShipmentLineJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "outsourcing_shipment_id", nullable = false)
    private Long outsourcingShipmentId;

    @Column(name = "line_no", nullable = false)
    private short lineNo;

    @Column(name = "outsourcing_order_line_id")
    private Long outsourcingOrderLineId;

    @Column(name = "parent_item_id")
    private Long parentItemId;

    @Column(name = "begin_process_code_id")
    private Long beginProcessCodeId;

    @Column(name = "end_process_code_id")
    private Long endProcessCodeId;

    @Column(name = "shipment_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal shipmentQty;

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

    protected OutsourcingShipmentLineJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getOutsourcingShipmentId() {
        return outsourcingShipmentId;
    }

    public void setOutsourcingShipmentId(Long outsourcingShipmentId) {
        this.outsourcingShipmentId = outsourcingShipmentId;
    }

    public short getLineNo() {
        return lineNo;
    }

    public void setLineNo(short lineNo) {
        this.lineNo = lineNo;
    }

    public Long getOutsourcingOrderLineId() {
        return outsourcingOrderLineId;
    }

    public void setOutsourcingOrderLineId(Long outsourcingOrderLineId) {
        this.outsourcingOrderLineId = outsourcingOrderLineId;
    }

    public Long getParentItemId() {
        return parentItemId;
    }

    public void setParentItemId(Long parentItemId) {
        this.parentItemId = parentItemId;
    }

    public Long getBeginProcessCodeId() {
        return beginProcessCodeId;
    }

    public void setBeginProcessCodeId(Long beginProcessCodeId) {
        this.beginProcessCodeId = beginProcessCodeId;
    }

    public Long getEndProcessCodeId() {
        return endProcessCodeId;
    }

    public void setEndProcessCodeId(Long endProcessCodeId) {
        this.endProcessCodeId = endProcessCodeId;
    }

    public BigDecimal getShipmentQty() {
        return shipmentQty;
    }

    public void setShipmentQty(BigDecimal shipmentQty) {
        this.shipmentQty = shipmentQty;
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
