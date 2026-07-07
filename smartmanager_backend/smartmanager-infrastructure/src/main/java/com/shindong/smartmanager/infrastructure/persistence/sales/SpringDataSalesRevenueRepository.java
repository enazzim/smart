package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataSalesRevenueRepository extends JpaRepository<SalesRevenueJpaEntity, Long> {

    long countByRevenueNoStartingWithAndRecordingState(String prefix, int recordingState);

    List<SalesRevenueJpaEntity> findByRecordingStateOrderByRevenueDateDescIdDesc(int recordingState);

    java.util.Optional<SalesRevenueJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            SalesRevenueStatus status
    );

    @Query("""
            SELECT r FROM SalesRevenueJpaEntity r
            WHERE r.recordingState = :active
              AND (:revenueDateFrom IS NULL OR r.revenueDate >= :revenueDateFrom)
              AND (:revenueDateTo IS NULL OR r.revenueDate <= :revenueDateTo)
              AND (:revenueNo IS NULL OR r.revenueNo LIKE CONCAT('%', :revenueNo, '%'))
              AND (:status IS NULL OR r.status = :status)
              AND (:excludeCancelled = false OR r.status <> :cancelled)
            ORDER BY r.revenueDate DESC, r.id DESC
            """)
    List<SalesRevenueJpaEntity> searchActive(
            @Param("active") int active,
            @Param("revenueDateFrom") java.time.LocalDate revenueDateFrom,
            @Param("revenueDateTo") java.time.LocalDate revenueDateTo,
            @Param("revenueNo") String revenueNo,
            @Param("status") SalesRevenueStatus status,
            @Param("excludeCancelled") boolean excludeCancelled,
            @Param("cancelled") SalesRevenueStatus cancelled
    );
}
