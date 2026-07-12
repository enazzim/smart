package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.domain.inventory.LotGenealogyLinkType;
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

@Entity
@Table(name = "lot_genealogy")
public class LotGenealogyJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "parent_lot_id", nullable = false)
    private Long parentLotId;

    @Column(name = "child_lot_id", nullable = false)
    private Long childLotId;

    @Enumerated(EnumType.STRING)
    @Column(name = "link_type", nullable = false, length = 20)
    private LotGenealogyLinkType linkType;

    @Column(name = "qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal qty;

    @Column(name = "stock_movement_id")
    private Long stockMovementId;

    @Column(name = "source_doc_type", length = 50)
    private String sourceDocType;

    @Column(name = "source_doc_id")
    private Long sourceDocId;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by", length = 100)
    private String createdBy;

    @Column(name = "created_by_id", length = 100)
    private String createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    public Long getId() {
        return id;
    }

    public Long getParentLotId() {
        return parentLotId;
    }

    public void setParentLotId(Long parentLotId) {
        this.parentLotId = parentLotId;
    }

    public Long getChildLotId() {
        return childLotId;
    }

    public void setChildLotId(Long childLotId) {
        this.childLotId = childLotId;
    }

    public LotGenealogyLinkType getLinkType() {
        return linkType;
    }

    public void setLinkType(LotGenealogyLinkType linkType) {
        this.linkType = linkType;
    }

    public BigDecimal getQty() {
        return qty;
    }

    public void setQty(BigDecimal qty) {
        this.qty = qty;
    }

    public Long getStockMovementId() {
        return stockMovementId;
    }

    public void setStockMovementId(Long stockMovementId) {
        this.stockMovementId = stockMovementId;
    }

    public String getSourceDocType() {
        return sourceDocType;
    }

    public void setSourceDocType(String sourceDocType) {
        this.sourceDocType = sourceDocType;
    }

    public Long getSourceDocId() {
        return sourceDocId;
    }

    public void setSourceDocId(Long sourceDocId) {
        this.sourceDocId = sourceDocId;
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
