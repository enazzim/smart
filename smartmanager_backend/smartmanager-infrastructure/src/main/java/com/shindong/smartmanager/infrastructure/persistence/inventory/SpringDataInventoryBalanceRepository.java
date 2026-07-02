package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataInventoryBalanceRepository extends JpaRepository<InventoryBalanceJpaEntity, Long> {

    Optional<InventoryBalanceJpaEntity> findByItemIdAndLocationIdAndFiscalYearAndOutputProcessIdAndPartnerId(
            Long itemId,
            Long locationId,
            short fiscalYear,
            Long outputProcessId,
            Long partnerId
    );

    List<InventoryBalanceJpaEntity> findByOutputProcessIdAndRecordingState(Long outputProcessId, int recordingState);
}
