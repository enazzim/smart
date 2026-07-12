package com.shindong.smartmanager.infrastructure.persistence.sales;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;

@Entity
@Table(name = "sales_revenue_line")
public class SalesRevenueLineJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "sales_revenue_id", nullable = false)
    private Long salesRevenueId;

    @Column(name = "line_no", nullable = false, columnDefinition = "SMALLINT")
    private short lineNo;

    @Column(name = "sales_shipment_line_id", nullable = false)
    private Long salesShipmentLineId;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "revenue_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal revenueQty;

    @Column(name = "unit_price", nullable = false, precision = 18, scale = 2)
    private BigDecimal unitPrice;

    @Column(name = "amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal amount;

    @Column(name = "lot_id")
    private Long lotId;

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

    protected SalesRevenueLineJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getSalesRevenueId() {
        return salesRevenueId;
    }

    public void setSalesRevenueId(Long salesRevenueId) {
        this.salesRevenueId = salesRevenueId;
    }

    public short getLineNo() {
        return lineNo;
    }

    public void setLineNo(short lineNo) {
        this.lineNo = lineNo;
    }

    public Long getSalesShipmentLineId() {
        return salesShipmentLineId;
    }

    public void setSalesShipmentLineId(Long salesShipmentLineId) {
        this.salesShipmentLineId = salesShipmentLineId;
    }

    public Long getItemId() {
        return itemId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public BigDecimal getRevenueQty() {
        return revenueQty;
    }

    public void setRevenueQty(BigDecimal revenueQty) {
        this.revenueQty = revenueQty;
    }

    public BigDecimal getUnitPrice() {
        return unitPrice;
    }

    public void setUnitPrice(BigDecimal unitPrice) {
        this.unitPrice = unitPrice;
    }

    public BigDecimal getAmount() {
        return amount;
    }

    public void setAmount(BigDecimal amount) {
        this.amount = amount;
    }

    public Long getLotId() {
        return lotId;
    }

    public void setLotId(Long lotId) {
        this.lotId = lotId;
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
