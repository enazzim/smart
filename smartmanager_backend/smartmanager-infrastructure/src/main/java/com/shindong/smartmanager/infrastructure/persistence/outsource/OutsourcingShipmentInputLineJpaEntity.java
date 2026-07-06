package com.shindong.smartmanager.infrastructure.persistence.outsource;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;

@Entity
@Table(name = "outsourcing_shipment_input_line")
public class OutsourcingShipmentInputLineJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "outsourcing_shipment_line_id", nullable = false)
    private Long outsourcingShipmentLineId;

    @Column(name = "line_no", nullable = false)
    private short lineNo;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "item_composition_id")
    private Long itemCompositionId;

    @Column(name = "issue_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal issueQty;

    @Column(name = "source_location_code", nullable = false, length = 20)
    private String sourceLocationCode;

    @Column(name = "source_process_id")
    private Long sourceProcessId;

    @Column(name = "input_process_id", nullable = false)
    private Long inputProcessId;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    protected OutsourcingShipmentInputLineJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getOutsourcingShipmentLineId() {
        return outsourcingShipmentLineId;
    }

    public void setOutsourcingShipmentLineId(Long outsourcingShipmentLineId) {
        this.outsourcingShipmentLineId = outsourcingShipmentLineId;
    }

    public short getLineNo() {
        return lineNo;
    }

    public void setLineNo(short lineNo) {
        this.lineNo = lineNo;
    }

    public Long getItemId() {
        return itemId;
    }

    public void setItemId(Long itemId) {
        this.itemId = itemId;
    }

    public Long getItemCompositionId() {
        return itemCompositionId;
    }

    public void setItemCompositionId(Long itemCompositionId) {
        this.itemCompositionId = itemCompositionId;
    }

    public BigDecimal getIssueQty() {
        return issueQty;
    }

    public void setIssueQty(BigDecimal issueQty) {
        this.issueQty = issueQty;
    }

    public String getSourceLocationCode() {
        return sourceLocationCode;
    }

    public void setSourceLocationCode(String sourceLocationCode) {
        this.sourceLocationCode = sourceLocationCode;
    }

    public Long getSourceProcessId() {
        return sourceProcessId;
    }

    public void setSourceProcessId(Long sourceProcessId) {
        this.sourceProcessId = sourceProcessId;
    }

    public Long getInputProcessId() {
        return inputProcessId;
    }

    public void setInputProcessId(Long inputProcessId) {
        this.inputProcessId = inputProcessId;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }
}
