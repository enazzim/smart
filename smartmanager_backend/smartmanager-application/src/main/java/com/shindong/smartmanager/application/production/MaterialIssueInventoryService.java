package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.RecordStockMovementCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public class MaterialIssueInventoryService {

    private static final String REFERENCE_TYPE = "MATERIAL_ISSUE";
    private static final String REFERENCE_TYPE_CANCEL = "MATERIAL_ISSUE_CANCEL";
    private static final String LOCATION_RAW = "RAW";
    private static final String LOCATION_WIP = "WIP";

    private final InventoryBalanceService inventoryBalanceService;
    private final ItemRepository itemRepository;
    private final WipBalanceProjector wipBalanceProjector;
    private final BomConsumptionCalculator bomConsumptionCalculator;

    public MaterialIssueInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ItemRepository itemRepository,
            WipBalanceProjector wipBalanceProjector,
            BomConsumptionCalculator bomConsumptionCalculator
    ) {
        this.inventoryBalanceService = inventoryBalanceService;
        this.itemRepository = itemRepository;
        this.wipBalanceProjector = wipBalanceProjector;
        this.bomConsumptionCalculator = bomConsumptionCalculator;
    }

    public BigDecimal resolveOnHandQty(
            long itemId,
            long parentItemId,
            short currentProcessSequenceNum,
            LocalDate issueDate
    ) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("투입 품목을 찾을 수 없습니다: " + itemId));

        if (item.propertyClassification() == PropertyClassification.원자재) {
            return inventoryBalanceService.currentStockQty(
                    item.id(), LOCATION_RAW, issueDate, null, null, null);
        }
        if (item.propertyClassification() == PropertyClassification.공정품) {
            Long sourceProcessId = bomConsumptionCalculator.resolveIssueSourceProcessId(
                    item.propertyClassification(),
                    item.id(),
                    parentItemId,
                    currentProcessSequenceNum
            );
            if (sourceProcessId == null) {
                return BigDecimal.ZERO;
            }
            return inventoryBalanceService.currentStockQty(
                    item.id(), LOCATION_WIP, issueDate, sourceProcessId, null, null);
        }
        return BigDecimal.ZERO;
    }

    public void assertSufficientStock(LocalDate issueDate, List<MaterialIssueLineSaveCommand> commands) {
        for (MaterialIssueLineSaveCommand command : commands) {
            ItemView item = itemRepository.findActiveById(command.itemId())
                    .orElseThrow(() -> new IllegalArgumentException(
                            "투입 품목을 찾을 수 없습니다: " + command.itemId()));
            inventoryBalanceService.assertSufficientStockForOutbound(
                    command.itemId(),
                    item.itemNo(),
                    command.locationCode(),
                    issueDate,
                    command.issueQty(),
                    command.sourceProcessId(),
                    null
            );
        }
    }

    public void applyRegistration(
            LocalDate issueDate,
            long issueId,
            List<MaterialIssueLineRecordView> lines,
            String actorUserId
    ) {
        apply(issueDate, issueId, lines, actorUserId, false);
    }

    public void applyCancellation(
            LocalDate issueDate,
            long issueId,
            List<MaterialIssueLineRecordView> lines,
            String actorUserId
    ) {
        apply(issueDate, issueId, lines, actorUserId, true);
    }

    public MaterialIssueLineSaveCommand resolveSaveCommand(
            MaterialIssueLineCommand lineCommand,
            long itemId,
            long parentItemId,
            short currentProcessSequenceNum
    ) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("투입 품목을 찾을 수 없습니다: " + itemId));

        if (item.propertyClassification() == PropertyClassification.원자재) {
            return new MaterialIssueLineSaveCommand(
                    item.id(), lineCommand.itemCompositionId(), lineCommand.issueQty(), LOCATION_RAW, null);
        }
        if (item.propertyClassification() == PropertyClassification.공정품) {
            Long sourceProcessId = bomConsumptionCalculator.resolveIssueSourceProcessId(
                    item.propertyClassification(),
                    item.id(),
                    parentItemId,
                    currentProcessSequenceNum
            );
            if (sourceProcessId == null) {
                throw new IllegalArgumentException(
                        "공정품 투입 공정을 찾을 수 없습니다: " + item.itemNo() + " — 공정 계획을 확인해 주세요.");
            }
            return new MaterialIssueLineSaveCommand(
                    item.id(), lineCommand.itemCompositionId(), lineCommand.issueQty(), LOCATION_WIP, sourceProcessId);
        }
        throw new IllegalArgumentException("투입할 수 없는 품목 분류입니다: " + item.propertyClassification());
    }

    private void apply(
            LocalDate issueDate,
            long issueId,
            List<MaterialIssueLineRecordView> lines,
            String actorUserId,
            boolean reverse
    ) {
        for (MaterialIssueLineRecordView line : lines) {
            if (line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            if (LOCATION_WIP.equals(line.locationCode()) && line.sourceProcessId() != null) {
                wipBalanceProjector.ensure(line.itemId(), line.sourceProcessId(), actorUserId);
            }
            StockMovementType movementType = reverse ? StockMovementType.IN : StockMovementType.OUT;
            inventoryBalanceService.recordMovement(new RecordStockMovementCommand(
                    line.itemId(),
                    line.locationCode(),
                    issueDate,
                    movementType,
                    line.issueQty(),
                    BigDecimal.ZERO,
                    reverse ? REFERENCE_TYPE_CANCEL : REFERENCE_TYPE,
                    issueId,
                    line.sourceProcessId(),
                    null,
                    null,
                    actorUserId
            ));
        }
    }
}
