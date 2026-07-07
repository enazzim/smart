package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

public class SalesRevenueInventoryService {

    private static final String REFERENCE_TYPE = "SALES_REVENUE";

    private final InventoryBalanceService inventoryBalanceService;

    public SalesRevenueInventoryService(InventoryBalanceService inventoryBalanceService) {
        this.inventoryBalanceService = inventoryBalanceService;
    }

    public void assertSufficientDeliveryStock(
            LocalDate revenueDate,
            long itemId,
            String itemNo,
            BigDecimal qty
    ) {
        inventoryBalanceService.assertSufficientStockForOutbound(
                itemId,
                itemNo,
                "DELIVERY",
                revenueDate,
                qty,
                null,
                null
        );
    }

    public void applyRegistration(
            LocalDate revenueDate,
            long revenueLineId,
            long itemId,
            String itemNo,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        assertSufficientDeliveryStock(revenueDate, itemId, itemNo, qty);
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                "DELIVERY",
                revenueDate,
                StockMovementType.OUT,
                qty,
                amount,
                REFERENCE_TYPE,
                revenueLineId,
                null,
                null,
                null,
                actorUserId
        ));
    }

    public void applyCancellation(
            LocalDate revenueDate,
            long revenueLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                "DELIVERY",
                revenueDate,
                StockMovementType.IN,
                qty,
                amount,
                REFERENCE_TYPE + "_CANCEL",
                revenueLineId,
                null,
                null,
                null,
                actorUserId
        ));
    }
}
