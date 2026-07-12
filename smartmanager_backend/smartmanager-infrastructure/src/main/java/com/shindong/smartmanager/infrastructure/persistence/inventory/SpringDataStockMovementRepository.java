package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataStockMovementRepository extends JpaRepository<StockMovementJpaEntity, Long> {

    Optional<StockMovementJpaEntity> findFirstByReferenceTypeAndReferenceIdAndRecordingStateOrderByIdDesc(
            String referenceType,
            long referenceId,
            int recordingState
    );
}
