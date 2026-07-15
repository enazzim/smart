package com.shindong.smartmanager.infrastructure.persistence.purchase;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataDefectClaimRepository extends JpaRepository<DefectClaimJpaEntity, Long> {

    Optional<DefectClaimJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    @Query("""
            SELECT d FROM DefectClaimJpaEntity d
            WHERE d.recordingState = 1
              AND (:partnerId IS NULL OR d.partnerId = :partnerId)
              AND (:itemId IS NULL OR d.itemId = :itemId)
              AND (:receiptDateFrom IS NULL OR d.receiptDate >= :receiptDateFrom)
              AND (:receiptDateTo IS NULL OR d.receiptDate <= :receiptDateTo)
            ORDER BY d.receiptDate DESC, d.id DESC
            """)
    List<DefectClaimJpaEntity> search(
            @Param("partnerId") Long partnerId,
            @Param("itemId") Long itemId,
            @Param("receiptDateFrom") LocalDate receiptDateFrom,
            @Param("receiptDateTo") LocalDate receiptDateTo
    );
}
