package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataInventoryLotBalanceRepository extends JpaRepository<InventoryLotBalanceJpaEntity, Long> {

    List<InventoryLotBalanceJpaEntity> findByLotIdAndRecordingState(long lotId, int recordingState);

    Optional<InventoryLotBalanceJpaEntity> findByLotIdAndInventoryBalanceIdAndRecordingState(
            long lotId,
            long inventoryBalanceId,
            int recordingState
    );
}
