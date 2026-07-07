package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

public class SalesShipmentInventoryService {

    private static final String REFERENCE_TYPE = "SALES_SHIPMENT";

    private final InventoryBalanceService inventoryBalanceService;

    public SalesShipmentInventoryService(InventoryBalanceService inventoryBalanceService) {
        this.inventoryBalanceService = inventoryBalanceService;
    }

    public void assertSufficientSalesStock(
            LocalDate shipmentDate,
            long itemId,
            String itemNo,
            BigDecimal qty
    ) {
        inventoryBalanceService.assertSufficientStockForOutbound(
                itemId,
                itemNo,
                "SALES",
                shipmentDate,
                qty,
                null,
                null
        );
    }

    public void applyRegistration(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            String itemNo,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        assertSufficientSalesStock(shipmentDate, itemId, itemNo, qty);
        recordSalesOut(shipmentDate, shipmentLineId, itemId, qty, amount, actorUserId);
        recordDeliveryIn(shipmentDate, shipmentLineId, itemId, qty, amount, actorUserId);
    }

    public void applyCancellation(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        recordDeliveryOut(shipmentDate, shipmentLineId, itemId, qty, amount, actorUserId);
        recordSalesIn(shipmentDate, shipmentLineId, itemId, qty, amount, actorUserId);
    }

    private void recordSalesOut(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                "SALES",
                shipmentDate,
                StockMovementType.OUT,
                qty,
                amount,
                REFERENCE_TYPE,
                shipmentLineId,
                null,
                null,
                null,
                actorUserId
        ));
    }

    private void recordSalesIn(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                "SALES",
                shipmentDate,
                StockMovementType.IN,
                qty,
                amount,
                REFERENCE_TYPE + "_CANCEL",
                shipmentLineId,
                null,
                null,
                null,
                actorUserId
        ));
    }

    private void recordDeliveryIn(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                "DELIVERY",
                shipmentDate,
                StockMovementType.IN,
                qty,
                amount,
                REFERENCE_TYPE,
                shipmentLineId,
                null,
                null,
                null,
                actorUserId
        ));
    }

    private void recordDeliveryOut(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                "DELIVERY",
                shipmentDate,
                StockMovementType.OUT,
                qty,
                amount,
                REFERENCE_TYPE + "_CANCEL",
                shipmentLineId,
                null,
                null,
                null,
                actorUserId
        ));
    }
}
