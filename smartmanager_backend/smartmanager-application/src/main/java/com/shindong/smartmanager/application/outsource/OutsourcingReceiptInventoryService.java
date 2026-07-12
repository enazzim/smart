package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.InventoryBalanceRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessSequenceNavigator;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.Year;
import java.util.List;
import java.util.Map;

public class OutsourcingReceiptInventoryService {

    private static final String REFERENCE_TYPE = "OUTSOURCING_RECEIPT";
    private static final String REFERENCE_TYPE_CANCEL = "OUTSOURCING_RECEIPT_CANCEL";
    private static final String LOCATION_OUTSOURCE = "OUTSOURCE";
    private static final String LOCATION_WIP = "WIP";
    private static final String LOCATION_SALES = "SALES";

    private final InventoryBalanceService inventoryBalanceService;
    private final InventoryBalanceRepository inventoryBalanceRepository;
    private final WipBalanceProjector wipBalanceProjector;
    private final ProcessRepository processRepository;
    private final ItemRepository itemRepository;
    private final OutsourcingShipmentConsumptionCalculator consumptionCalculator;
    private final SystemSettingService systemSettingService;

    public OutsourcingReceiptInventoryService(
            InventoryBalanceService inventoryBalanceService,
            InventoryBalanceRepository inventoryBalanceRepository,
            WipBalanceProjector wipBalanceProjector,
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            OutsourcingShipmentConsumptionCalculator consumptionCalculator,
            SystemSettingService systemSettingService
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.inventoryBalanceRepository = inventoryBalanceRepository;
        this.wipBalanceProjector = wipBalanceProjector;
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.consumptionCalculator = consumptionCalculator;
        this.systemSettingService = systemSettingService;
    }

    public void assertSufficientOutsourceStock(
            LocalDate receiptDate,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal receiptQty
    ) {
        List<OutsourcingShipmentInputSaveCommand> inputs = consumptionCalculator.calculateInputLines(
                order, orderLine, receiptQty, "system");
        for (OutsourcingShipmentInputSaveCommand line : inputs) {
            if (line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            if (!systemSettingService.isNegativeStockAllowed()) {
                BigDecimal onHand = inventoryBalanceService.currentStockQty(
                        line.itemId(),
                        LOCATION_OUTSOURCE,
                        receiptDate,
                        null,
                        line.inputProcessId(),
                        partnerId
                );
                if (onHand.compareTo(line.issueQty()) < 0) {
                    throw new IllegalArgumentException(
                            "외주창고 재고가 부족합니다. 품목=" + line.itemId()
                                    + ", 필요=" + line.issueQty().stripTrailingZeros().toPlainString()
                                    + ", 보유=" + onHand.stripTrailingZeros().toPlainString()
                    );
                }
            }
        }
    }

    public void applyRegistration(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal receiptQty,
            BigDecimal amount,
            Long inboundLotId,
            Map<Long, Long> outsourceLotIdByItemId,
            String actorUserId
    ) {
        apply(receiptDate, receiptId, partnerId, order, orderLine, receiptQty, receiptQty, amount,
                inboundLotId, outsourceLotIdByItemId, actorUserId, false);
    }

    public void applyRegistration(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal outsourceDecreaseQty,
            BigDecimal inboundQty,
            BigDecimal amount,
            Long inboundLotId,
            Map<Long, Long> outsourceLotIdByItemId,
            String actorUserId
    ) {
        apply(receiptDate, receiptId, partnerId, order, orderLine, outsourceDecreaseQty, inboundQty, amount,
                inboundLotId, outsourceLotIdByItemId, actorUserId, false);
    }

    public void applyCancellation(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal receiptQty,
            BigDecimal amount,
            Long inboundLotId,
            Map<Long, Long> outsourceLotIdByItemId,
            String actorUserId
    ) {
        apply(receiptDate, receiptId, partnerId, order, orderLine, receiptQty, receiptQty, amount,
                inboundLotId, outsourceLotIdByItemId, actorUserId, true);
    }

    public void applyCancellation(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal outsourceDecreaseQty,
            BigDecimal inboundQty,
            BigDecimal amount,
            Long inboundLotId,
            Map<Long, Long> outsourceLotIdByItemId,
            String actorUserId
    ) {
        apply(receiptDate, receiptId, partnerId, order, orderLine, outsourceDecreaseQty, inboundQty, amount,
                inboundLotId, outsourceLotIdByItemId, actorUserId, true);
    }

    private void apply(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal outsourceDecreaseQty,
            BigDecimal inboundQty,
            BigDecimal amount,
            Long inboundLotId,
            Map<Long, Long> outsourceLotIdByItemId,
            String actorUserId,
            boolean reverse
    ) {
        if (outsourceDecreaseQty.compareTo(BigDecimal.ZERO) > 0) {
            applyOutsourceDecrease(
                    receiptDate,
                    receiptId,
                    partnerId,
                    order,
                    orderLine,
                    outsourceDecreaseQty,
                    outsourceLotIdByItemId,
                    actorUserId,
                    reverse
            );
        }

        if (inboundQty.compareTo(BigDecimal.ZERO) > 0) {
            Long resolvedInboundLotId = inboundLotId;
            if (reverse && resolvedInboundLotId == null) {
                resolvedInboundLotId = inventoryBalanceService
                        .findLotIdByReference(REFERENCE_TYPE, receiptId)
                        .orElse(null);
            }
            applyInbound(
                    receiptDate,
                    receiptId,
                    orderLine,
                    inboundQty,
                    amount,
                    resolvedInboundLotId,
                    actorUserId,
                    reverse
            );
        }
    }

    private void applyOutsourceDecrease(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal outsourceDecreaseQty,
            Map<Long, Long> outsourceLotIdByItemId,
            String actorUserId,
            boolean reverse
    ) {
        int fiscalYear = Year.now().getValue();
        StockMovementType outsourceType = reverse ? StockMovementType.IN : StockMovementType.OUT;
        String referenceType = reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE;

        List<OutsourcingShipmentInputSaveCommand> inputs = consumptionCalculator.calculateInputLines(
                order, orderLine, outsourceDecreaseQty, actorUserId);

        for (OutsourcingShipmentInputSaveCommand line : inputs) {
            if (line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            inventoryBalanceRepository.ensureOutsourceInputBalance(
                    line.itemId(),
                    fiscalYear,
                    partnerId,
                    line.inputProcessId(),
                    actorUserId
            );
            Long lotId = outsourceLotIdByItemId != null
                    ? outsourceLotIdByItemId.get(line.itemId())
                    : null;
            if (lotId == null) {
                lotId = line.lotId();
            }
            if (reverse && lotId == null) {
                lotId = inventoryBalanceService.findLotIdByReference(REFERENCE_TYPE, receiptId)
                        .orElse(null);
            }
            inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                    line.itemId(),
                    LOCATION_OUTSOURCE,
                    receiptDate,
                    outsourceType,
                    line.issueQty(),
                    BigDecimal.ZERO,
                    referenceType,
                    receiptId,
                    null,
                    line.inputProcessId(),
                    partnerId,
                    lotId,
                    actorUserId
            ));
        }
    }

