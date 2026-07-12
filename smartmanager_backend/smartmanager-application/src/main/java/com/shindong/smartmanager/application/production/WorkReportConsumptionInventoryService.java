package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessSequenceNavigator;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public class WorkReportConsumptionInventoryService {

    private static final String REFERENCE_TYPE = "WORK_REPORT_CONSUMPTION";
    private static final String REFERENCE_TYPE_CANCEL = "WORK_REPORT_CONSUMPTION_CANCEL";
    private static final String LOCATION_RAW = "RAW";
    private static final String LOCATION_WIP = "WIP";

    private final InventoryBalanceService inventoryBalanceService;
    private final ProcessRepository processRepository;
    private final ItemRepository itemRepository;
    private final WipBalanceProjector wipBalanceProjector;
    private final BomConsumptionCalculator bomConsumptionCalculator;

    public WorkReportConsumptionInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            WipBalanceProjector wipBalanceProjector,
            BomConsumptionCalculator bomConsumptionCalculator
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.wipBalanceProjector = wipBalanceProjector;
        this.bomConsumptionCalculator = bomConsumptionCalculator;
    }

    public void applyRegistration(
            LocalDate reportDate,
            long reportId,
            List<WorkReportConsumptionRecordView> lines,
            String actorUserId
    ) {
        apply(reportDate, reportId, lines, actorUserId, false);
    }

    public BigDecimal resolveOnHandQty(
            long itemId,
            long parentItemId,
            short currentProcessSequenceNum,
            LocalDate reportDate,
            String actorUserId
    ) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("투입 품목을 찾을 수 없습니다: " + itemId));

        if (item.propertyClassification() == PropertyClassification.원자재) {
            return inventoryBalanceService.currentStockQty(
                    item.id(),
                    LOCATION_RAW,
                    reportDate,
                    null,
                    null,
                    null
            );
        }
        Long wipSourceProcessId = resolveWipSourceProcessId(
                item.id(),
                item.propertyClassification(),
                parentItemId,
                currentProcessSequenceNum
        );
        if (wipSourceProcessId != null) {
            return inventoryBalanceService.currentStockQty(
                    item.id(),
                    LOCATION_WIP,
                    reportDate,
                    wipSourceProcessId,
                    null,
                    null
            );
        }
        return BigDecimal.ZERO;
    }

    public void assertSufficientStock(
            LocalDate reportDate,
            List<WorkReportConsumptionSaveCommand> commands
    ) {
        for (WorkReportConsumptionSaveCommand command : commands) {
            ItemView item = itemRepository.findActiveById(command.itemId())
                    .orElseThrow(() -> new IllegalArgumentException(
                            "투입 품목을 찾을 수 없습니다: " + command.itemId()));
            inventoryBalanceService.assertSufficientStockForOutbound(
                    command.itemId(),
                    item.itemNo(),
                    command.locationCode(),
                    reportDate,
                    command.issueQty(),
                    command.sourceProcessId(),
                    null
            );
        }
    }

    public void applyCancellation(
            LocalDate reportDate,
            long reportId,
            List<WorkReportConsumptionRecordView> lines,
            String actorUserId
    ) {
        apply(reportDate, reportId, lines, actorUserId, true);
    }

    private void apply(
            LocalDate reportDate,
            long reportId,
            List<WorkReportConsumptionRecordView> lines,
            String actorUserId,
            boolean reverse
    ) {
        for (WorkReportConsumptionRecordView line : lines) {
            if (line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            recordLine(reportDate, reportId, line, actorUserId, reverse);
        }
    }

    private void recordLine(
            LocalDate reportDate,
            long reportId,
            WorkReportConsumptionRecordView line,
            String actorUserId,
            boolean reverse
    ) {
        if (LOCATION_WIP.equals(line.locationCode()) && line.sourceProcessId() != null) {
            wipBalanceProjector.ensure(line.itemId(), line.sourceProcessId(), actorUserId);
        }

        StockMovementType movementType = reverse ? StockMovementType.IN : StockMovementType.OUT;
        inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                line.itemId(),
                line.locationCode(),
                reportDate,
                movementType,
                line.issueQty(),
                BigDecimal.ZERO,
                reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE,
                reportId,
                line.sourceProcessId(),
                null,
                null,
                line.lotId(),
                actorUserId
        ));
    }

    public WorkReportConsumptionSaveCommand resolveSaveCommand(
            WorkReportIssueLineCommand issueLine,
            long parentItemId,
            short currentProcessSequenceNum,
            String actorUserId
    ) {
        ItemView item = itemRepository.findActiveById(issueLine.itemId())
                .orElseThrow(() -> new IllegalArgumentException("투입 품목을 찾을 수 없습니다: " + issueLine.itemId()));

        if (item.propertyClassification() == PropertyClassification.원자재) {
            if (item.lotTracked() && issueLine.lotId() == null) {
                throw new IllegalArgumentException(
                        "Lot 추적 품목은 Lot를 선택해야 합니다: " + item.itemNo());
            }
            return new WorkReportConsumptionSaveCommand(
                    item.id(),
                    issueLine.itemCompositionId(),
                    issueLine.issueQty(),
                    LOCATION_RAW,
                    null,
                    issueLine.lotId()
            );
        }
        Long wipSourceProcessId = resolveWipSourceProcessId(
                item.id(),
                item.propertyClassification(),
                parentItemId,
                currentProcessSequenceNum
        );
        if (wipSourceProcessId != null) {
            if (item.lotTracked() && issueLine.lotId() == null) {
                throw new IllegalArgumentException(
                        "Lot 추적 품목은 Lot를 선택해야 합니다: " + item.itemNo());
            }
            return new WorkReportConsumptionSaveCommand(
                    item.id(),
                    issueLine.itemCompositionId(),
                    issueLine.issueQty(),
                    LOCATION_WIP,
                    wipSourceProcessId,
                    issueLine.lotId()
            );
        }
        throw new IllegalArgumentException("투입할 수 없는 품목 분류입니다: " + item.propertyClassification());
    }

    /**
     * 비첫 공정에서 모품목(자기 자신) 투입은 직전 공정 실적이 넣어 둔 <b>현재 공정 WIP</b>를 사용한다.
     * (WorkReportInventoryService: 완료 시 next process WIP IN)
     */
    private Long resolveWipSourceProcessId(
            long itemId,
            PropertyClassification classification,
            long parentItemId,
            short currentProcessSequenceNum
    ) {
        if (itemId == parentItemId
                && !ProcessSequenceNavigator.isFirstProcess(
                        processRepository, parentItemId, currentProcessSequenceNum)) {
            return processRepository.findAllActiveByItemId(parentItemId, ProcessVariant.plan).stream()
                    .filter(process -> process.processSequenceNum() == currentProcessSequenceNum)
                    .map(ProcessView::id)
                    .findFirst()
                    .orElse(null);
        }
        if (classification == PropertyClassification.공정품) {
            return bomConsumptionCalculator.resolveIssueSourceProcessId(
                    classification,
                    itemId,
                    parentItemId,
                    currentProcessSequenceNum
            );
        }
        return null;
    }
}
