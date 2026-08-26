package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.process.ProcessItemInventorySupport;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.LocalDate;

public class SalesShipmentInventoryService {

    private static final String LOCATION_SALES = "SALES";
    private static final String LOCATION_WIP = "WIP";
    private static final String LOCATION_DELIVERY = "DELIVERY";
    private static final String REFERENCE_TYPE = "SALES_SHIPMENT";
    private static final String REFERENCE_TYPE_CANCEL = "SALES_SHIPMENT_CANCEL";

    private final InventoryBalanceService inventoryBalanceService;
    private final ProcessRepository processRepository;
    private final WipBalanceProjector wipBalanceProjector;

    public SalesShipmentInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ProcessRepository processRepository,
            WipBalanceProjector wipBalanceProjector
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.processRepository = processRepository;
        this.wipBalanceProjector = wipBalanceProjector;
    }

    public void assertSufficientSalesStock(
            LocalDate shipmentDate,
            long itemId,
            String itemNo,
            PropertyClassification propertyClassification,
            BigDecimal qty
    ) {
        if (qty == null || qty.compareTo(BigDecimal.ZERO) <= 0) {
            return;
        }
        if (propertyClassification != null && propertyClassification.shipmentFromWipFinalProcess()) {
            long finalProcessId = ProcessItemInventorySupport.requireFinalProcessId(
                    processRepository, itemId, itemNo);
            wipBalanceProjector.ensure(itemId, finalProcessId, "system");
            inventoryBalanceService.assertSufficientStockForOutbound(
                    itemId,
                    itemNo,
                    LOCATION_WIP,
                    shipmentDate,
                    qty,
                    finalProcessId,
                    null
            );
            return;
        }
        inventoryBalanceService.assertSufficientStockForOutbound(
                itemId,
                itemNo,
                LOCATION_SALES,
                shipmentDate,
                qty,
                null,
                null
        );
    }

    public BigDecimal resolveShipmentAvailableQty(
            LocalDate shipmentDate,
            long itemId,
            PropertyClassification propertyClassification
    ) {
        if (propertyClassification != null && propertyClassification.shipmentFromWipFinalProcess()) {
            return resolveWipFinalOnHandQty(shipmentDate, itemId);
        }
        return inventoryBalanceService.currentStockQty(
                itemId, LOCATION_SALES, shipmentDate, null, null, null);
    }

    public BigDecimal resolveWipFinalOnHandQty(LocalDate shipmentDate, long itemId) {
        return ProcessItemInventorySupport.resolveFinalProcessId(processRepository, itemId)
                .map(processId -> inventoryBalanceService.currentStockQty(
                        itemId, LOCATION_WIP, shipmentDate, processId, null, null))
                .orElse(BigDecimal.ZERO);
    }

    public Long resolveFinalProcessId(long itemId, PropertyClassification propertyClassification) {
        if (propertyClassification == null || !propertyClassification.shipmentFromWipFinalProcess()) {
            return null;
        }
        return ProcessItemInventorySupport.resolveFinalProcessId(processRepository, itemId)
                .orElse(null);
    }

    public String resolveSourceLocationCode(PropertyClassification propertyClassification) {
        if (propertyClassification != null && propertyClassification.shipmentFromWipFinalProcess()) {
            return LOCATION_WIP;
        }
        return LOCATION_SALES;
    }

    public void applyRegistration(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            String itemNo,
            PropertyClassification propertyClassification,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        if (propertyClassification != null && propertyClassification.shipmentFromWipFinalProcess()) {
            applyProcessItemRegistration(
                    shipmentDate, shipmentLineId, itemId, itemNo, qty, amount, lotId, actorUserId);
            return;
        }
        inventoryBalanceService.assertSufficientStockForOutbound(
                itemId, itemNo, LOCATION_SALES, shipmentDate, qty, null, null);
        recordSalesOut(shipmentDate, shipmentLineId, itemId, qty, amount, lotId, actorUserId);
        recordDeliveryIn(shipmentDate, shipmentLineId, itemId, qty, amount, lotId, actorUserId);
    }

    public void applyCancellation(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            String itemNo,
            PropertyClassification propertyClassification,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        Long resolvedLotId = resolveCancelLotId(shipmentLineId, lotId);
        if (propertyClassification != null && propertyClassification.shipmentFromWipFinalProcess()) {
            applyProcessItemCancellation(
                    shipmentDate, shipmentLineId, itemId, itemNo, qty, amount, resolvedLotId, actorUserId);
            return;
        }
        recordDeliveryOut(shipmentDate, shipmentLineId, itemId, qty, amount, resolvedLotId, actorUserId);
        recordSalesIn(shipmentDate, shipmentLineId, itemId, qty, amount, resolvedLotId, actorUserId);
    }

    private Long resolveCancelLotId(long shipmentLineId, Long preferredLotId) {
        if (preferredLotId != null) {
            return preferredLotId;
        }
        return inventoryBalanceService.findLotIdByReference(REFERENCE_TYPE, shipmentLineId)
                .orElse(null);
    }

    private void applyProcessItemRegistration(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            String itemNo,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        long finalProcessId = ProcessItemInventorySupport.requireFinalProcessId(
                processRepository, itemId, itemNo);
        wipBalanceProjector.ensure(itemId, finalProcessId, actorUserId);
        inventoryBalanceService.assertSufficientStockForOutbound(
                itemId, itemNo, LOCATION_WIP, shipmentDate, qty, finalProcessId, null);
        recordWipOut(shipmentDate, shipmentLineId, itemId, qty, finalProcessId, lotId, actorUserId);
        recordDeliveryIn(shipmentDate, shipmentLineId, itemId, qty, amount, lotId, actorUserId);
    }

    private void applyProcessItemCancellation(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            String itemNo,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        long finalProcessId = ProcessItemInventorySupport.requireFinalProcessId(
                processRepository, itemId, itemNo);
        wipBalanceProjector.ensure(itemId, finalProcessId, actorUserId);
        recordDeliveryOut(shipmentDate, shipmentLineId, itemId, qty, amount, lotId, actorUserId);
        recordWipIn(shipmentDate, shipmentLineId, itemId, qty, finalProcessId, lotId, actorUserId);
    }

    private void recordWipOut(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            long outputProcessId,
            Long lotId,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                LOCATION_WIP,
                shipmentDate,
                StockMovementType.OUT,
                qty,
                BigDecimal.ZERO,
                REFERENCE_TYPE,
                shipmentLineId,
                outputProcessId,
                null,
                null,
                lotId,
                actorUserId
        ));
    }

    private void recordWipIn(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            long outputProcessId,
            Long lotId,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                LOCATION_WIP,
                shipmentDate,
                StockMovementType.IN,
                qty,
                BigDecimal.ZERO,
                REFERENCE_TYPE_CANCEL,
                shipmentLineId,
                outputProcessId,
                null,
                null,
                lotId,
                actorUserId
        ));
    }

    private void recordSalesOut(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                LOCATION_SALES,
                shipmentDate,
                StockMovementType.OUT,
                qty,
                amount,
                REFERENCE_TYPE,
                shipmentLineId,
                null,
                null,
                null,
                lotId,
                actorUserId
        ));
    }

    private void recordSalesIn(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                LOCATION_SALES,
                shipmentDate,
                StockMovementType.IN,
                qty,
                amount,
                REFERENCE_TYPE_CANCEL,
                shipmentLineId,
                null,
                null,
                null,
                lotId,
                actorUserId
        ));
    }

    private void recordDeliveryIn(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                LOCATION_DELIVERY,
                shipmentDate,
                StockMovementType.IN,
                qty,
                amount,
                REFERENCE_TYPE,
                shipmentLineId,
                null,
                null,
                null,
                lotId,
                actorUserId
        ));
    }

    private void recordDeliveryOut(
            LocalDate shipmentDate,
            long shipmentLineId,
            long itemId,
            BigDecimal qty,
            BigDecimal amount,
            Long lotId,
            String actorUserId
    ) {
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                LOCATION_DELIVERY,
                shipmentDate,
                StockMovementType.OUT,
                qty,
                amount,
                REFERENCE_TYPE_CANCEL,
                shipmentLineId,
                null,
                null,
                null,
                lotId,
                actorUserId
        ));
    }
}
