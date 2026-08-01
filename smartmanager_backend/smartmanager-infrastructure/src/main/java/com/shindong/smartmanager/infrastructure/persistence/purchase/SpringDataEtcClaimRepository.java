package com.shindong.smartmanager.infrastructure.persistence.purchase;

import java.time.Instant;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataEtcClaimRepository extends JpaRepository<EtcClaimJpaEntity, Long> {

    Optional<EtcClaimJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    @Query("""
            SELECT e FROM EtcClaimJpaEntity e
            WHERE e.recordingState = 1
              AND (:partnerId IS NULL OR e.partnerId = :partnerId)
              AND (:receiptDateFrom IS NULL OR e.receiptDate >= :receiptDateFrom)
              AND (:receiptDateTo IS NULL OR e.receiptDate <= :receiptDateTo)
              AND (:reason IS NULL OR :reason = '' OR LOWER(e.reason) LIKE LOWER(CONCAT('%', :reason, '%')))
              AND (:registeredFromInstant IS NULL OR e.createdAt >= :registeredFromInstant)
              AND (:registeredToExclusiveInstant IS NULL OR e.createdAt < :registeredToExclusiveInstant)
            ORDER BY e.receiptDate DESC, e.id DESC
            """)
    List<EtcClaimJpaEntity> search(
            @Param("partnerId") Long partnerId,
            @Param("receiptDateFrom") LocalDate receiptDateFrom,
            @Param("receiptDateTo") LocalDate receiptDateTo,
            @Param("reason") String reason,
            @Param("registeredFromInstant") Instant registeredFromInstant,
            @Param("registeredToExclusiveInstant") Instant registeredToExclusiveInstant
    );
}
