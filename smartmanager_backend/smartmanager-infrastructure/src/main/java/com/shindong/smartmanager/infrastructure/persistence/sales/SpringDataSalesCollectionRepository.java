package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.domain.sales.SalesCollectionStatus;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataSalesCollectionRepository extends JpaRepository<SalesCollectionJpaEntity, Long> {

    long countByCollectionNoStartingWithAndRecordingState(String prefix, int recordingState);

    Optional<SalesCollectionJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            SalesCollectionStatus status
    );

    @Query("""
            SELECT COALESCE(SUM(c.totalAmount), 0)
            FROM SalesCollectionJpaEntity c
            WHERE c.partnerId = :partnerId
              AND c.recordingState = :active
              AND c.status = :issued
            """)
    java.math.BigDecimal sumIssuedTotalAmountByPartnerId(
            @Param("partnerId") long partnerId,
            @Param("active") int active,
            @Param("issued") SalesCollectionStatus issued
    );

    @Query("""
            SELECT c FROM SalesCollectionJpaEntity c
            WHERE c.recordingState = :active
              AND (:collectionDateFrom IS NULL OR c.collectionDate >= :collectionDateFrom)
              AND (:collectionDateTo IS NULL OR c.collectionDate <= :collectionDateTo)
              AND (:collectionNo IS NULL OR c.collectionNo LIKE CONCAT('%', :collectionNo, '%'))
              AND (:status IS NULL OR c.status = :status)
              AND (:excludeCancelled = false OR c.status <> :cancelled)
            ORDER BY c.collectionDate DESC, c.id DESC
            """)
    List<SalesCollectionJpaEntity> searchActive(
            @Param("active") int active,
            @Param("collectionDateFrom") java.time.LocalDate collectionDateFrom,
            @Param("collectionDateTo") java.time.LocalDate collectionDateTo,
            @Param("collectionNo") String collectionNo,
            @Param("status") SalesCollectionStatus status,
            @Param("excludeCancelled") boolean excludeCancelled,
            @Param("cancelled") SalesCollectionStatus cancelled
    );
}
