package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataInventoryBalanceMonthlyRepository
        extends JpaRepository<InventoryBalanceMonthlyJpaEntity, Long> {

    Optional<InventoryBalanceMonthlyJpaEntity> findByInventoryBalanceIdAndMonthNumAndRecordingState(
            Long inventoryBalanceId,
            byte monthNum,
            int recordingState
    );
}
