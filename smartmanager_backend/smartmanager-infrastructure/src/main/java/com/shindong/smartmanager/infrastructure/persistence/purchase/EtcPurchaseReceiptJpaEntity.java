package com.shindong.smartmanager.infrastructure.persistence.purchase;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

@Entity
@Table(name = "etc_purchase_receipt")
public class EtcPurchaseReceiptJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "receipt_no", nullable = false, length = 30)
    private String receiptNo;

    @Column(name = "etc_purchase_order_id", nullable = false)
    private Long etcPurchaseOrderId;

    @Column(name = "partner_id", nullable = false)
    private Long partnerId;

    @Column(name = "item_name", nullable = false, length = 200)
    private String itemName;

    @Column(name = "receipt_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal receiptQty;

    @Column(name = "unit_price", nullable = false, precision = 18, scale = 2)
    private BigDecimal unitPrice;

    @Column(name = "amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal amount;

    @Column(name = "receipt_date", nullable = false)
    private LocalDate receiptDate;

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

    @Column(name = "updated_by_id")
    private Long updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected EtcPurchaseReceiptJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public void setReceiptNo(String receiptNo) {
        this.receiptNo = receiptNo;
    }

    public void setEtcPurchaseOrderId(Long etcPurchaseOrderId) {
        this.etcPurchaseOrderId = etcPurchaseOrderId;
    }

    public void setPartnerId(Long partnerId) {
        this.partnerId = partnerId;
    }

    public void setItemName(String itemName) {
        this.itemName = itemName;
    }

    public void setReceiptQty(BigDecimal receiptQty) {
        this.receiptQty = receiptQty;
    }

    public void setUnitPrice(BigDecimal unitPrice) {
        this.unitPrice = unitPrice;
    }

    public void setAmount(BigDecimal amount) {
        this.amount = amount;
    }

    public void setReceiptDate(LocalDate receiptDate) {
        this.receiptDate = receiptDate;
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

    public void setUpdatedById(Long updatedById) {
        this.updatedById = updatedById;
    }

    public void setUpdatedAt(Instant updatedAt) {
        this.updatedAt = updatedAt;
    }

    public Long getEtcPurchaseOrderId() {
        return etcPurchaseOrderId;
    }

    public BigDecimal getReceiptQty() {
        return receiptQty;
    }

    public BigDecimal getUnitPrice() {
        return unitPrice;
    }

    public BigDecimal getAmount() {
        return amount;
    }

    public LocalDate getReceiptDate() {
        return receiptDate;
    }

    public short getFiscalYear() {
        return fiscalYear;
    }

    public byte getFiscalMonth() {
        return fiscalMonth;
    }

    public Long getPartnerId() {
        return partnerId;
    }

    public String getItemName() {
        return itemName;
    }
}
