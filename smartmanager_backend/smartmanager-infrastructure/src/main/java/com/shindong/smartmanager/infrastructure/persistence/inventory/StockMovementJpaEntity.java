package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.domain.inventory.StockMovementType;
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
@Table(name = "stock_movement")
public class StockMovementJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "inventory_balance_id", nullable = false)
    private Long inventoryBalanceId;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "location_id", nullable = false)
    private Long locationId;

    @Column(name = "fiscal_year", nullable = false)
    private short fiscalYear;

    @Column(name = "fiscal_month", nullable = false)
    private byte fiscalMonth;

    @Enumerated(EnumType.STRING)
    @Column(name = "movement_type", nullable = false, length = 10)
    private StockMovementType movementType;

    @Column(name = "qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal qty;

    @Column(name = "amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal amount;

    @Column(name = "reference_type", nullable = false, length = 50)
    private String referenceType;

    @Column(name = "reference_id", nullable = false)
    private Long referenceId;

    @Column(name = "movement_date", nullable = false)
    private LocalDate movementDate;

    @Column(name = "output_process_id")
    private Long outputProcessId;

    @Column(name = "input_process_id")
    private Long inputProcessId;

    @Column(name = "partner_id")
    private Long partnerId;

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

    protected StockMovementJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getInventoryBalanceId() {
        return inventoryBalanceId;
    }

    public void setInventoryBalanceId(Long inventoryBalanceId) {
        this.inventoryBalanceId = inventoryBalanceId;
    }

    public Long getItemId() {
        return itemId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public Long getLocationId() {
        return locationId;
    }

    public void setLocationId(Long locationId) {
        this.locationId = locationId;
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

    public StockMovementType getMovementType() {
        return movementType;
    }

    public void setMovementType(StockMovementType movementType) {
        this.movementType = movementType;
    }

    public BigDecimal getQty() {
        return qty;
    }

    public void setQty(BigDecimal qty) {
        this.qty = qty;
    }

    public BigDecimal getAmount() {
        return amount;
    }

    public void setAmount(BigDecimal amount) {
        this.amount = amount;
    }

    public String getReferenceType() {
        return referenceType;
    }

    public void setReferenceType(String referenceType) {
        this.referenceType = referenceType;
    }

    public Long getReferenceId() {
        return referenceId;
    }

    public void setReferenceId(Long referenceId) {
        this.referenceId = referenceId;
    }

    public LocalDate getMovementDate() {
        return movementDate;
    }

    public void setMovementDate(LocalDate movementDate) {
        this.movementDate = movementDate;
    }

    public Long getOutputProcessId() {
        return outputProcessId;
    }

    public void setOutputProcessId(Long outputProcessId) {
        this.outputProcessId = outputProcessId;
    }

    public Long getInputProcessId() {
        return inputProcessId;
    }

    public void setInputProcessId(Long inputProcessId) {
        this.inputProcessId = inputProcessId;
    }

    public Long getPartnerId() {
        return partnerId;
    }

    public void setPartnerId(Long partnerId) {
        this.partnerId = partnerId;
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
}
