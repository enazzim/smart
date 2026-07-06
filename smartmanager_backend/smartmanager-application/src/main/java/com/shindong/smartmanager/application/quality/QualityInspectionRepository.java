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
            Instant completedAt,
            String actorUserId
    );

    Optional<QualityInspectionView> findPendingByReceiptLineId(long receiptLineId);

    Optional<QualityInspectionView> findActiveByReceiptLineId(long receiptLineId);

    void cancelByReceiptLineId(long receiptLineId, String actorUserId);

    void cancelById(long id, String actorUserId);
}
