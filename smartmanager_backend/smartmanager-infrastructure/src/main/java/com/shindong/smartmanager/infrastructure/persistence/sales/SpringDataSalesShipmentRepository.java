package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.domain.sales.SalesShipmentStatus;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataSalesShipmentRepository extends JpaRepository<SalesShipmentJpaEntity, Long> {

    long countByShipmentNoStartingWithAndRecordingState(String prefix, int recordingState);

    List<SalesShipmentJpaEntity> findByRecordingStateOrderByShipmentDateDescIdDesc(int recordingState);

    java.util.Optional<SalesShipmentJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            SalesShipmentStatus status
    );

    @Query("""
            SELECT s FROM SalesShipmentJpaEntity s
            WHERE s.recordingState = :active
              AND (:shipmentDateFrom IS NULL OR s.shipmentDate >= :shipmentDateFrom)
              AND (:shipmentDateTo IS NULL OR s.shipmentDate <= :shipmentDateTo)
              AND (:shipmentNo IS NULL OR s.shipmentNo LIKE CONCAT('%', :shipmentNo, '%'))
              AND (:status IS NULL OR s.status = :status)
              AND (:excludeCancelled = false OR s.status <> :cancelled)
            ORDER BY s.shipmentDate DESC, s.id DESC
            """)
    List<SalesShipmentJpaEntity> searchActive(
            @Param("active") int active,
            @Param("shipmentDateFrom") java.time.LocalDate shipmentDateFrom,
            @Param("shipmentDateTo") java.time.LocalDate shipmentDateTo,
            @Param("shipmentNo") String shipmentNo,
            @Param("status") SalesShipmentStatus status,
            @Param("excludeCancelled") boolean excludeCancelled,
            @Param("cancelled") SalesShipmentStatus cancelled
    );
}
