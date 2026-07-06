package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.Comparator;
import java.util.List;
import java.util.Optional;

public class WorkReportInventoryService {

    private static final String REFERENCE_TYPE = "WORK_REPORT";
    private static final String REFERENCE_TYPE_CANCEL = "WORK_REPORT_CANCEL";
    private static final String LOCATION_WIP = "WIP";
    private static final String LOCATION_SALES = "SALES";

    private final InventoryBalanceService inventoryBalanceService;
    private final ProcessRepository processRepository;
    private final WipBalanceProjector wipBalanceProjector;

    public WorkReportInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ProcessRepository processRepository,
            WipBalanceProjector wipBalanceProjector
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.processRepository = processRepository;
        this.wipBalanceProjector = wipBalanceProjector;
    }

    public void applyRegistration(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty,
            long reportId,
            String actorUserId
    ) {
        apply(context, goodQty, scrapQty, reportId, actorUserId, false);
    }

    public void applyCancellation(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty,
            long reportId,
            String actorUserId
    ) {
        apply(context, goodQty, scrapQty, reportId, actorUserId, true);
    }

    private void apply(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty,
            long reportId,
            String actorUserId,
            boolean reverse
    ) {
        wipBalanceProjector.ensure(context.itemId(), context.processSequenceId(), actorUserId);

        boolean firstInhouseProcess = isFirstInhouseProcess(context.itemId(), context.processSequenceNum());
        BigDecimal outQty = goodQty.add(scrapQty);
        if (outQty.compareTo(BigDecimal.ZERO) > 0 && !firstInhouseProcess) {
            record(reverse, context.itemId(), LOCATION_WIP, context.reportDate(),
                    StockMovementType.OUT,
                    outQty, context.processSequenceId(), null,
                    reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE, reportId, actorUserId);
        }

        if (goodQty.compareTo(BigDecimal.ZERO) > 0) {
            Optional<Long> nextProcessId = findNextInhouseProcessId(context.itemId(), context.processSequenceNum());
            if (nextProcessId.isPresent()) {
                long nextId = nextProcessId.get();
                wipBalanceProjector.ensure(context.itemId(), nextId, actorUserId);
                record(reverse, context.itemId(), LOCATION_WIP, context.reportDate(),
                        StockMovementType.IN,
                        goodQty, nextId, null,
                        reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE, reportId, actorUserId);
            } else {
                record(reverse, context.itemId(), LOCATION_SALES, context.reportDate(),
                        StockMovementType.IN,
                        goodQty, null, null,
                        reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE, reportId, actorUserId);
            }
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

    private Optional<Long> findNextInhouseProcessId(long itemId, short currentSequenceNum) {
        List<ProcessView> processes = processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan).stream()
                .filter(process -> process.workDistinction() == WorkDistinction.INHOUSE
                        || process.workDistinction() == WorkDistinction.SPLIT)
                .sorted(Comparator.comparing(ProcessView::processSequenceNum))
                .toList();
        return processes.stream()
                .filter(process -> process.processSequenceNum() > currentSequenceNum)
                .map(ProcessView::id)
                .findFirst();
    }

    /**
     * 첫 사내공정은 모품목 WIP 잔고가 없음 — 자재투입이 하위품만 출고하므로 WIP OUT 생략.
     */
    private boolean isFirstInhouseProcess(long itemId, short currentSequenceNum) {
        Optional<Short> minSequence = processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan).stream()
                .filter(process -> process.workDistinction() == WorkDistinction.INHOUSE
                        || process.workDistinction() == WorkDistinction.SPLIT)
                .map(ProcessView::processSequenceNum)
                .min(Short::compare);
        return minSequence.map(min -> min.equals(currentSequenceNum)).orElse(true);
    }

}
