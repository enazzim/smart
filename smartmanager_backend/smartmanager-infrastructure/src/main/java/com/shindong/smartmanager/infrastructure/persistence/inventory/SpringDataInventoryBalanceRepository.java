package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataInventoryBalanceRepository extends JpaRepository<InventoryBalanceJpaEntity, Long> {

    @Query("""
            SELECT b FROM InventoryBalanceJpaEntity b
            WHERE b.itemId = :itemId
              AND b.locationId = :locationId
              AND b.fiscalYear = :fiscalYear
              AND b.recordingState = 1
              AND ((:outputProcessId IS NULL AND b.outputProcessId IS NULL) OR b.outputProcessId = :outputProcessId)
              AND ((:inputProcessId IS NULL AND b.inputProcessId IS NULL) OR b.inputProcessId = :inputProcessId)
              AND ((:partnerId IS NULL AND b.partnerId IS NULL) OR b.partnerId = :partnerId)
            """)
    Optional<InventoryBalanceJpaEntity> findActiveSlot(
            @Param("itemId") Long itemId,
            @Param("locationId") Long locationId,
            @Param("fiscalYear") short fiscalYear,
            @Param("outputProcessId") Long outputProcessId,
            @Param("inputProcessId") Long inputProcessId,
            @Param("partnerId") Long partnerId
    );

    Optional<InventoryBalanceJpaEntity> findByItemIdAndLocationIdAndFiscalYearAndOutputProcessIdAndPartnerId(
            Long itemId,
            Long locationId,
            short fiscalYear,
            Long outputProcessId,
            Long partnerId
    );

    Optional<InventoryBalanceJpaEntity> findByItemIdAndLocationIdAndFiscalYearAndInputProcessIdAndPartnerId(
            Long itemId,
            Long locationId,
            short fiscalYear,
            Long inputProcessId,
            Long partnerId
    );

    List<InventoryBalanceJpaEntity> findByOutputProcessIdAndRecordingState(Long outputProcessId, int recordingState);
}
