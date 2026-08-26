package com.shindong.smartmanager.application.process;

import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.util.Comparator;
import java.util.Optional;

/** 공정품 재고 슬롯(라우팅 마지막 공정) 조회 */
public final class ProcessItemInventorySupport {

    private ProcessItemInventorySupport() {
    }

    public static Optional<ProcessView> resolveFinalProcess(ProcessRepository processRepository, long itemId) {
        return processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan).stream()
                .max(Comparator.comparingInt(ProcessView::processSequenceNum));
    }

    public static Optional<Long> resolveFinalProcessId(ProcessRepository processRepository, long itemId) {
        return resolveFinalProcess(processRepository, itemId).map(ProcessView::id);
    }

    public static long requireFinalProcessId(ProcessRepository processRepository, long itemId, String itemNo) {
        return resolveFinalProcessId(processRepository, itemId)
                .orElseThrow(() -> new IllegalArgumentException(
                        "출고할 공정품의 최종 공정이 없습니다. 품목=" + itemNo + " — 공정 계획을 확인해 주세요."
                ));
    }
}
