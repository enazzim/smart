package com.shindong.smartmanager.infrastructure.persistence.purchase;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataEtcPurchaseReceiptRepository extends JpaRepository<EtcPurchaseReceiptJpaEntity, Long> {

    Optional<EtcPurchaseReceiptJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    long countByReceiptNoStartingWithAndRecordingState(String prefix, int recordingState);
}
