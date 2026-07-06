package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentStatus;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataOutsourcingShipmentRepository extends JpaRepository<OutsourcingShipmentJpaEntity, Long> {

    long countByShipmentNoStartingWithAndRecordingState(String prefix, int recordingState);

    List<OutsourcingShipmentJpaEntity> findByRecordingStateOrderByShipmentDateDescIdDesc(int recordingState);

    java.util.Optional<OutsourcingShipmentJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            OutsourcingShipmentStatus status
    );

    @Query("""
            SELECT s FROM OutsourcingShipmentJpaEntity s
            WHERE s.recordingState = :active
              AND (:shipmentDateFrom IS NULL OR s.shipmentDate >= :shipmentDateFrom)
              AND (:shipmentDateTo IS NULL OR s.shipmentDate <= :shipmentDateTo)
              AND (:shipmentNo IS NULL OR s.shipmentNo LIKE CONCAT('%', :shipmentNo, '%'))
              AND (:status IS NULL OR s.status = :status)
              AND (:excludeCancelled = false OR s.status <> :cancelled)
            ORDER BY s.shipmentDate DESC, s.id DESC
            """)
    List<OutsourcingShipmentJpaEntity> searchActive(
            @Param("active") int active,
            @Param("shipmentDateFrom") java.time.LocalDate shipmentDateFrom,
            @Param("shipmentDateTo") java.time.LocalDate shipmentDateTo,
            @Param("shipmentNo") String shipmentNo,
            @Param("status") OutsourcingShipmentStatus status,
            @Param("excludeCancelled") boolean excludeCancelled,
            @Param("cancelled") OutsourcingShipmentStatus cancelled
    );
}
