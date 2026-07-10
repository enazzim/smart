package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.domain.inventory.MiscStockMovementStatus;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataMiscStockMovementRepository extends JpaRepository<MiscStockMovementJpaEntity, Long> {

    long countByMovementNoStartingWithAndRecordingState(String prefix, int recordingState);

    Optional<MiscStockMovementJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