    private void applyInbound(
            LocalDate receiptDate,
            long receiptId,
            OutsourcingOrderLineView orderLine,
            BigDecimal inboundQty,
            BigDecimal amount,
            Long inboundLotId,
            String actorUserId,
            boolean reverse
    ) {
        ItemView item = itemRepository.findActiveById(orderLine.itemId())
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + orderLine.itemId()));
        long endProcessId = resolveEndProcessSequenceId(orderLine);
        boolean finalProcess = ProcessSequenceNavigator.isFinalProcessBySequenceId(
                processRepository,
                orderLine.itemId(),
                endProcessId
        );

        StockMovementType inboundType = reverse ? StockMovementType.OUT : StockMovementType.IN;
        String referenceType = reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE;

        if (finalProcess && item.propertyClassification().salesWarehouseAtProductionComplete()) {
            inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                    orderLine.itemId(),
                    LOCATION_SALES,
                    receiptDate,
                    inboundType,
                    inboundQty,
                    amount,
                    referenceType,
                    receiptId,
                    null,
                    null,
                    null,
                    inboundLotId,
                    actorUserId
            ));
            return;
        }

        wipBalanceProjector.ensure(orderLine.itemId(), endProcessId, actorUserId);
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                orderLine.itemId(),
                LOCATION_WIP,
                receiptDate,
                inboundType,
                inboundQty,
                amount,
                referenceType,
                receiptId,
                endProcessId,
                null,
                null,
                inboundLotId,
                actorUserId
        ));
    }

    private long resolveEndProcessSequenceId(OutsourcingOrderLineView orderLine) {
        return processRepository.findAllActiveByItemId(orderLine.itemId(), ProcessVariant.plan).stream()
                .filter(process -> process.processCodeId() == orderLine.endProcessCodeId())
                .map(com.shindong.smartmanager.application.process.ProcessView::id)
                .findFirst()
                .orElseThrow(() -> new IllegalArgumentException(
                        "외주 완료 공정을 찾을 수 없습니다. 품목=" + orderLine.itemNo()
                                + ", 공정코드=" + orderLine.endProcessCode()));
    }
}
