package com.shindong.smartmanager.infrastructure.persistence.purchase;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataPurchaseReceiptLineRepository extends JpaRepository<PurchaseReceiptLineJpaEntity, Long> {

    List<PurchaseReceiptLineJpaEntity> findByPurchaseReceiptIdAndRecordingStateOrderByLineNoAsc(
            long purchaseReceiptId,
            int recordingState
    );

    Optional<PurchaseReceiptLineJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
