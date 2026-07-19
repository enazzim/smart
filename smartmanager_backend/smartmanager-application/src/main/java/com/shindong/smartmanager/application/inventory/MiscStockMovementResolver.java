package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessSequenceNavigator;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;

public class MiscStockMovementResolver {

    private static final String LOCATION_RAW = "RAW";
    private static final String LOCATION_WIP = "WIP";
    private static final String LOCATION_SALES = "SALES";

    private final ProcessRepository processRepository;

    public MiscStockMovementResolver(ProcessRepository processRepository) {
        this.processRepository = processRepository;
    }

    public MiscStockMovementTarget resolve(ItemView item, Long processSequenceId) {
        PropertyClassification classification = item.propertyClassification();
        if (classification == null) {
            throw new IllegalArgumentException("품목 재고분류가 없습니다: " + item.itemNo());
        }
        return switch (classification) {
            case 원자재 -> MiscStockMovementTarget.of(LOCATION_RAW, null, null, null, classification);
            case 상품 -> MiscStockMovementTarget.of(LOCATION_SALES, null, null, null, classification);
            case 공정품 -> resolveWipProcess(item, processSequenceId, classification);
            case 제품 -> resolveProduct(item, processSequenceId, classification);
            case 부자재 -> throw new IllegalArgumentException(
                    "부자재는 창고 재고를 관리하지 않아 기타입출고 대상이 아닙니다: " + item.itemNo());
        };
    }

    private MiscStockMovementTarget resolveWipProcess(
            ItemView item,
            Long processSequenceId,
            PropertyClassification classification
    ) {
        ProcessView process = requireProcessBelongingToItem(item, processSequenceId);
        return MiscStockMovementTarget.of(
                LOCATION_WIP,
                process.id(),
                process.processSequenceNum(),
                process.processName(),
                classification
        );
    }

    private MiscStockMovementTarget resolveProduct(
            ItemView item,
            Long processSequenceId,
            PropertyClassification classification
    ) {
        ProcessView process = requireProcessBelongingToItem(item, processSequenceId);
        if (ProcessSequenceNavigator.isFinalProcess(
                processRepository,
                item.id(),
                process.processSequenceNum()
        )) {
            return MiscStockMovementTarget.of(LOCATION_SALES, null, null, null, classification);
        }
        return MiscStockMovementTarget.of(
                LOCATION_WIP,
                process.id(),
                process.processSequenceNum(),
                process.processName(),
                classification
        );
    }

    private ProcessView requireProcessBelongingToItem(ItemView item, Long processSequenceId) {
        if (processSequenceId == null) {
            throw new IllegalArgumentException(
                    item.propertyClassification().name() + " 품목은 공정을 선택해 주세요.");
        }
        ProcessView process = processRepository.findActiveById(processSequenceId)
                .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + processSequenceId));
        if (process.itemId() != item.id()) {
            throw new IllegalArgumentException("선택한 공정이 해당 품목의 공정이 아닙니다.");
        }
        boolean registeredOnItem = processRepository.findAllActiveByItemId(item.id(), ProcessVariant.plan).stream()
                .anyMatch(candidate -> candidate.id() == process.id());
        if (!registeredOnItem) {
            throw new IllegalArgumentException("선택한 공정이 해당 품목의 공정순서에 없습니다.");
        }
        return process;
    }
}
