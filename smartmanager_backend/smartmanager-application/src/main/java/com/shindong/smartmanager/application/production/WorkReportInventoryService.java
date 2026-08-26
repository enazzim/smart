package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.LotService;
import com.shindong.smartmanager.application.inventory.LotView;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessSequenceNavigator;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public class WorkReportInventoryService {

    private static final String REFERENCE_TYPE = "WORK_REPORT";
    private static final String REFERENCE_TYPE_CANCEL = "WORK_REPORT_CANCEL";
    private static final String LOCATION_WIP = "WIP";
    private static final String LOCATION_SALES = "SALES";

    private final InventoryBalanceService inventoryBalanceService;
    private final ProcessRepository processRepository;
    private final ItemRepository itemRepository;
    private final WipBalanceProjector wipBalanceProjector;
    private final LotService lotService;

    public WorkReportInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            WipBalanceProjector wipBalanceProjector,
            LotService lotService
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.wipBalanceProjector = wipBalanceProjector;
        this.lotService = lotService;
    }

    /**
     * @param preferredInputLotId 비첫 공정에서 UI가 선택한 직전 공정 WIP Lot (없으면 가용 Lot 자동 선택)
     * @return 산출 Lot id (lot_tracked 품목), 아니면 null
     */
    public Long applyRegistration(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty,
            long reportId,
            Long preferredInputLotId,
            String outputLotNo,
            boolean autoGenerateOutputLot,
            String actorUserId
    ) {
        return apply(
                context,
                goodQty,
                scrapQty,
                reportId,
                null,
                preferredInputLotId,
                outputLotNo,
                autoGenerateOutputLot,
                actorUserId,
                false
        );
    }

    public void applyCancellation(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty,
            long reportId,
            Long outputLotId,
            String actorUserId
    ) {
        apply(context, goodQty, scrapQty, reportId, outputLotId, null, null, false, actorUserId, true);
    }

    private Long apply(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty,
            long reportId,
            Long existingOutputLotId,
            Long preferredInputLotId,
            String outputLotNo,
            boolean autoGenerateOutputLot,
            String actorUserId,
            boolean reverse
    ) {
        ItemView item = itemRepository.findActiveById(context.itemId())
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + context.itemId()));

        wipBalanceProjector.ensure(context.itemId(), context.processSequenceId(), actorUserId);

        boolean firstProcess = ProcessSequenceNavigator.isFirstProcess(
                processRepository, context.itemId(), context.processSequenceNum());
        Long inputProcessId = firstProcess ? null : requirePriorProcessId(context);
        if (inputProcessId != null) {
            wipBalanceProjector.ensure(context.itemId(), inputProcessId, actorUserId);
        }
        BigDecimal outQty = goodQty.add(scrapQty);

        Long lotId;
        if (reverse) {
            lotId = item.lotTracked() ? existingOutputLotId : null;
        } else {
            lotId = resolveOutputLotId(
                    item,
                    firstProcess,
                    inputProcessId,
                    outQty,
                    goodQty,
                    preferredInputLotId,
                    outputLotNo,
                    autoGenerateOutputLot,
                    reportId,
                    actorUserId
            );
        }

        if (outQty.compareTo(BigDecimal.ZERO) > 0 && inputProcessId != null) {
            record(reverse, context.itemId(), LOCATION_WIP, context.reportDate(),
                    StockMovementType.OUT,
                    outQty, inputProcessId, null,
                    reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE, reportId, lotId, actorUserId);
        }

        if (goodQty.compareTo(BigDecimal.ZERO) > 0) {
            boolean lastProcess = ProcessSequenceNavigator.isFinalProcess(
                    processRepository, context.itemId(), context.processSequenceNum());
            if (lastProcess && item.propertyClassification().salesWarehouseAtProductionComplete()) {
                record(reverse, context.itemId(), LOCATION_SALES, context.reportDate(),
                        StockMovementType.IN,
                        goodQty, null, null,
                        reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE, reportId, lotId, actorUserId);
            } else {
                record(reverse, context.itemId(), LOCATION_WIP, context.reportDate(),
                        StockMovementType.IN,
                        goodQty, context.processSequenceId(), null,
                        reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE, reportId, lotId, actorUserId);
            }
        }
        return reverse ? existingOutputLotId : lotId;
    }

    private Long resolveOutputLotId(
            ItemView item,
            boolean firstProcess,
            Long inputProcessId,
            BigDecimal outQty,
            BigDecimal goodQty,
            Long preferredInputLotId,
            String outputLotNo,
            boolean autoGenerateOutputLot,
            long reportId,
            String actorUserId
    ) {
        if (!item.lotTracked()) {
            return null;
        }
        if (!firstProcess && inputProcessId != null && outQty.compareTo(BigDecimal.ZERO) > 0) {
            if (preferredInputLotId != null) {
                assertPreferredLotAvailable(item, inputProcessId, preferredInputLotId, outQty);
                return preferredInputLotId;
            }
            List<LotView> available = lotService.findAvailableLots(
                    item.id(), LOCATION_WIP, inputProcessId);
            LotView matched = available.stream()
                    .filter(lot -> lot.balances().stream()
                            .anyMatch(b -> b.qtyOnHand() != null && b.qtyOnHand().compareTo(outQty) >= 0))
                    .findFirst()
                    .orElseThrow(() -> new IllegalArgumentException(
                            "공정창고에 투입할 수 있는 Lot 잔량이 부족합니다: " + item.itemNo()));
            return matched.id();
        }
        if (goodQty.compareTo(BigDecimal.ZERO) <= 0) {
            return null;
        }
        boolean auto = autoGenerateOutputLot
                || outputLotNo == null
                || outputLotNo.isBlank();
        return lotService.createOnProduction(
                item.id(),
                outputLotNo,
                auto,
                "work_report",
                reportId,
                actorUserId
        ).id();
    }

    private void assertPreferredLotAvailable(
            ItemView item,
            long inputProcessId,
            long preferredInputLotId,
            BigDecimal outQty
    ) {
        List<LotView> available = lotService.findAvailableLots(
                item.id(), LOCATION_WIP, inputProcessId);
        LotView matched = available.stream()
                .filter(lot -> lot.id() == preferredInputLotId)
                .findFirst()
                .orElseThrow(() -> new IllegalArgumentException(
                        "선택한 Lot가 직전 공정 창고에 없습니다: " + item.itemNo()));
        boolean enough = matched.balances().stream()
                .anyMatch(b -> b.qtyOnHand() != null && b.qtyOnHand().compareTo(outQty) >= 0);
        if (!enough) {
            throw new IllegalArgumentException(
                    "선택한 Lot 잔량이 부족합니다: " + matched.lotNo());
        }
    }

    private void record(
            boolean reverse,
            long itemId,
            String locationCode,
            LocalDate movementDate,
            StockMovementType movementType,
            BigDecimal qty,
            Long outputProcessId,
            Long inputProcessId,
            String referenceType,
            long referenceId,
            Long lotId,
            String actorUserId
    ) {
        StockMovementType type = reverse ? flip(movementType) : movementType;
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                itemId,
                locationCode,
                movementDate,
                type,
                qty,
                BigDecimal.ZERO,
                referenceType,
                referenceId,
                outputProcessId,
                inputProcessId,
                null,
                lotId,
                actorUserId
        ));
    }

    private static StockMovementType flip(StockMovementType type) {
        return switch (type) {
            case IN -> StockMovementType.OUT;
            case OUT -> StockMovementType.IN;
            case ADJUST -> StockMovementType.ADJUST;
        };
    }

    private long requirePriorProcessId(WorkReportRegistrationContext context) {
        return ProcessSequenceNavigator.findImmediatePriorProcess(
                processRepository,
                context.itemId(),
                context.processSequenceNum()
        ).map(ProcessView::id)
                .orElseThrow(() -> new IllegalArgumentException(
                        "직전 공정을 찾을 수 없습니다. 공정 계획을 확인해 주세요."));
    }
}
