package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataPurchaseHistoryRepository extends JpaRepository<PurchaseHistoryJpaEntity, Long> {

    List<PurchaseHistoryJpaEntity> findBySourceTypeAndSourceIdAndRecordingState(
            PurchaseHistorySourceType sourceType,
            long sourceId,
            int recordingState
    );
}
