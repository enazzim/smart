package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.process.InventoryBalanceRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.Year;
import java.util.List;

public class OutsourcingReceiptInventoryService {

    private static final String REFERENCE_TYPE = "OUTSOURCING_RECEIPT";
    private static final String REFERENCE_TYPE_CANCEL = "OUTSOURCING_RECEIPT_CANCEL";
    private static final String LOCATION_OUTSOURCE = "OUTSOURCE";
    private static final String LOCATION_WIP = "WIP";

    private final InventoryBalanceService inventoryBalanceService;
    private final InventoryBalanceRepository inventoryBalanceRepository;
    private final WipBalanceProjector wipBalanceProjector;
    private final ProcessRepository processRepository;
    private final OutsourcingShipmentConsumptionCalculator consumptionCalculator;
    private final SystemSettingService systemSettingService;

    public OutsourcingReceiptInventoryService(
            InventoryBalanceService inventoryBalanceService,
            InventoryBalanceRepository inventoryBalanceRepository,
            WipBalanceProjector wipBalanceProjector,
            ProcessRepository processRepository,
            OutsourcingShipmentConsumptionCalculator consumptionCalculator,
            SystemSettingService systemSettingService
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.inventoryBalanceRepository = inventoryBalanceRepository;
        this.wipBalanceProjector = wipBalanceProjector;
        this.processRepository = processRepository;
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
            String actorUserId
    ) {
        apply(receiptDate, receiptId, partnerId, order, orderLine, receiptQty, amount, actorUserId, false);
    }

    public void applyCancellation(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal receiptQty,
            BigDecimal amount,
            String actorUserId
    ) {
        apply(receiptDate, receiptId, partnerId, order, orderLine, receiptQty, amount, actorUserId, true);
    }

    private void apply(
            LocalDate receiptDate,
            long receiptId,
            long partnerId,
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal receiptQty,
            BigDecimal amount,
            String actorUserId,
            boolean reverse
    ) {
        int fiscalYear = Year.now().getValue();
        StockMovementType outsourceType = reverse ? StockMovementType.IN : StockMovementType.OUT;
        StockMovementType wipType = reverse ? StockMovementType.OUT : StockMovementType.IN;
        String referenceType = reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE;

        List<OutsourcingShipmentInputSaveCommand> inputs = consumptionCalculator.calculateInputLines(
                order, orderLine, receiptQty, actorUserId);

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
                    actorUserId
            ));
        }

        long endProcessId = resolveEndProcessSequenceId(orderLine);
        wipBalanceProjector.ensure(orderLine.itemId(), endProcessId, actorUserId);
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                orderLine.itemId(),
                LOCATION_WIP,
                receiptDate,
                wipType,
                receiptQty,
                amount,
                referenceType,
                receiptId,
                endProcessId,
                null,
                null,
                actorUserId
        ));
    }

    private long resolveEndProcessSequenceId(OutsourcingOrderLineView orderLine) {
        return processRepository.findAllActiveByItemId(orderLine.itemId(), ProcessVariant.plan).stream()
                .filter(process -> process.processCodeId() == orderLine.endProcessCodeId())
                .map(ProcessView::id)
                .findFirst()
                .orElseThrow(() -> new IllegalArgumentException(
                        "외주 완료 공정을 찾을 수 없습니다. 품목=" + orderLine.itemNo()
                                + ", 공정코드=" + orderLine.endProcessCode()));
    }
}
