package com.shindong.smartmanager.application.quality;

import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

public interface QualityInspectionRepository {

    long createPending(
            QualityInspectionSourceType sourceType,
            long sourceReceiptLineId,
            long itemId,
            long companyId,
            BigDecimal requestQty,
            String actorUserId
    );

    Optional<QualityInspectionView> findActiveById(long id);

    List<QualityInspectionView> findActive(QualityInspectionListCriteria criteria);

    void complete(
            long id,
            BigDecimal passedQty,
            BigDecimal failedQty,
            Long inspectionDecisionCodeId,
            Long unsuitabilityCauseCodeId,
            Long unsuitabilityStatusCodeId,
            String failureReason,
            Instant completedAt,
            String actorUserId
    );

    Optional<QualityInspectionView> findPendingByReceiptLineId(long receiptLineId);

    Optional<QualityInspectionView> findActiveByReceiptLineId(long receiptLineId);

    void cancelByReceiptLineId(long receiptLineId, String actorUserId);

    void cancelById(long id, String actorUserId);

    /** 완료 검사를 대기 상태로 되돌린다 (검사 이력 취소). */
    void revertCompletedToPending(long id, String actorUserId);
}
