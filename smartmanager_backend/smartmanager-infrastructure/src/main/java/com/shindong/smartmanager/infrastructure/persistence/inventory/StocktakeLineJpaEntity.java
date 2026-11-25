package com.shindong.smartmanager.infrastructure.persistence.inventory;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.Instant;

@Entity
@Table(name = "stocktake_line")
public class StocktakeLineJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "batch_id", nullable = false)
    private Long batchId;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "location_code", nullable = false, length = 20)
    private String locationCode;

    @Column(name = "output_process_id")
    private Long outputProcessId;

    @Column(name = "partner_id")
    private Long partnerId;

    @Column(name = "input_process_id")
    private Long inputProcessId;

    @Column(name = "lot_id")
    private Long lotId;

    @Column(name = "book_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal bookQty;

    @Column(name = "counted_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal countedQty;

    @Column(name = "misc_stock_movement_id")
    private Long miscStockMovementId;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    public StocktakeLineJpaEntity() {
    }

    public void setBatchId(Long batchId) {
        this.batchId = batchId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public void setLocationCode(String locationCode) {
        this.locationCode = locationCode;
    }

    public void setOutputProcessId(Long outputProcessId) {
        this.outputProcessId = outputProcessId;
    }

    public void setPartnerId(Long partnerId) {
        this.partnerId = partnerId;
    }

    public void setInputProcessId(Long inputProcessId) {
        this.inputProcessId = inputProcessId;
    }

    public void setLotId(Long lotId) {
        this.lotId = lotId;
    }

    public void setBookQty(BigDecimal bookQty) {
        this.bookQty = bookQty;
    }

    public void setCountedQty(BigDecimal countedQty) {
        this.countedQty = countedQty;
    }

    public void setMiscStockMovementId(Long miscStockMovementId) {
        this.miscStockMovementId = miscStockMovementId;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }
}
