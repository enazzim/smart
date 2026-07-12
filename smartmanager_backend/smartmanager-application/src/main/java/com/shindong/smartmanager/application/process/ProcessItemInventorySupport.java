package com.shindong.smartmanager.application.process;

import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.util.Comparator;
import java.util.Optional;

/** 공정품 재고 슬롯(최종 사내공정) 조회 */
public final class ProcessItemInventorySupport {

    private ProcessItemInventorySupport() {
    }

    public static Optional<ProcessView> resolveFinalInhouseProcess(ProcessRepository processRepository, long itemId) {
        return processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan).stream()
                .filter(process -> process.workDistinction() == WorkDistinction.INHOUSE
                        || process.workDistinction() == WorkDistinction.SPLIT)
                .max(Comparator.comparingInt(ProcessView::processSequenceNum));
    }

    public static Optional<Long> resolveFinalInhouseProcessId(ProcessRepository processRepository, long itemId) {
        return resolveFinalInhouseProcess(processRepository, itemId).map(ProcessView::id);
    }

    public static long requireFinalInhouseProcessId(ProcessRepository processRepository, long itemId, String itemNo) {
        return resolveFinalInhouseProcessId(processRepository, itemId)
                .orElseThrow(() -> new IllegalArgumentException(
                        "출고할 공정품의 최종 사내공정이 없습니다. 품목=" + itemNo + " — 공정 계획을 확인해 주세요."
                ));
    }
}
