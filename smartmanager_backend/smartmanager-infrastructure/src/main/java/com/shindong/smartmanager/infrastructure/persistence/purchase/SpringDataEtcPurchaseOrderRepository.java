package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataEtcPurchaseOrderRepository extends JpaRepository<EtcPurchaseOrderJpaEntity, Long> {

    Optional<EtcPurchaseOrderJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    long countByOrderNoStartingWithAndRecordingState(String prefix, int recordingState);
}
