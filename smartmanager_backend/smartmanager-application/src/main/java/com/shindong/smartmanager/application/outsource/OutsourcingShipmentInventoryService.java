package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.process.InventoryBalanceRepository;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.Year;
import java.util.List;

public class OutsourcingShipmentInventoryService {

    private static final String REFERENCE_TYPE = "OUTSOURCING_SHIPMENT";
    private static final String REFERENCE_TYPE_CANCEL = "OUTSOURCING_SHIPMENT_CANCEL";
    private static final String LOCATION_OUTSOURCE = "OUTSOURCE";

    private final InventoryBalanceService inventoryBalanceService;
    private final InventoryBalanceRepository inventoryBalanceRepository;
    private final WipBalanceProjector wipBalanceProjector;

    public OutsourcingShipmentInventoryService(
            InventoryBalanceService inventoryBalanceService,
            InventoryBalanceRepository inventoryBalanceRepository,
            WipBalanceProjector wipBalanceProjector
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.inventoryBalanceRepository = inventoryBalanceRepository;
        this.wipBalanceProjector = wipBalanceProjector;
    }

    public void assertSufficientStock(
            LocalDate shipmentDate,
            long partnerId,
            List<OutsourcingShipmentInputSaveCommand> inputLines
    ) {
        for (OutsourcingShipmentInputSaveCommand line : inputLines) {
            if (line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            inventoryBalanceService.assertSufficientStockForOutbound(
                    line.itemId(),
                    String.valueOf(line.itemId()),
                    line.sourceLocationCode(),
                    shipmentDate,
                    line.issueQty(),
                    line.sourceProcessId(),
                    null
            );
        }
    }

    public void applyRegistration(
            LocalDate shipmentDate,
            long shipmentId,
            long partnerId,
            List<OutsourcingShipmentInputSaveCommand> inputLines,
            String actorUserId
    ) {
        apply(shipmentDate, shipmentId, partnerId, inputLines, actorUserId, false);
    }

    public void applyCancellation(
            LocalDate shipmentDate,
            long shipmentId,
            long partnerId,
            List<OutsourcingShipmentInputSaveCommand> inputLines,
            String actorUserId
    ) {
        apply(shipmentDate, shipmentId, partnerId, inputLines, actorUserId, true);
    }

    private void apply(
            LocalDate shipmentDate,
            long shipmentId,
            long partnerId,
            List<OutsourcingShipmentInputSaveCommand> inputLines,
            String actorUserId,
            boolean reverse
    ) {
        int fiscalYear = Year.now().getValue();
        StockMovementType outType = reverse ? StockMovementType.IN : StockMovementType.OUT;
        StockMovementType inType = reverse ? StockMovementType.OUT : StockMovementType.IN;
        String referenceType = reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE;

        for (OutsourcingShipmentInputSaveCommand line : inputLines) {
            if (line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            if ("WIP".equals(line.sourceLocationCode()) && line.sourceProcessId() != null) {
                wipBalanceProjector.ensure(line.itemId(), line.sourceProcessId(), actorUserId);
            }
            inventoryBalanceRepository.ensureOutsourceInputBalance(
                    line.itemId(),
                    fiscalYear,
                    partnerId,
                    line.inputProcessId(),
                    actorUserId
            );

            Long lotId = line.lotId();
            if (reverse && lotId == null) {
                lotId = inventoryBalanceService.findLotIdByReference(REFERENCE_TYPE, shipmentId)
                        .orElse(null);
            }

            inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                    line.itemId(),
                    line.sourceLocationCode(),
                    shipmentDate,
                    outType,
                    line.issueQty(),
                    BigDecimal.ZERO,
                    referenceType,
                    shipmentId,
                    line.sourceProcessId(),
                    null,
                    null,
                    lotId,
                    actorUserId
            ));
            inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                    line.itemId(),
                    LOCATION_OUTSOURCE,
                    shipmentDate,
                    inType,
                    line.issueQty(),
                    BigDecimal.ZERO,
                    referenceType,
                    shipmentId,
                    null,
                    line.inputProcessId(),
                    partnerId,
                    lotId,
                    actorUserId
            ));
        }
    }
}
