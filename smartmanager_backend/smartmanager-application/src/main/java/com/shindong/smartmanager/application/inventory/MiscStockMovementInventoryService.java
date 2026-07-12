package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

public class MiscStockMovementInventoryService {

    private static final String REFERENCE_TYPE = "MISC_STOCK_MOVEMENT";
    private static final String REFERENCE_TYPE_CANCEL = "MISC_STOCK_MOVEMENT_CANCEL";
    private static final String LOCATION_WIP = "WIP";

    private final InventoryBalanceService inventoryBalanceService;
    private final WipBalanceProjector wipBalanceProjector;

    public MiscStockMovementInventoryService(
            InventoryBalanceService inventoryBalanceService,
            WipBalanceProjector wipBalanceProjector
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.wipBalanceProjector = wipBalanceProjector;
    }

    public BigDecimal resolveOnHandQty(
            long itemId,
            String itemNo,
            String locationCode,
            LocalDate movementDate,
            Long outputProcessId
    ) {
        return inventoryBalanceService.currentStockQty(
                itemId,
                locationCode,
                movementDate,
                outputProcessId,
                null,
                null
        );
    }

    public void ensureInventorySlot(
            long itemId,
            String locationCode,
            LocalDate movementDate,
            Long outputProcessId,
            String actorUserId
    ) {
        if (LOCATION_WIP.equals(locationCode) && outputProcessId != null && actorUserId != null) {
            wipBalanceProjector.ensure(itemId, outputProcessId, actorUserId);
        }
        inventoryBalanceService.currentStockQty(
                itemId,
                locationCode,
                movementDate,
                outputProcessId,
                null,
                null
        );
    }

    public void applyRegistration(
            MiscStockMovementView movement,
            String itemNo,
            String actorUserId
    ) {
        apply(movement, itemNo, actorUserId, false);
    }

    public void applyCancellation(
            MiscStockMovementView movement,
            String itemNo,
            String actorUserId
    ) {
        apply(movement, itemNo, actorUserId, true);
    }

    private void apply(
            MiscStockMovementView movement,
            String itemNo,
            String actorUserId,
            boolean reverse
    ) {
        ensureInventorySlot(
                movement.itemId(),
                movement.locationCode(),
                movement.movementDate(),
                movement.outputProcessId(),
                actorUserId
        );

        StockMovementType stockType = toStockMovementType(movement.movementDirection(), reverse);
        if (stockType == StockMovementType.OUT) {
            inventoryBalanceService.assertSufficientStockForOutbound(
                    movement.itemId(),
                    itemNo,
                    movement.locationCode(),
                    movement.movementDate(),
                    movement.qty(),
                    movement.outputProcessId(),
                    null
            );
        }

        Long lotId = movement.lotId();
        if (reverse && lotId == null) {
            lotId = inventoryBalanceService.findLotIdByReference(REFERENCE_TYPE, movement.id())
                    .orElse(null);
        }

        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                movement.itemId(),
                movement.locationCode(),
                movement.movementDate(),
                stockType,
                movement.qty(),
                BigDecimal.ZERO,
                reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE,
                movement.id(),
                movement.outputProcessId(),
                null,
                null,
                lotId,
                actorUserId
        ));
    }

    private static StockMovementType toStockMovementType(
            MiscStockMovementDirection direction,
            boolean reverse
    ) {
        StockMovementType base = direction == MiscStockMovementDirection.IN
                ? StockMovementType.IN
                : StockMovementType.OUT;
        if (!reverse) {
            return base;
        }
        return base == StockMovementType.IN ? StockMovementType.OUT : StockMovementType.IN;
    }
}
