package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataInventoryLotRepository extends JpaRepository<InventoryLotJpaEntity, Long> {

    Optional<InventoryLotJpaEntity> findByIdAndRecordingState(long id, int recordingState);

    Optional<InventoryLotJpaEntity> findByItemIdAndLotNoAndRecordingState(
            long itemId,
            String lotNo,
            int recordingState
    );
}
