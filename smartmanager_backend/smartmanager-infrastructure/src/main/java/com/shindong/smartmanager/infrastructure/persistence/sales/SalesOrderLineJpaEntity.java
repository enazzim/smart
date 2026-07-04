package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.math.BigDecimal;
import java.time.LocalDate;

@Entity
@Table(name = "sales_order_line")
public class SalesOrderLineJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "sales_order_id", nullable = false)
    private Long salesOrderId;

    @Column(name = "line_no", nullable = false, columnDefinition = "SMALLINT")
    private short lineNo;

    @Column(name = "item_id", nullable = false)
    private Long itemId;

    @Column(name = "order_qty", nullable = false, precision = 18, scale = 4)
    private BigDecimal orderQty;

    @Column(name = "unit_price", nullable = false, precision = 18, scale = 2)
    private BigDecimal unitPrice;

    @Column(name = "amount", nullable = false, precision = 18, scale = 2)
    private BigDecimal amount;

    @Column(name = "delivery_date")
    private LocalDate deliveryDate;

    @Enumerated(EnumType.STRING)
    @Column(name = "fulfillment_route", nullable = false, columnDefinition = "ENUM('COMMODITY','MANUFACTURING')")
    private SalesFulfillmentRoute fulfillmentRoute;

    @Enumerated(EnumType.STRING)
    @Column(name = "fulfillment_status", nullable = false, columnDefinition = "ENUM('WAITING','IN_PROGRESS','COMPLETED','FORCE_COMPLETED')")
    private SalesLineFulfillmentStatus fulfillmentStatus = SalesLineFulfillmentStatus.WAITING;

    @Enumerated(EnumType.STRING)
    @Column(name = "delivery_status", nullable = false, columnDefinition = "ENUM('NOT_STARTED','IN_PROGRESS','COMPLETED')")
    private SalesLineDeliveryStatus deliveryStatus = SalesLineDeliveryStatus.NOT_STARTED;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    protected SalesOrderLineJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getSalesOrderId() {
        return salesOrderId;
    }

    public void setSalesOrderId(Long salesOrderId) {
        this.salesOrderId = salesOrderId;
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

    public BigDecimal getOrderQty() {
        return orderQty;
    }

    public void setOrderQty(BigDecimal orderQty) {
        this.orderQty = orderQty;
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

    public LocalDate getDeliveryDate() {
        return deliveryDate;
    }

    public void setDeliveryDate(LocalDate deliveryDate) {
        this.deliveryDate = deliveryDate;
    }

    public SalesFulfillmentRoute getFulfillmentRoute() {
        return fulfillmentRoute;
    }

    public void setFulfillmentRoute(SalesFulfillmentRoute fulfillmentRoute) {
        this.fulfillmentRoute = fulfillmentRoute;
    }

    public SalesLineFulfillmentStatus getFulfillmentStatus() {
        return fulfillmentStatus;
    }

    public void setFulfillmentStatus(SalesLineFulfillmentStatus fulfillmentStatus) {
        this.fulfillmentStatus = fulfillmentStatus;
    }

    public SalesLineDeliveryStatus getDeliveryStatus() {
        return deliveryStatus;
    }

    public void setDeliveryStatus(SalesLineDeliveryStatus deliveryStatus) {
        this.deliveryStatus = deliveryStatus;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }
}
