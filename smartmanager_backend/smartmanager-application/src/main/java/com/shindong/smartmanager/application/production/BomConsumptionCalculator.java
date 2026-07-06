package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.Optional;

public class BomConsumptionCalculator {

    private static final int QTY_SCALE = 4;
    private static final RoundingMode QTY_ROUNDING = RoundingMode.HALF_UP;

    private final ItemCompositionRepository itemCompositionRepository;
    private final ItemRepository itemRepository;
    private final ProcessRepository processRepository;

    public BomConsumptionCalculator(
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository,
            ProcessRepository processRepository
    ) {
        this.itemCompositionRepository = itemCompositionRepository;
        this.itemRepository = itemRepository;
        this.processRepository = processRepository;
    }

    public List<WorkReportConsumptionLineView> calculateLines(
            long parentItemId,
            short currentProcessSequenceNum,
            BigDecimal totalGoodQty,
            Map<Long, BigDecimal> issuedQtyByCompositionId
    ) {
        BigDecimal goodQty = totalGoodQty != null && totalGoodQty.compareTo(BigDecimal.ZERO) > 0
                ? totalGoodQty
                : BigDecimal.ZERO;

        List<WorkReportConsumptionLineView> lines = new ArrayList<>();
        for (ItemCompositionView bomLine : itemCompositionRepository.findActiveByParentItemId(parentItemId)) {
            ItemView child = itemRepository.findActiveById(bomLine.childItemId())
                    .orElseThrow(() -> new IllegalArgumentException(
                            "BOM 자품목을 찾을 수 없습니다: " + bomLine.childItemId()));

            BigDecimal unitRatio = bomLine.childQuantity()
                    .divide(bomLine.parentQuantity(), QTY_SCALE, QTY_ROUNDING);
            BigDecimal requiredQty = goodQty.compareTo(BigDecimal.ZERO) > 0
                    ? unitRatio.multiply(goodQty).setScale(QTY_SCALE, QTY_ROUNDING)
                    : BigDecimal.ZERO.setScale(QTY_SCALE, QTY_ROUNDING);
            BigDecimal issuedQty = issuedQtyByCompositionId.getOrDefault(bomLine.id(), BigDecimal.ZERO);
            BigDecimal remainingQty = requiredQty.subtract(issuedQty).max(BigDecimal.ZERO);
            boolean satisfied = goodQty.compareTo(BigDecimal.ZERO) == 0 || issuedQty.compareTo(requiredQty) >= 0;

            SourceProcess sourceProcess = resolveSourceProcess(
                    child,
                    parentItemId,
                    currentProcessSequenceNum
            );

            lines.add(new WorkReportConsumptionLineView(
                    bomLine.id(),
                    child.id(),
                    child.itemNo(),
                    child.itemName(),
                    child.propertyClassification().name(),
                    unitRatio,
                    requiredQty,
                    issuedQty,
                    remainingQty,
                    satisfied,
                    sourceProcess.processId(),
                    sourceProcess.processName(),
                    BigDecimal.ZERO.setScale(QTY_SCALE, QTY_ROUNDING)
            ));
        }
        return lines;
    }

    private SourceProcess resolveSourceProcess(
            ItemView child,
            long parentItemId,
            short currentProcessSequenceNum
    ) {
        if (child.propertyClassification() == PropertyClassification.원자재) {
            return new SourceProcess(null, "원자재");
        }
        if (child.propertyClassification() == PropertyClassification.공정품) {
            Optional<ProcessView> priorOnParent = resolvePriorInhouseProcess(parentItemId, currentProcessSequenceNum);
            if (priorOnParent.isPresent()) {
                ProcessView process = priorOnParent.get();
                return new SourceProcess(process.id(), process.processName());
            }
            return resolveFinalInhouseProcess(child.id())
                    .map(process -> new SourceProcess(process.id(), process.processName()))
                    .orElse(new SourceProcess(null, "공정 미등록"));
        }
        return new SourceProcess(null, null);
    }

    private Optional<ProcessView> resolvePriorInhouseProcess(long parentItemId, short currentProcessSequenceNum) {
        return processRepository.findAllActiveByItemId(parentItemId, ProcessVariant.plan).stream()
                .filter(process -> process.processSequenceNum() < currentProcessSequenceNum)
                .filter(process -> process.workDistinction() == WorkDistinction.INHOUSE
                        || process.workDistinction() == WorkDistinction.SPLIT)
                .max(Comparator.comparingInt(ProcessView::processSequenceNum));
    }

    private Optional<ProcessView> resolveFinalInhouseProcess(long childItemId) {
        return processRepository.findAllActiveByItemId(childItemId, ProcessVariant.plan).stream()
                .filter(process -> process.workDistinction() == WorkDistinction.INHOUSE
                        || process.workDistinction() == WorkDistinction.SPLIT)
                .max(Comparator.comparingInt(ProcessView::processSequenceNum));
    }

    private record SourceProcess(Long processId, String processName) {
    }

    public Long resolveIssueSourceProcessId(
            PropertyClassification classification,
            long childItemId,
            long parentItemId,
            short currentProcessSequenceNum
    ) {
        if (classification == PropertyClassification.원자재) {
            return null;
        }
        if (classification == PropertyClassification.공정품) {
            Optional<ProcessView> priorOnParent = resolvePriorInhouseProcess(parentItemId, currentProcessSequenceNum);
            if (priorOnParent.isPresent()) {
                return priorOnParent.get().id();
            }
            return resolveFinalInhouseProcess(childItemId).map(ProcessView::id).orElse(null);
        }
        return null;
    }
}
